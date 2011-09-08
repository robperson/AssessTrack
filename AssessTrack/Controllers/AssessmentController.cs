using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Helpers;
using System.Web.Security;
using AssessTrack.Filters;
using System.Xml;
using System.Text;
using System.Transactions;

namespace AssessTrack.Controllers
{
    public class AssessmentFormViewModel
    {
        public Assessment Assessment;
        public SelectList AssessmentTypes;
        public AssessmentFormViewModel(Assessment assessment, SelectList assessmentTypes, string data)
        {
            Assessment = assessment;
            AssessmentTypes = assessmentTypes;
            DesignerData = data;
        }
        public AssessmentFormViewModel(Assessment assessment, SelectList assessmentTypes): this(assessment, assessmentTypes, "")
        {
        }
        public string DesignerData;
    }

    public class AssessmentIndexViewModel
    {
        public List<AssessmentTableModel> AssessmentTables;
        

        public AssessmentIndexViewModel(List<AssessmentTableModel> tables)
        {
            AssessmentTables = tables;
        }
    }

    public class AssessmentTableModel
    {
        public string Caption;
        public List<Assessment> Assessments;
        public bool Admin = false;

        public AssessmentTableModel(string cap, List<Assessment> assessments, bool admin)
        {
            Caption = cap;
            Assessments = assessments;
            Admin = admin;
        }
    }

    [ValidateInput(false)]
    public class AssessmentController : ATController
    {
        //
        // GET: /Assessment/
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Index(string siteShortName, string courseTermShortName)
        {
            try
            {
                bool admin = false;
                
                List<AssessmentTableModel> tables = new List<AssessmentTableModel>();
                CourseTermMember member = dataRepository.GetCourseTermMemberByMembershipID(courseTerm, UserHelpers.GetCurrentUserID());
                if (member != null)
                {
                    admin = (member.AccessLevel > 1);
                }
                if (admin)
                {
                    tables.Add(new AssessmentTableModel("Private Assessments", dataRepository.GetPrivateAssessments(courseTerm),admin));
                }
                tables.Add(new AssessmentTableModel("Current Assessments", dataRepository.GetUpcomingAssessments(courseTerm),admin));
                tables.Add(new AssessmentTableModel("Past Assessments", dataRepository.GetPastDueAssessments(courseTerm),admin));

                if (admin)
                {
                    tables.Add(new AssessmentTableModel("Question Bank Assessments", dataRepository.GetQuestionBankAssessments(courseTerm),admin));
                }

                return View(new AssessmentIndexViewModel(tables));
            }
            catch(Exception e)
            {
                throw;
                //probably return not authorized view
            }
        }

        //
        // GET: /Assessment/Details/5
        [ATAuth(AuthScope = AuthScope.CourseTerm,MinLevel=0,MaxLevel=10)]
        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            Assessment assessment = dataRepository.GetAssessmentByID(courseTerm,id);
            if (assessment == null)
                return View("AssessmentNotFound");
            return View(assessment);
        }

        //
        // GET: /Assessment/Create
        [ATAuth(AuthScope = AuthScope.CourseTerm,MinLevel=3,MaxLevel=10)]
        public ActionResult Create(string courseTermShortName, string siteShortName)
        {
            Assessment assessment = new Assessment() { DueDate = DateTime.Now.AddDays(1), CreatedDate = DateTime.Now };
            return View(new AssessmentFormViewModel(assessment, dataRepository.GetAssessmentTypesSelectList(courseTerm)));
        } 

