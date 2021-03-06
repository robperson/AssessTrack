﻿using System;
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
                if (site == null)
                {
                    return string.Empty;
                }
                string sitelink = HtmlHelper.GenerateRouteLink(html.ViewContext.RequestContext,
                    html.RouteCollection, site.Title, null,
                    new System.Web.Routing.RouteValueDictionary(
                        new { 
                            action = "Details", 
                            controller = "Site",
                            siteShortName = siteShortName
                        }), null);

                if (site != null)
                {
                    string finallink = before + sitelink + after;
                    return finallink;
                        
                }
            }
            return string.Empty;
        }

        public static string SiteLink(this HtmlHelper html, Site site, string before, string after)
        {
            if (site == null)
            {
                return string.Empty;
            }
            string sitelink = HtmlHelper.GenerateRouteLink(html.ViewContext.RequestContext,
                html.RouteCollection, site.Title, null,
                new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        action = "Details",
                        controller = "Site",
                        siteShortName = site.ShortName
                    }), null);

            string finallink = before + sitelink + after;
            return finallink;
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
                        if (ct == null)
                        {
                            return string.Empty;
                        }
                        string courseTermLink = HtmlHelper.GenerateRouteLink(html.ViewContext.RequestContext,
                            html.RouteCollection, ct.Name, null,
                            new System.Web.Routing.RouteValueDictionary(
                                new 
                                { 
                                    action = "Details", 
                                    controller = "CourseTerm",
                                    siteShortName = siteShortName,
                                    courseTermShortName = courseTermShortName
                                }), null);
                        string finallink = before + courseTermLink + after;
                        return finallink;
                    }
                }
            }
            return string.Empty;
        }

        public static string CourseTermLink(this HtmlHelper html, CourseTerm courseTerm, string before, string after)
        {
            Site site = courseTerm.Site;
            string courseTermLink = HtmlHelper.GenerateRouteLink(html.ViewContext.RequestContext,
                html.RouteCollection, courseTerm.Name, null,
                new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        action = "Details",
                        controller = "CourseTerm",
                        siteShortName = site.ShortName,
                        courseTermShortName = courseTerm.ShortName
                    }), null);
            string finallink = before + courseTermLink + after;
            return finallink;
        }

        public static string CurrentSiteShortName(this HtmlHelper html)
        {
            return (string)html.ViewContext.RouteData.Values["siteShortName"] ?? "";
        }

        public static string CurrentCourseTermShortName(this HtmlHelper html)
        {
            return (string)html.ViewContext.RouteData.Values["courseTermShortName"] ?? "";
        }
    }
}
