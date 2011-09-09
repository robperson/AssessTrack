using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AssessTrack.Models;
using System.Security.Principal;
using AssessTrack.Filters;
using System.Web.Routing;
using System.Web.Mvc;
using System.Reflection;
using System.Text;
using System.IO;
using System.Web.Mvc.Html;

namespace AssessTrack.Helpers
{
    public static class AuthHelper
    {
        public static bool IsCurrentStudentOrUserIsAdmin(CourseTerm ct, Guid requestedID)
        {
            try
            {
                AssessTrackDataRepository repo = new AssessTrackDataRepository();

                CourseTermMember member = repo.GetCourseTermMemberByMembershipID(ct, UserHelpers.GetCurrentUserID());

                if (member == null)
                {
                    return false;
                }
                else if (member.AccessLevel < 2 && member.MembershipID != requestedID)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckAuthorization(HttpContext httpContext, Site site, CourseTerm courseTerm, AuthScope scope, int minLevel, int maxLevel)
        {
            AssessTrackDataRepository data = new AssessTrackDataRepository();
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }
            //Get the user's profile and see if they have
            //the required access level
            Profile profile = data.GetLoggedInProfile();
            
            // Setting a profile to Access Level 0 essentially disables their account
            if (profile.AccessLevel == 0)
                return false;

            SiteMember smember = null;
            if (scope == AuthScope.Site || scope == AuthScope.CourseTerm)
                smember = data.GetSiteMemberByMembershipID(site, profile.MembershipID);
            
            
            CourseTermMember cmember = null;
            if (scope == AuthScope.CourseTerm)
                cmember = data.GetCourseTermMemberByMembershipID(courseTerm, profile.MembershipID);

            switch (scope)
            {
                case AuthScope.Application:
                    {
                        if (profile.AccessLevel < minLevel || profile.AccessLevel > maxLevel)
                            return false;
                        break;
                    }
                case AuthScope.Site:
                    {
                        
                        if (smember == null ||
                            (smember.AccessLevel < minLevel || smember.AccessLevel > maxLevel))
                            return false;
                    }
                    break;
                case AuthScope.CourseTerm:
                    {
                        if (cmember == null ||
                            (cmember.AccessLevel < minLevel || cmember.AccessLevel > maxLevel) ||
                            smember.AccessLevel == 0) // If site access level is 0, user can't access dependent course offering info
                            return false;
                    }
                    break;
                default:
                    //TODO Do some logging here maybe?
                    return false;
            }
            return true;
        }

        //Will return false if routeData points to non-existant site or courseterm
        public static bool CheckAuthorization(AuthScope scope, int minLevel, int maxLevel, RouteValueDictionary routeData)
        {
            //RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            AssessTrackDataRepository data = new AssessTrackDataRepository();
            string siteShortName;
            Site site = null;
            string courseTermShortName;
            CourseTerm courseTerm = null;
            //HttpContext.Current.

            if (scope != AuthScope.Application)
            {
                //Try to get the site by shortName
                if (routeData["siteShortName"] != null)
                {
                    siteShortName = routeData["siteShortName"].ToString();
                    site = data.GetSiteByShortName(siteShortName);
                }
                //if scope is Site, then {id} should refer to SiteID
                else if (scope != AuthScope.CourseTerm && routeData["id"] != null)
                {
                    try
                    {

                        Guid siteID = new Guid(routeData["id"].ToString());
                        site = data.GetSiteByID(siteID);
                    }
                    catch
                    {
                        //Do nothing here
                        //if this fails, site will be null and the following code will 
                        //return SiteNotFound
                    }
                }


                if (site == null)
                {
                    return false;
                }
                if (scope == AuthScope.CourseTerm)
                {
                    //Try to get the site by shortName
                    if (routeData["courseTermShortName"] != null)
                    {
                        courseTermShortName = routeData["courseTermShortName"].ToString();
                        courseTerm = data.GetCourseTermByShortName(site, courseTermShortName);
                    }
                    //if scope is CourseTerm, then {id} should refer to CourseTermID
                    else if (routeData["id"].ToString() != null)
                    {
                        try
                        {

                            Guid courseTermID = new Guid(routeData["id"].ToString());
                            courseTerm = data.GetCourseTermByID(site, courseTermID);
                        }
                        catch
                        {
                            //Do nothing here
                            //if this fails, courseTerm will be null and the following code will 
                            //return CourseTermNotFound
                        }
                    }
                    if (courseTerm == null)
                    {
                        
                        return false;
                    }
                }
            }
            //Set up is complete, now check if the user is authorized
            if (CheckAuthorization(HttpContext.Current, site, courseTerm, scope, minLevel, maxLevel))
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public static string ATAuthLink(this HtmlHelper html, string linkText, object routeValues, AuthScope scope, int minLevel, int maxLevel)
        {
            return ATAuthLink(html, linkText, string.Empty, string.Empty, routeValues, scope, minLevel, maxLevel);
        }

        public static string ATAuthLink(this HtmlHelper html, string linkText, string before, string after, object routeValues, AuthScope scope, int minLevel, int maxLevel)
        {
            RouteValueDictionary routeValuesDict = new RouteValueDictionary(html.ViewContext.RouteData.Values);
            //routeValuesDict

            foreach (PropertyInfo prop in routeValues.GetType().GetProperties())
            {
                string name = prop.Name;
                string val = prop.GetValue(routeValues, null).ToString();
                routeValuesDict[name] = val;
                //html.ViewContext.RouteData.Values.Add(name,val);
            }
            if (CheckAuthorization(scope, minLevel, maxLevel, routeValuesDict))
            {
                RouteValueDictionary newValues = new RouteValueDictionary(routeValues);
                if (newValues["controller"] == null)
                {
                    newValues["controller"] = routeValuesDict["controller"];
                }
                return before + HtmlHelper.GenerateRouteLink(html.ViewContext.RequestContext, html.RouteCollection, linkText, null, newValues, null) + after;
            }
            return "";
        }

        public static string RenderPartialToString(this HtmlHelper html, string controlName, object model)
        {
            ViewDataDictionary vd = new ViewDataDictionary(html.ViewData);
            vd.Model = model;
            ViewPage vp = new ViewPage { ViewData = vd, ViewContext = html.ViewContext };
            Control control = vp.LoadControl(controlName);
            
            vp.Controls.Add(control);

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    vp.RenderControl(tw);
                }
            }

            return sb.ToString();
        }

        public static void ATAuthPartial(this HtmlHelper html, AuthScope scope, int minlevel, int maxlevel, string partialName)
        {
            if (CheckAuthorization(scope, minlevel, maxlevel, html.ViewContext.RouteData.Values))
            {
                html.RenderPartial(partialName);
            }
        }

        public static bool CheckAuthorization(this HtmlHelper html, int minlevel, int maxlevel, AuthScope scope)
        {
            return CheckAuthorization(scope, minlevel, maxlevel, html.ViewContext.RouteData.Values);
        }
    }
}
