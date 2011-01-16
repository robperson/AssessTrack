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
    [ValidateInput(false)]
    public class ATController : Controller
    {
        protected AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();

        protected Site site = null;
        protected CourseTerm courseTerm = null;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["showSiteMenu"] = false;
            ViewData["showCourseTermMenu"] = false;
            ViewData["RecentMessages"] = (Request.IsAuthenticated) ? dataRepository.GetRecentCourseTermMessages() : null;
            if (Request.IsAuthenticated)
            {
                List<Assessment> upcomingAssessments = dataRepository.GetUpcomingUnsubmittedAssessments();
                ViewData["UpcomingAssessments"] = (upcomingAssessments.Count > 0) ? upcomingAssessments : null;
            }
            ViewData["UserFullName"] = (Request.IsAuthenticated) ? UserHelpers.GetFullNameForCurrentUser() : "Guest";
            base.OnActionExecuting(filterContext);
            
            string siteShortName = (string)(filterContext.RouteData.Values["siteShortName"] ?? string.Empty);
            if (!string.IsNullOrEmpty(siteShortName))
            {
                site = dataRepository.GetSiteByShortName(siteShortName);
                if (site == null)
                {
                    filterContext.Result = View("SiteNotFound");
                    
                }
                else
                {
                    ViewData["showSiteMenu"] = true;
                }
            }

            string courseTermShortName = (string)(filterContext.RouteData.Values["courseTermShortName"] ?? string.Empty);
            if (!string.IsNullOrEmpty(courseTermShortName))
            {
                courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
                if (courseTerm == null)
                {
                    filterContext.Result = View("CourseTermNotFound");
                    
                }
                else
                {
                    if (courseTerm.Term.EndDate.CompareTo(DateTime.Now) < 0) //This course has ended
                    {
                        CourseTermMember member = dataRepository.GetCourseTermMemberByMembershipID(courseTerm, UserHelpers.GetCurrentUserID());
                        if (member != null && member.AccessLevel < 6) //Only admins can view course info after a semester has ended
                        {
                            filterContext.Result = View("SemesterEnded");
                        }
                    }
                    ViewData["showCourseTermMenu"] = true;
                }
            }

        }

    }
}
