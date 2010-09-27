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
    [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 1, MaxLevel = 10)]
    public class CourseTermMessageController : ATController
    {
        //
        // GET: /AssessmentType/
        public ActionResult Index(string siteShortName, string courseTermShortName)
        {
            return View(courseTerm.CourseTermMessages.ToList());
        }

        //
        // GET: /AssessmentType/Details/5

        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            CourseTermMessage message = dataRepository.GetCourseTermMessageByID(id);
            if (message == null)
                return View("MessageNotFound");
            return View(message);
        }

        //
        // GET: /AssessmentType/Create
        [ATAuth(AuthScope=AuthScope.CourseTerm,MinLevel=5,MaxLevel=10)]
        public ActionResult Create(string courseTermShortName, string siteShortName)
        {
            CourseTermMessage message = new CourseTermMessage();
            message.Subject = string.Empty;
            message.Body = string.Empty;
            return View(message);
        } 

        //
        // POST: /AssessmentType/Create

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 5, MaxLevel = 10)]
        public ActionResult Create(string courseTermShortName, string siteShortName, CourseTermMessage message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    message.CreatedDate = DateTime.Now;
                    message.CreatedBy = UserHelpers.GetCurrentUserID();
                    courseTerm.CourseTermMessages.Add(message);
                    dataRepository.Save();
                    FlashMessageHelper.AddMessage("Message created successfully.");
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(message.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            return View(message);
           
        }

        //
        // GET: /AssessmentType/Edit/5
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 5, MaxLevel = 10)]
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id)
        {
            CourseTermMessage message = dataRepository.GetCourseTermMessageByID(id);
            if (message == null)
                return View("MessageNotFound");
            return View(message);
        }

        //
        // POST: /AssessmentType/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 5, MaxLevel = 10)]
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id, FormCollection collection)
        {
            CourseTermMessage message = dataRepository.GetCourseTermMessageByID(id);
            if (message == null)
                return View("MessageNotFound");
            
            UpdateModel(message);
            if (ModelState.IsValid)
            {
                try
                {
                    message.CreatedDate = DateTime.Now;
                    dataRepository.Save();
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(message.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            return View(message);
        }
    }
}
