using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Filters;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using AssessTrack.Models.ReportsAndTools;

namespace AssessTrack.Controllers
{
    public class DownloadAnswerCodeViewModel
    {
        public SelectList Assessments;
        public List<Answer> Answers;
        public List<string> AnswerTypes;
        public Assessment Assessment;
    }
    [ATAuth(AuthScope=AuthScope.CourseTerm,MinLevel=5,MaxLevel=10)]
    public class CourseTermToolsController : ATController
    {
        //
        // GET: /CourseTermTools/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DownloadAnswerCode(Guid? id)
        {
            DownloadAnswerCodeViewModel model = new DownloadAnswerCodeViewModel();
            model.Assessments = new SelectList(dataRepository.GetAllNonTestBankAssessments(courseTerm), "AssessmentID", "Name", id);

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DownloadAnswerCode(Guid AssessmentID)
        {
            DownloadAnswerCodeViewModel model = new DownloadAnswerCodeViewModel();
            model.Assessments = new SelectList(dataRepository.GetAllNonTestBankAssessments(courseTerm), "AssessmentID", "Name", AssessmentID);

            Assessment assessment = dataRepository.GetAssessmentByID(courseTerm,AssessmentID);
            if (assessment != null)
            {
                model.Answers = (from answer in assessment.Answers
                                 //where answer.Type == "code-answer"
                                 select answer).ToList();
                model.AnswerTypes = (from answer in assessment.Answers
                                     select answer.Type).ToList();
                model.Assessment = assessment;
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetCodeAnswerDownloadLink(Guid? AnswerID, String AnswerType)
        {
            if (AnswerID == null)
                return View("AnswerNotFound");
            
            string tempDir = Environment.GetEnvironmentVariable("TEMP") + "\\assesstrack-code-for-answer" + AnswerID + "\\";
            Directory.CreateDirectory(tempDir);

            string contentDir = Server.MapPath("/Content/CodeDownloads/");

            //Get All responses for the requested answer
            string filenameFormat = "{0}_{1}s_{2}_Question{3}_Answer{4}_{5}.cpp";
            if(!AnswerType.Equals("code-answer"))
                filenameFormat = "{0}_{1}s_{2}_Question{3}_Answer{4}_{5}.txt";
            //"{FirstName}_{LastName}s_{AssessmentName}_Question{Number}_Answer{Number}_{SubmissionID}.cpp"

            List<Response> responses;
            FullResponseList responselist = new FullResponseList();
            responses = responselist.GetFullResponseList(AnswerID);

            if (responses.Count == 0)
            {
                return View("NoResponses");
            }
            string zipFileName = responses[0].Answer.Assessment.Name + "_Question" + responses[0].Answer.Question.Number + "_Answer" + responses[0].Answer.Number + "_Code.zip";
            FileStream fsOut = System.IO.File.Create(contentDir + zipFileName);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression
            
            foreach (var response in responses)
            {
                string filename = string.Format(filenameFormat,
                    response.SubmissionRecord.Profile.FirstName,
                    response.SubmissionRecord.Profile.LastName,
                    response.SubmissionRecord.Assessment.Name,
                    response.Answer.Question.Number,
                    response.Answer.Number,
                    response.SubmissionRecordID);
                StreamWriter writer = System.IO.File.CreateText(tempDir + filename);
                writer.Write(response.ResponseText);
                writer.Close();

                string entryName = ZipEntry.CleanName(filename); // Removes drive from name and fixes slash direction
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = DateTime.Now;

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either.
                   zipStream.UseZip64 = UseZip64.Off;
                //   newEntry.Size = the file size;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = System.IO.File.OpenRead(tempDir + filename))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }

            zipStream.IsStreamOwner = true;	// Makes the Close also Close the underlying stream
            zipStream.Close();

            return RedirectToRoute(new { controller = "CourseTermTools", action = "ShowCodeDownloadLink", courseTermShortName = courseTerm.ShortName, siteShortName = site.ShortName, link = "/Content/CodeDownloads/" + zipFileName });
        }

        public ActionResult ShowCodeDownloadLink(string link)
        {
            ViewData["CodeDownloadLink"] = link;
            return View("CodeAnswerDownloadLink");
        }
    }
}
