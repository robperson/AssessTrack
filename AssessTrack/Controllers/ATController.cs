using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;

namespace AssessTrack.Controllers
{
    public class ATController : Controller
    {
        protected AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();

        protected Site site = null;
        protected CourseTerm courseTerm = null;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string siteShortName = (string)(filterContext.RouteData.Values["siteShortName"] ?? string.Empty);
            if (!string.IsNullOrEmpty(siteShortName))
            {
                site = dataRepository.GetSiteByShortName(siteShortName);
                if (site == null)
                {
                    filterContext.Result = View("SiteNotFound");
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
            }

        }

    }
}