        //
        // POST: /Assessment/Create

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Create(string courseTermShortName, string siteShortName, Assessment newAssessment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    newAssessment.CourseTermID = courseTerm.CourseTermID;
                    dataRepository.SaveAssessment(newAssessment);
                    string previewlink = Url.Action("preview", new
                    {
                        controller = "assessment",
                        siteShortName = site.ShortName,
                        courseTermShortName = courseTerm.ShortName,
                        id = newAssessment.AssessmentID
                    });
                    FlashMessageHelper.AddMessage(string.Format(@"Assessment Saved Successfully! <a href=""{0}"" target=""_NEWWINDOW_"">Click here to preview.</a>", previewlink));
                    return RedirectToAction("Edit", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName, id = newAssessment.AssessmentID });
                    //return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(newAssessment.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex.Message);
                }

            }
            string data = DesignerHelper.LoadAssessment(newAssessment.Data);
            
            return View(new AssessmentFormViewModel(newAssessment, dataRepository.GetAssessmentTypesSelectList(courseTerm, newAssessment.AssessmentTypeID),data));
                
        }

        //
        // GET: /Assessment/Edit/5
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id)
        {
            Assessment assessment = dataRepository.GetAssessmentByID(courseTerm, id);
            

            if (assessment == null)
                return View("AssessmentNotFound");
            //Fix markup if question ID's are lost somehow
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(assessment.Data);
            //if (doc.SelectSingleNode("//question[@id=\"\"]") != null) //If there exists a question node with an empty id attribute, something bad has happened
            //{
            //    //Rebuild the markup from the data in the database
            //    StringBuilder newmarkup = new StringBuilder();
            //    newmarkup.Append("<assessment>\n");
            //    foreach (var question in assessment.Questions)
            //    {
            //        newmarkup.Append(question.Data);
            //    }
            //    newmarkup.Append("</assessment>");
            //    assessment.Data = newmarkup.ToString();
            //}

            string data = DesignerHelper.LoadAssessment(assessment.Data);
            // Convert ampersand to appropriate html entity
            data = data.Replace("&", "&amp;");
            return View(new AssessmentFormViewModel(assessment,dataRepository.GetAssessmentTypesSelectList(courseTerm,assessment.AssessmentTypeID), data));
        }

        //
        // POST: /Assessment/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id, FormCollection collection)
        {
            Assessment assessment = dataRepository.GetAssessmentByID(courseTerm, id);
            if (assessment == null)
                return View("AssessmentNotFound");
            try
            {
                UpdateModel(assessment);
                if (ModelState.IsValid)
                {
                    dataRepository.SaveAssessment(assessment, false);
                    string previewlink = Url.Action("preview", new
                    {
                        controller = "assessment",
                        siteShortName = site.ShortName,
                        courseTermShortName = courseTerm.ShortName,
                        id = id
                    });
                    FlashMessageHelper.AddMessage(string.Format(@"Assessment Saved Successfully! <a href=""{0}"" target=""_NEWWINDOW_"">Click here to preview.</a>",previewlink));
                    return RedirectToAction("Edit", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName, id = id });
                }
            }
            catch (RuleViolationException)
            {
                ModelState.AddModelErrors(assessment.GetRuleViolations());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_FORM", ex.Message);
            }
            string data = DesignerHelper.LoadAssessment(assessment.Data);
            data = data.Replace("&", "&amp;");
            return View(new AssessmentFormViewModel(assessment, dataRepository.GetAssessmentTypesSelectList(courseTerm, assessment.AssessmentTypeID), data));
        }
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 1, MaxLevel = 1)]
        public ActionResult Submit(string courseTermShortName, string siteShortName, Guid id)
        {
            Assessment assessment = dataRepository.GetAssessmentByID(courseTerm, id);
            if (assessment == null || !assessment.IsVisible)
                return View("AssessmentNotFound");
            if (!assessment.AllowMultipleSubmissions && dataRepository.HasUserSubmittedAssessment(assessment))
            {
                return View("AlreadySubmitted");
            }
            if (DateTime.Now.Subtract(assessment.DueDate).TotalSeconds > 0)
            {
                if (!AssessmentHelpers.StudentHasExtension(assessment, UserHelpers.GetCurrentUserID()))
                {
                    return View("DueDatePassed");
                }
            }
            if (!assessment.IsOpen)
            {
                return View("AssessmentClosed");
            }
            return View(assessment);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 1, MaxLevel = 1)]
        public ActionResult Submit(string courseTermShortName, string siteShortName, Guid id, FormCollection collection)
        {
            Assessment assessment = dataRepository.GetAssessmentByID(courseTerm, id);
            if (assessment == null || !assessment.IsVisible)
                return View("AssessmentNotFound");
            if (!assessment.AllowMultipleSubmissions && dataRepository.HasUserSubmittedAssessment(assessment))
            {
                return View("AlreadySubmitted");
            }
            if (DateTime.Now.Subtract(assessment.DueDate).TotalSeconds > 0)
            {
                if (!AssessmentHelpers.StudentHasExtension(assessment, UserHelpers.GetCurrentUserID()))
                {
                    return View("DueDatePassed");
                }
            }
            if (!assessment.IsOpen)
            {
                return View("AssessmentClosed");
            }
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    //Upload all attachments and store their IDs
                    Dictionary<string, string> answerAttachments = new Dictionary<string, string>();
                    foreach (Answer answer in assessment.Answers)
                    {
                        if (answer.Type == "attachment")
                        {
                            AssessTrack.Models.File file = FileUploader.GetFile(answer.AnswerID.ToString(), Request);
                            if (file != null)
                            {
                                dataRepository.SaveFile(file);
                                dataRepository.Save();
                                string downloadUrl = Url.Action("Download", "File", new { id = file.FileID });
                                string link = string.Format("<a href=\"{0}\">Click here to download {1}.</a>", downloadUrl, file.Name);
                                answerAttachments.Add(answer.AnswerID.ToString(), link);
                            }
                        }
                    }
                    
                    SubmissionRecord record = new SubmissionRecord();
                    
                    Guid studentID = dataRepository.GetLoggedInProfile().MembershipID;
                    record.StudentID = studentID;
                    record.SubmissionDate = DateTime.Now;
                    
                    foreach (Answer answer in assessment.Answers)
                    {
                        string responseText = collection[answer.AnswerID.ToString()];

                        //Put the download link in responseText if this is an attachment
                        if (answer.Type == "attachment")
                        {
                            answerAttachments.TryGetValue(answer.AnswerID.ToString(), out responseText);
                        }
                        
                        if (responseText != null)
                        {
                            Response response = new Response();
                            response.Answer = answer;
                            response.ResponseText = responseText;
                            record.Responses.Add(response);
                        }
                        
                    }
                    record.Assessment = assessment;
                    dataRepository.SaveSubmissionRecord(record);
                    FlashMessageHelper.AddMessage("Your answers were submitted successfully!");
                    transaction.Complete();
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                }
            }
            catch
            {
                FlashMessageHelper.AddMessage("An error occurred while submitting your answers :'(");
                return View(assessment);
            }
        }

        public ActionResult Preview(string siteShortName, string courseTermShortName, Guid id)
        {
            Assessment assessment = dataRepository.GetAssessmentByID(id);
            if (assessment == null)
                return View("AssessmentNotFound");
            //TODO ensure user has permission to view this
            return View(assessment);
        }

        public ActionResult Delete(Guid id)
        {
            Assessment assessment = dataRepository.GetAssessmentByID(id);
            if (assessment == null)
                return View("AssessmentNotFound");

            return View(assessment);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id, FormCollection input)
        {
            Assessment assessment = dataRepository.GetAssessmentByID(id);
            if (assessment == null)
                return View("AssessmentNotFound");

            try
            {
                dataRepository.DeleteAssessment(assessment);
                dataRepository.Save();
                FlashMessageHelper.AddMessage("Assessment deleted successfully.");
            }
            catch (Exception e)
            {
                FlashMessageHelper.AddMessage("An error occurred: " + e.Message);
            }

            return RedirectToAction("Index", new { siteShortName = site.ShortName, courseTermShortName = courseTerm.ShortName });
        }
    }
}
