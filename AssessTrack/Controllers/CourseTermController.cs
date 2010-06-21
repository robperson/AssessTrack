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
    public class CourseTermViewModel
    {
        public SelectList CourseList;
        public SelectList TermList;
        public CourseTerm CourseTerm;
        
        public CourseTermViewModel(Site site, CourseTerm courseTerm)
        {
            CourseList = new SelectList(site.Courses.ToList(), "CourseID", "Name");
            TermList = new SelectList(site.Terms.ToList(), "TermID", "Name");
            CourseTerm = courseTerm;
        }

        public CourseTermViewModel(Site site, CourseTerm courseTerm, Guid courseid, Guid termid)
        {
            CourseList = new SelectList(site.Courses.ToList(), "CourseID", "Name", courseid);
            TermList = new SelectList(site.Terms.ToList(), "TermID", "Name", termid);
            CourseTerm = courseTerm;
        }
    }

    public class CourseTermJoinModel
    {
        public SelectList CourseTermsList;
        public CourseTermJoinModel(IEnumerable<CourseTerm> courseTerms)
        {
            CourseTermsList = new SelectList(courseTerms, "CourseTermID", "Name");
        }
    }
    public class CourseTermController : ATController
    {
        //
        // GET: /CourseTerm/
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Index(string siteShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                return View(site.CourseTerms.ToList());
            }
            else
            {
                return View("SiteNotFound");
            }
        }

        public ActionResult Students(string siteShortName, string courseTermShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            return View(courseTerm);
        }

        //
        // GET: /CourseTerm/Details/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Details(string siteShortName, string courseTermShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site,courseTermShortName);
                if (courseTerm != null)
                {
                    return View(courseTerm);
                }
                return View("CourseTermNotFound");
            }
            return View("SiteNotFound");

        }

        //
        // GET: /CourseTerm/Create
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Create(string siteShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                CourseTerm courseTerm = new CourseTerm();
                return View(new CourseTermViewModel(site,courseTerm));
            }
            return View("SiteNotFound");
        } 

        //
        // POST: /CourseTerm/Create

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Create(string siteShortName, Guid CourseID, Guid TermID, CourseTerm courseTerm)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        courseTerm.CourseID = CourseID;
                        courseTerm.TermID = TermID;
                        courseTerm.Site = site;
                        dataRepository.CreateCourseTerm(courseTerm);
                        return RedirectToAction("Index", new { siteShortName = siteShortName });
                    }
                    catch (RuleViolationException)
                    {
                        ModelState.AddModelErrors(courseTerm.GetRuleViolations());
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("_FORM", ex.Message);
                    }
                }
                return View(new CourseTermViewModel(site, courseTerm, CourseID, TermID));
            }
            return View("SiteNotFound");
        }

        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Join(string siteShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            return View(new CourseTermJoinModel(site.CourseTerms.AsEnumerable()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Join(string siteShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            try
            {
                CourseTerm courseTerm = dataRepository.GetCourseTermByID(site,id);
                if (courseTerm == null)
                    return View("CourseTermNotFound");
                if (dataRepository.JoinCourseTerm(courseTerm))
                    return RedirectToAction("Index", new { siteShortName = siteShortName });
                else
                    return View("AlreadyCourseTermMember");
                
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
