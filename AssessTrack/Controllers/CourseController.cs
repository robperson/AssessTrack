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
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                return View(dataRepository.GetSiteCourses(site));
            }
            else
            {
                return View("SiteNotFound");
            }
        }

        //
        // GET: /Course/Details/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Details(string siteShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
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
            else
            {
                return View("SiteNotFound");
            }
            
        }

        //
        // GET: /Course/Create
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Create(string siteShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                Course course = new Course();
                return View(course);
            }
            else
            {
                return View("SiteNotFound");
            }
            
        } 

        //
        // POST: /Course/Create

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Create(string siteShortName, Course course)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
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
            else
            {
                return View("SiteNotFound");
            }
            
        }

        //
        // GET: /Course/Edit
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Edit(string siteShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
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
            else
            {
                return View("SiteNotFound");
            }
        }

        //
        // POST: /Course/Edit

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Edit(string siteShortName, Guid id, FormCollection collection)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
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
            else
            {
                return View("SiteNotFound");
            }
        }
    }
}
