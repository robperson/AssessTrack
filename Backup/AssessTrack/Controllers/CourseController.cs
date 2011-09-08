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
    [Authorize()]
    public class CourseController : ATController
    {
        //
        // GET: /Course/
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Index(string siteShortName)
        {
            return View(dataRepository.GetSiteCourses(site));
        }

        //
        // GET: /Course/Details/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Details(string siteShortName, Guid id)
        {
            Course course = dataRepository.GetCourseByID(id);
            if (course != null)
            {
                return View(course);
            }
            else
            {
                return View("CourseNotFound");
            }
        }

        //
        // GET: /Course/Create
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Create(string siteShortName)
        {
            Course course = new Course();
            return View(course);
        } 

        //
        // POST: /Course/Create

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Create(string siteShortName, Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    course.Site = site;
                    dataRepository.CreateCourse(course);
                    return RedirectToAction("Index", new { siteShortName = siteShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(course.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            return View(course);
        }

        //
        // GET: /Course/Edit
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Edit(string siteShortName, Guid id)
        {
            Course course = dataRepository.GetCourseByID(id);
            if (course != null)
            {
                return View(course);
            }
            else
            {
                return View("CourseNotFound");
            }
        }

        //
        // POST: /Course/Edit

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Edit(string siteShortName, Guid id, FormCollection collection)
        {
            Course course = dataRepository.GetCourseByID(id);
            if (course != null)
            {
                UpdateModel<Course>(course);
                if (ModelState.IsValid)
                {
                    try
                    {
                        dataRepository.Save();

                        return RedirectToAction("Details", new { siteShortName = siteShortName, id = course.CourseID });
                    }
                    catch (RuleViolationException)
                    {
                        ModelState.AddModelErrors(course.GetRuleViolations());
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("_FORM", ex);
                    }
                }
                return View(course);
            }
            else
            {
                return View("CourseNotFound");
            }
        }

        //
        // GET: /Course/Delete
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Delete(string siteShortName, Guid id)
        {
            Course course = dataRepository.GetCourseByID(id);
            if (course != null)
            {
                return View(course);
            }
            else
            {
                return View("CourseNotFound");
            }
        }

        //
        // POST: /Course/Delete

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Delete(string siteShortName, Guid id, FormCollection collection)
        {
            Course course = dataRepository.GetCourseByID(id);
            if (course != null)
            {
                try
                {
                    dataRepository.DeleteCourse(course);
                    dataRepository.Save();
                    FlashMessageHelper.AddMessage(course.Name + " has been deleted.");
                }
                catch (Exception ex)
                {
                    FlashMessageHelper.AddMessage("An error occurred: " + ex.Message);
                }

                return RedirectToAction("Index", new { siteShortName = siteShortName });
            }
            else
            {
                return View("CourseNotFound");
            }
        }
    }
}
