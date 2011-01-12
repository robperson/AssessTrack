using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Helpers;
using AssessTrack.Filters;

namespace AssessTrack.Controllers
{
    [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
    public class AssessmentTypeController : ATController
    {
        //
        // GET: /AssessmentType/
        public ActionResult Index(string siteShortName, string courseTermShortName)
        {
            return View(courseTerm.AssessmentTypes.ToList());
        }

        //
        // GET: /AssessmentType/Details/5

        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            AssessmentType assessmentType = dataRepository.GetAssessmentTypeByID(courseTerm, id);
            if (assessmentType == null)
                return View("AssessmentTypeNotFound");
            return View(assessmentType);
        }

        //
        // GET: /AssessmentType/Create

        public ActionResult Create(string courseTermShortName, string siteShortName)
        {
            AssessmentType newType = new AssessmentType() { Weight = 0, QuestionBank = false, CourseTerm = courseTerm };
            return View(newType);
        } 

        //
        // POST: /AssessmentType/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string courseTermShortName, string siteShortName, AssessmentType newType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    courseTerm.AssessmentTypes.Add(newType);
                    dataRepository.Save();
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(newType.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            return View(newType);
           
        }

        //
        // GET: /AssessmentType/Edit/5

        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id)
        {
            AssessmentType assessmentType = dataRepository.GetAssessmentTypeByID(courseTerm, id);
            if (assessmentType == null)
                return View("AssessmentTypeNotFound");
            return View(assessmentType);
        }

        //
        // POST: /AssessmentType/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id, FormCollection collection)
        {
            AssessmentType assessmentType = dataRepository.GetAssessmentTypeByID(courseTerm, id);
            if (assessmentType == null)
                return View("AssessmentTypeNotFound");
            UpdateModel(assessmentType);
            if (ModelState.IsValid)
            {
                try
                {
                    dataRepository.Save();
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(assessmentType.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            return View(assessmentType);
        }

        //
        // GET: /AssessmentType/Delete/5

        public ActionResult Delete(string courseTermShortName, string siteShortName, Guid id)
        {
            AssessmentType assessmentType = dataRepository.GetAssessmentTypeByID(courseTerm, id);
            if (assessmentType == null)
                return View("AssessmentTypeNotFound");
            return View(assessmentType);
        }

        //
        // POST: /AssessmentType/Delete/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(string courseTermShortName, string siteShortName, Guid id, FormCollection collection)
        {
            AssessmentType assessmentType = dataRepository.GetAssessmentTypeByID(courseTerm, id);
            if (assessmentType == null)
                return View("AssessmentTypeNotFound");
            try
            {
                dataRepository.DeleteAssessmentType(assessmentType);
                dataRepository.Save();
                FlashMessageHelper.AddMessage("Assessment Type deleted successfully.");
            }
            catch (Exception ex)
            {
                FlashMessageHelper.AddMessage("An error occured while deleting: " + ex.Message);
            }

            return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
        }
    }
}
