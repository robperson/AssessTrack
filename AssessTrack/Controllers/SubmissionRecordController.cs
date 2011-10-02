using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using System.Web.Security;
using AssessTrack.Helpers;
using AssessTrack.Filters;


namespace AssessTrack.Controllers
{
    
    public class SubmissionRecordViewModel
    {
        public SelectList AssessmentList;
        public List<SubmissionRecord> Submissions = new List<SubmissionRecord>();

        
    }

    public class CreateSubmissionViewModel
    {
        public SelectList Students;
        public SelectList Assessments;

        public CreateSubmissionViewModel(List<Profile> students, List<Assessment> assessments)
        {
            Students = new SelectList(students, "MembershipID", "FullName");
            Assessments = new SelectList(assessments, "AssessmentID", "Name");
        }
    }

    public class SingleSubmissionViewModel
    {
        public List<SubmissionRecord> OtherSubmissionRecords;
        public SubmissionRecord SubmissionRecord;

        public SingleSubmissionViewModel(SubmissionRecord record, List<SubmissionRecord> otherrecords)
        {
            OtherSubmissionRecords = otherrecords;
            SubmissionRecord = record;
        }
    }

    [ATAuth(AuthScope = AuthScope.Site, MinLevel = 1, MaxLevel = 10)]
    public class SubmissionRecordController : ATController
    {
        //
        // GET: /SubmissionRecord/
        public ActionResult Index(string siteShortName, string courseTermShortName, Guid? id)
        {
            SubmissionRecordViewModel model = new SubmissionRecordViewModel();
            Assessment assessment = null;
            IEnumerable<Assessment> assessments = dataRepository.GetAllNonTestBankAssessments(courseTerm);

            if (id != null)
                assessment = dataRepository.GetAssessmentByID(id.Value);

            if (assessment != null)
            {
                model.AssessmentList = new SelectList(assessments, "AssessmentID", "Name", id.Value);
                model.Submissions.AddRange(dataRepository.GetMostRecentSubmissions(assessment));
            }
            else
            {
                model.AssessmentList = new SelectList(assessments, "AssessmentID", "Name");
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string siteShortName, string courseTermShortName, Guid id)
        {
            SubmissionRecordViewModel model = new SubmissionRecordViewModel();
            Assessment assessment = null;
            IEnumerable<Assessment> assessments = dataRepository.GetAllNonTestBankAssessments(courseTerm);

            assessment = dataRepository.GetAssessmentByID(id);

            if (assessment != null)
            {
                model.AssessmentList = new SelectList(assessments, "AssessmentID", "Name", id);
                model.Submissions = dataRepository.GetMostRecentSubmissions(assessment);
            }
            else
            {
                return View("AssessmentNotFound");
            }

            return View(model);
        }

        //
        // Create new SubmissionRecord with grade
        public ActionResult CreateSubmission()
        {
            List<Profile> students = dataRepository.GetStudentProfiles(courseTerm);
            List<Assessment> assessments = dataRepository.GetAllNonTestBankAssessments(courseTerm);
            
            CreateSubmissionViewModel model = new CreateSubmissionViewModel(students,assessments);
            
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSubmission(Guid AssessmentID, Guid MembershipID, double Score, DateTime SubmissionDate)
        {
            try
            {
                Assessment assessment = dataRepository.GetAssessmentByID(courseTerm, AssessmentID);
                if (assessment == null)
                {
                    return View("AssessmentNotFound");
                }

                SubmissionRecord record = new SubmissionRecord();
                assessment.SubmissionRecords.Add(record);
                record.GradedBy = UserHelpers.GetCurrentUserID();
                record.GradedOn = DateTime.Now;
                record.StudentID = MembershipID;
                record.SubmissionDate = SubmissionDate;

                foreach (var answer in assessment.Answers)
                {
                    AssessTrack.Models.Response response = new Response();
                    response.Score = (Score / 100.0) * answer.Weight;
                    response.AnswerID = answer.AnswerID;
                    response.ResponseText = "n/a";
                    record.Responses.Add(response);
                }

                dataRepository.Save();
            }
            catch (Exception)
            {
                ModelState.AddModelError("_FORM", "An error occurred while creating the submission.");
                List<Profile> students = dataRepository.GetStudentProfiles(courseTerm);
                List<Assessment> assessments = dataRepository.GetAllNonTestBankAssessments(courseTerm);
            
                CreateSubmissionViewModel model = new CreateSubmissionViewModel(students,assessments);
            
                return View(model);
            }
            FlashMessageHelper.AddMessage("Score added successfully!");
            return RedirectToRoute(new { siteShortName = site.ShortName, courseTermShortName = courseTerm.ShortName, action = "Index", controller = "SubmissionRecord" });
        }

        public ActionResult Grade(string siteShortName, string courseTermShortName, Guid id)
        {
            SubmissionRecord submission = dataRepository.GetSubmissionRecordByID(id);
            if (submission == null)
                return View("SubmissionNotFound");
            //TODO ensure user has permission to grade this
            List<SubmissionRecord> othersubmissions = dataRepository.GetOtherSubmissionRecords(submission);
            SingleSubmissionViewModel model = new SingleSubmissionViewModel(submission, othersubmissions);
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Grade(string siteShortName, string courseTermShortName, Guid id, FormCollection input)
        {
            SubmissionRecord submission = dataRepository.GetSubmissionRecordByID(id);
            if (submission == null)
                return View("SubmissionNotFound");

            List<SubmissionRecord> othersubmissions = dataRepository.GetOtherSubmissionRecords(submission);
            SingleSubmissionViewModel model = new SingleSubmissionViewModel(submission, othersubmissions);
            if (ModelState.IsValid)
            {
                try
                {
                    submission.GradedOn = DateTime.Now;
                    Guid userid = UserHelpers.GetCurrentUserID();
                    submission.GradedBy = userid;
                    submission.Comments = input["Comments"];
                    foreach (Models.Response response in submission.Responses)
                    {
                        response.Score = Convert.ToDouble(input["score-" + response.AnswerID.ToString()]);
                        response.Comment = input["comment-" + response.AnswerID.ToString()];
                    }
                    dataRepository.Save();
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName, id = submission.AssessmentID });
                }
                catch (FormatException)
                {
                    FlashMessageHelper.AddMessage("An error occurred while saving scores. One of the scores entered is not a valid number.");
                }
                catch
                {
                    FlashMessageHelper.AddMessage("An unexpected error has occurred.");
                }

            }
            return View(model);
        }

        public ActionResult View(string siteShortName, string courseTermShortName, Guid id)
        {
            SubmissionRecord submission = dataRepository.GetSubmissionRecordByID(id);
            if (submission == null)
                return View("SubmissionNotFound");
            List<SubmissionRecord> othersubmissions = dataRepository.GetOtherSubmissionRecords(submission);
            SingleSubmissionViewModel model = new SingleSubmissionViewModel(submission, othersubmissions);

            //TODO ensure user has permission to view this
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GradeAll(Guid id)
        {
            try
            {
                Assessment assessment = dataRepository.GetAssessmentByID(courseTerm, id);
                dataRepository.GradeAllResponses(assessment);
                dataRepository.Save();
                FlashMessageHelper.AddMessage(assessment.Name + "'s submissions were graded successfully.");
            }
            catch
            {
                FlashMessageHelper.AddMessage("An error occurred while grading all submissions.");
            }
            return RedirectToAction("Index", new { siteShortName = site.ShortName, courseTermShortName = courseTerm.ShortName, id = id });
        }

    }
}
