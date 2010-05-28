using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessTrack.Models;

namespace AssessTrack.Helpers
{
    public static class NavigationHelper
    {
        public static string CurrentSiteLink(this HtmlHelper html, string before, string after)
        {
            AssessTrackDataRepository data = new AssessTrackDataRepository();
            if (html.ViewContext.RouteData.Values["siteShortName"] != null)
            {
                string siteShortName = html.ViewContext.RouteData.Values["siteShortName"].ToString();
                Site site = data.GetSiteByShortName(siteShortName);
                string sitelink = HtmlHelper.GenerateRouteLink(html.ViewContext.RequestContext,
                    html.RouteCollection, site.Title, null,
                    new System.Web.Routing.RouteValueDictionary(new { action = "Details", controller = "Site" }), null);

                if (site != null)
                {
                    string finallink = before + sitelink + after;
                    return finallink;
                        
                }
            }
            return "";
        }

        public static string CurrentCourseTermLink(this HtmlHelper html, string before, string after)
        {
            AssessTrackDataRepository data = new AssessTrackDataRepository();
            if (html.ViewContext.RouteData.Values["siteShortName"] != null)
            {
                string siteShortName = html.ViewContext.RouteData.Values["siteShortName"].ToString();
                Site site = data.GetSiteByShortName(siteShortName);
                

                if (site != null)
                {
                    if (html.ViewContext.RouteData.Values["courseTermShortName"] != null)
                    {
                        string courseTermShortName = html.ViewContext.RouteData.Values["courseTermShortName"].ToString();
                        CourseTerm ct = data.GetCourseTermByShortName(site,courseTermShortName);
                        string courseTermLink = HtmlHelper.GenerateRouteLink(html.ViewContext.RequestContext,
                            html.RouteCollection, ct.Name, null,
                            new System.Web.Routing.RouteValueDictionary(new { action = "Details", controller = "CourseTerm" }), null);
                        string finallink = before + courseTermLink + after;
                        return finallink;
                    }
                }
            }
            return "";
        }
    }
}
