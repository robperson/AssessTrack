using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Helpers;

namespace AssessTrack.Controllers
{
    public class SubmissionExceptionFormModel
    {
        public SelectList AssessmentsList;
        public SelectList StudentsList;
        public DateTime DueDate = DateTime.Now;

        public SubmissionExceptionFormModel(CourseTerm ct): this(ct.Assessments.OrderBy(a => a.Name).ToList(),ct)
        {
        }
        public SubmissionExceptionFormModel(List<Assessment> Assessments, CourseTerm ct)
        {

            AssessmentsList = new SelectList(Assessments, "AssessmentID", "Name");
            StudentsList = new SelectList(ct.GetMembers(1, 1), "MembershipID", "FullName");
        }

        public SubmissionExceptionFormModel(CourseTerm ct, object selectedStudent, object selectedAssessment, DateTime dueDate)
        {
            AssessmentsList = new SelectList(ct.Assessments, "AssessmentID", "Name", selectedAssessment);
            StudentsList = new SelectList(ct.GetMembers(1,1), "MembershipID", "FullName", selectedStudent);
            DueDate = dueDate;
        }

        public SubmissionExceptionFormModel(List<Assessment> Assessments, CourseTerm ct, object selectedStudent, object selectedAssessment, DateTime dueDate)
        {
            AssessmentsList = new SelectList(Assessments, "AssessmentID", "Name", selectedAssessment);
            StudentsList = new SelectList(ct.GetMembers(1, 1), "MembershipID", "FullName", selectedStudent);
            DueDate = dueDate;
        }
    }

    public class SubmissionExceptionController : ATController
    {
        //
        // GET: /SubmissionException/

        public ActionResult Index(string siteShortName, string courseTermShortName)
        {
            return View(courseTerm.SubmissionExceptions.ToList());
        }

        //
        // GET: /SubmissionException/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SubmissionException/Create

        public ActionResult Create(string siteShortName, string courseTermShortName)
        {
            List<Assessment> list = dataRepository.GetAllNonTestBankAssessments(courseTerm);
            list.Sort(new Comparison<Assessment>((a1, a2) => a1.Name.CompareTo(a2.Name)));
            return View(new SubmissionExceptionFormModel(list,courseTerm));
        } 

        //
        // POST: /SubmissionException/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string siteShortName, string courseTermShortName, FormCollection collection)
        {
            SubmissionException subExc = new SubmissionException();
            try
            {
                TryUpdateModel(subExc);
                subExc.CourseTerm = courseTerm;
                dataRepository.Save();
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            catch (RuleViolationException)
            {
                List<Assessment> list = dataRepository.GetAllNonTestBankAssessments(courseTerm);
                list.Sort(new Comparison<Assessment>((a1, a2) => a1.Name.CompareTo(a2.Name)));
                ModelState.AddModelErrors(subExc.GetRuleViolations());
                return View(new SubmissionExceptionFormModel(list,courseTerm, subExc.StudentID, subExc.AssessmentID, subExc.DueDate));
            }
            catch
            {
                throw;
            }
        }

        //
        // GET: /SubmissionException/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SubmissionException/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
