using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AssessTrack
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.MapRoute(
                "Compiler",
                "Compiler/{action}/{submissionRecordID}",
                new { controller = "Compiler" }
                );
            routes.MapRoute(
                "Reports",
                "{siteShortName}/{courseTermShortName}/Reports/{action}/{id}",
                new { controller = "Reports", action = "Index", id = "" }
                );
            routes.MapRoute(
                "Submissions",
                "{siteShortName}/{courseTermShortName}/Submissions/{action}/{id}",
                new { controller = "SubmissionRecord", action = "Index", id = "" }
                );
            routes.MapRoute(
                "AssesmentTypes",
                "{siteShortName}/{courseTermShortName}/AssessmentTypes/{action}/{id}",
                new { controller = "AssessmentType", action = "Index", id = "" }
                );
            routes.MapRoute(
                "Assesments",
                "{siteShortName}/{courseTermShortName}/Assessments/{action}/{id}",
                new { controller = "Assessment", action = "Index", id = "" }
                );
            routes.MapRoute(
                "Tags",
                "{siteShortName}/{courseTermShortName}/Tags/{action}/{id}",
                new { controller = "Tag", action = "Index", id = "" }
                );
            routes.MapRoute(
                "SiteCourseTerms",
                "{siteShortName}/CourseTerms/{action}/{id}",
                new { controller = "CourseTerm", action = "Index", id = "" },
                new { action = "Create|Index|Join" }
                );
            routes.MapRoute(
                "SiteCourses",
                "{siteShortName}/Courses/{action}/{id}",
                new { controller = "Course", action = "Index", id = "" }
                );
            routes.MapRoute(
                "SiteTerms",
                "{siteShortName}/Terms/{action}/{id}",
                new { controller = "Term", action = "Index", id = "" }
                );
            routes.MapRoute(
                "Accounts",
                "Accounts/{action}",
                new { controller = "Account" }
                );
            routes.MapRoute(
                "Home",
                "{action}",
                new { controller = "Home", action = "Index" },
                new { action = "Index|About|Install" }
                );

            routes.MapRoute(
                "Sites",
                "Sites/{action}/{id}",
                new { controller = "Site", action = "Index", id = "" },
                new { action = "Index|Edit|Delete|Create|Join" }
                );
            routes.MapRoute(
                "SitesShortName",
                "{siteShortName}",
                new { controller = "Site", action = "Details" }
                );
            routes.MapRoute(
                "CourseTermDetails",
                "{siteShortName}/{courseTermShortName}/{action}",
                new { controller = "CourseTerm", action = "Details" },
                new { action = "Details|Students" }
                );
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

        }
    }
}