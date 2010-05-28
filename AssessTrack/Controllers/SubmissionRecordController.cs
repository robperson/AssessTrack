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
        public List<SubmissionRecord> Submissions;

        public SubmissionRecordViewModel(List<Assessment> assessments)
        {
            AssessmentList = new SelectList(assessments,"AssessmentID","Name");
            Submissions = new List<SubmissionRecord>();
        }

        public SubmissionRecordViewModel(List<Assessment> assessments, Assessment selectedAssessment)
        {
            AssessmentList = new SelectList(assessments, "AssessmentID", "Name", selectedAssessment.AssessmentID);
            Submissions = selectedAssessment.SubmissionRecords.ToList();
        }
    }

    [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
    public class SubmissionRecordController : ATController
    {
        //
        // GET: /SubmissionRecord/
        public ActionResult Index(string siteShortName, string courseTermShortName, Guid? id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site,courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            if (id == null)
                return View(new SubmissionRecordViewModel(courseTerm.Assessments.ToList()));
            Assessment assessment = dataRepository.GetAssessmentByID(courseTerm, (Guid)id);
            if (assessment == null)
                return View(new SubmissionRecordViewModel(courseTerm.Assessments.ToList()));
            return View(new SubmissionRecordViewModel(courseTerm.Assessments.ToList(), assessment));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string siteShortName, string courseTermShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site,courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            Assessment assessment = dataRepository.GetAssessmentByID(courseTerm, id);
            return View(new SubmissionRecordViewModel(courseTerm.Assessments.ToList(),assessment));
        }

        public ActionResult Grade(string siteShortName, string courseTermShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site,courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            SubmissionRecord submission = dataRepository.GetSubmissionRecordByID(id);
            if (submission == null)
                return View("SubmissionNotFound");
            //TODO ensure user has permission to grade this
            return View(submission);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Grade(string siteShortName, string courseTermShortName, Guid id, FormCollection input)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site,courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            SubmissionRecord submission = dataRepository.GetSubmissionRecordByID(id);
            if (submission == null)
                return View("SubmissionNotFound");
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
                        response.Score = Convert.ToDouble(input[response.AnswerID.ToString()]);
                    }
                    dataRepository.Save();
                    return RedirectToAction("Index", new { id = submission.AssessmentID });
                }
                catch
                {
                    throw;
                }

            }
            return View(submission);
        }

    }
}
