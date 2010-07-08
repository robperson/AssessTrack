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

    public class AssessmentListViewModel
    {
        public List<Assessment> CurrentAssessments;
        public List<Assessment> PastAssessments;

        public AssessmentListViewModel(List<Assessment> currAssessments, List<Assessment> pastAssessments)
        {
            CurrentAssessments = currAssessments;
            PastAssessments = pastAssessments;
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
            List<Assessment> curr = dataRepository.GetUpcomingAssessments(courseTerm);
            List<Assessment> past = dataRepository.GetPastDueAssessments(courseTerm);
            return View(new AssessmentListViewModel(curr,past));
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
                    newAssessment.CourseTerm = courseTerm;
                    dataRepository.SaveAssessment(newAssessment);
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
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
            string data = DesignerHelper.LoadAssessment(assessment.Data);
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
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
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
                SubmissionRecord record = new SubmissionRecord();
                record.Assessment = assessment;
                Guid studentID = dataRepository.GetLoggedInProfile().MembershipID;
                record.StudentID = studentID;
                record.SubmissionDate = DateTime.Now;
                foreach (Answer answer in assessment.Answers)
                {
                    Response response = new Response();
                    response.Answer = answer;
                    response.ResponseText = collection[answer.AnswerID.ToString()];
                    record.Responses.Add(response);
                }
                dataRepository.SaveSubmissionRecord(record);
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            catch
            {
                return View(assessment);
            }
        }
    }
}
