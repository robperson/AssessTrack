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
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using AssessTrack.Models;
using System.Web.Mvc;
using AssessTrack.Helpers;

namespace AssessTrack.Filters
{
    public enum AuthScope
    {
        Application,
        Site,
        CourseTerm
    }


    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
        Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ATAuthAttribute : FilterAttribute, IAuthorizationFilter
    {

        private AssessTrackDataRepository data = new AssessTrackDataRepository();
        private int minLevel = 10;
        private int maxLevel = 10;
        private AuthScope scope = AuthScope.Application;

        public int MinLevel
        {
            get
            {
                return minLevel;
            }
            set
            {
                minLevel = value;
            }
        }

        public int MaxLevel
        {
            get
            {
                return maxLevel;
            }
            set
            {
                maxLevel = value;
            }
        }

        public AuthScope AuthScope
        {
            get
            {
                return scope;
            }
            set
            {
                scope = value;
            }
        }


        // This method must be thread-safe since it is called by the thread-safe OnCacheAuthorization() method.
        protected virtual bool AuthorizeCore(HttpContextBase httpContext, Site site, CourseTerm courseTerm)
        {
            return AuthHelper.CheckAuthorization(HttpContext.Current, site, courseTerm, scope, minLevel, maxLevel);
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            string siteShortName;
            Site site = null;
            string courseTermShortName;
            CourseTerm courseTerm = null;
            
            if (scope != AuthScope.Application)
            {
                //Try to get the site by shortName
                if (filterContext.RouteData.Values["siteShortName"] != null)
                {
                    siteShortName = filterContext.RouteData.Values["siteShortName"].ToString();
                    site = data.GetSiteByShortName(siteShortName);
                }
                //if scope is Site, then {id} should refer to SiteID
                else if (scope != AuthScope.CourseTerm && filterContext.RouteData.Values["id"] != null)
                {
                    try
                    {

                        Guid siteID = new Guid(filterContext.RouteData.Values["id"].ToString());
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
                    filterContext.Result = new ViewResult() { ViewName = "SiteNotFound" };
                    return;
                }
                if (scope == AuthScope.CourseTerm)
                {
                    //Try to get the site by shortName
                    if (filterContext.RouteData.Values["courseTermShortName"] != null)
                    {
                        courseTermShortName = filterContext.RouteData.Values["courseTermShortName"].ToString();
                        courseTerm = data.GetCourseTermByShortName(site,courseTermShortName);
                    }
                    //if scope is CourseTerm, then {id} should refer to CourseTermID
                    else if (filterContext.RouteData.Values["id"].ToString() != null)
                    {
                        try
                        {

                            Guid courseTermID = new Guid(filterContext.RouteData.Values["id"].ToString());
                            courseTerm = data.GetCourseTermByID(site,courseTermID);
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
                        filterContext.Result = new ViewResult() { ViewName = "CourseTermNotFound" };
                        return;
                    }
                }
            }
            //Set up is complete, now check if the user is authorized
            if (AuthorizeCore(filterContext.HttpContext, site, courseTerm))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else //Result depends on WHY AuthorizeCore returned false
            {
                IPrincipal user = filterContext.HttpContext.User;
                if (!user.Identity.IsAuthenticated)
                {
                    // not logged in, redirect to login page
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    //not authorized to view resource, redirect to not authorized view
                    filterContext.Result = new ViewResult() { ViewName = "NotAuthorized" };
                }
                
            }
        }

        // This method must be thread-safe since it is called by the caching module.
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            //bool isAuthorized = AuthorizeCore(httpContext);
            //DON'T CACHE ANY PAGES THAT USE ATAuth
            //TODO put some logging here
            return HttpValidationStatus.Invalid;
        }

    }

}
