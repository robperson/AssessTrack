﻿using System;
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
                "Site",
                "Sites/{action}/{siteShortName}",
                new { controller = "Site", action = "Details" }
                );
            routes.MapRoute(
                "Compiler",
                "Compiler/{action}/{submissionRecordID}",
                new { controller = "Compiler" }
                );
            routes.MapRoute(
                "SiteCourses",
                "Courses/{action}/{siteShortName}/{id}",
                new { controller = "Course", action = "Index", id = "" }
                );
            routes.MapRoute(
                "SiteTerms",
                "Terms/{action}/{siteShortName}/{id}",
                new { controller = "Term", action = "Index", id = "" }
                );
            routes.MapRoute(
                "SiteMembers",
                "Members/{action}/{siteShortName}/{id}",
                new { controller = "SiteMember", action = "Index", id = "" }
                );
            routes.MapRoute(
                "SiteCourseTerms",
                "CourseOfferings/{controller}/{action}/{siteShortName}/{courseTermShortName}/{id}",
                new { controller = "CourseTerm", action = "Index", courseTermShortName = "", id = "" }
                );
            //routes.MapRoute(
            //    "CourseTerms",
            //    "CourseEnrollments/{controller}/{action}/",
            //    new { controller = "CourseTerm", action = "MyCourseEnrollments", }
            //    );
            
            routes.MapRoute(
                "Default",                                              // Route name
               "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

            routes.MapRoute("Catch All", "{*path}",
                new { controller = "Error", action = "NotFound" });

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            //HibernatingRhinos.Profiler.Appender.LinqToSql.LinqToSqlProfiler.Initialize();
        }
    }
}