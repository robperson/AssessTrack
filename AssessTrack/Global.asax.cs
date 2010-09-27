using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AssessTrack.Helpers;
using System.Net.Mail;
using System.Configuration;
using System.Text;

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
            routes.IgnoreRoute("{file}.txt");


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

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                string username = ConfigurationManager.AppSettings["ErrorLoggerUsername"];
                string password = ConfigurationManager.AppSettings["ErrorLoggerPassword"];

                StringBuilder requestinfoBuilder = new StringBuilder();
                requestinfoBuilder.AppendFormat("<p>Requested Url: {0}</p>\n", Request.Url.AbsoluteUri);
                requestinfoBuilder.AppendFormat("<p>Referrer: {0}</p>\n", (Request.UrlReferrer != null)? Request.UrlReferrer.AbsoluteUri : "");
                requestinfoBuilder.AppendFormat("<p>User IP: {0}</p>\n", Request.UserHostAddress);
                if (ex.InnerException != null)
                {
                    requestinfoBuilder.AppendFormat("<p>Inner Exception: {0} - {1}</p>", ex.InnerException, ex.InnerException.Message);
                }
                message.Body = string.Format("<strong>Exception type</strong><p>{3}</p><strong>Request Info</strong>{2}<strong>Message:</strong>\n\n<p>{0}</p>\n\n<strong>Stack trace:</strong>\n\n <pre>{1}</pre>", Server.HtmlEncode(ex.Message), Server.HtmlEncode(ex.StackTrace),requestinfoBuilder.ToString(),ex.GetType().Name);

                message.From = new MailAddress(username);
                message.To.Add(new MailAddress(username));
                EmailHelper.SendEmail(username, password, message);
            }
            catch
            {
                //Do nothing
            }
            //Server.ClearError();
        }
    }
}