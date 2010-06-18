using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;

namespace AssessTrack.Controllers
{
    public class CourseTermMemberController : ATController
    {
        //
        // GET: /CourseTermMember/

        public ActionResult Index(string siteShortName, string courseTermShortName)
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
        // GET: /CourseTermMember/Details/5

        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");

            CourseTermMember member = (from ctm in courseTerm.CourseTermMembers
                                       where ctm.CourseTermMemberID == id
                                       select ctm).Single();

            return View(member);
        }

        
        //
        // GET: /CourseTermMember/Edit/5
 
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");

            CourseTermMember member = (from ctm in courseTerm.CourseTermMembers
                                       where ctm.CourseTermMemberID == id
                                       select ctm).Single();
            return View(member);
        }

        //
        // POST: /CourseTermMember/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id, FormCollection collection)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");

            CourseTermMember member = (from ctm in courseTerm.CourseTermMembers
                                       where ctm.CourseTermMemberID == id
                                       select ctm).Single();
            
            try
            {
                UpdateModel(member);
                dataRepository.Save();

                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            catch
            {
                return View(member);
            }
        }
    }
}
