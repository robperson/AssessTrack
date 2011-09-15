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
using System.Web.Mvc;
using System.Collections.Generic;
using AssessTrack.Helpers;
using AssessTrack.Models.SiteViews;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public List<Site> GetUserSites()
        {
            return GetUserSites(UserHelpers.GetCurrentUserID());
        }

        public List<Site> GetUserSites(Guid userid)
        {
            return (from site in dc.Sites
                   join sitemember in dc.SiteMembers on site.SiteID equals sitemember.SiteID
                   where sitemember.MembershipID == userid
                   select site)
                   .OrderByDescending(s => s.Terms.Max(t => t.StartDate))
                   .ThenBy(s => s.Title)
                   .ToList();
        }

        /// <summary>
        /// Get sites that the user is not enrolled in
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Site> GetUnEnrolledSites(Guid userid)
        {
            List<Site> allSites = GetAllSites().ToList();
            List<Site> unenrolled = new List<Site>();

            foreach (var site in allSites)
            {
                if (!IsSiteMember(site, userid))
                {
                    unenrolled.Add(site);
                }
            }

            return unenrolled;
        }

        public IEnumerable<Site> GetAllSites()
        {
            return dc.Sites
                .OrderByDescending(s => s.Terms.Max(t => t.StartDate))
                   .ThenBy(s => s.Title)
                   .AsEnumerable();
        }

        public bool JoinSite(Site site)
        {
            if (IsSiteMember(site, GetLoggedInProfile()))
                return false;
            SiteMember siteMember = new SiteMember()
            {
                Site = site,
                Profile = GetLoggedInProfile(),
                AccessLevel = 1
            };
            dc.SiteMembers.InsertOnSubmit(siteMember);
            dc.SubmitChanges();
            return true;
        }

        //public SiteMember GetSiteMember
        public bool IsSiteMember(Site site)
        {
            return IsSiteMember(site, GetLoggedInProfile());
        }

        public bool IsSiteMember(Site site, Profile profile)
        {

            return IsSiteMember(site, profile.MembershipID);
        }

        public bool IsSiteMember(Site site, Guid id)
        {
            SiteMember sitemember = (from sm in dc.SiteMembers
                                     where sm.MembershipID == id
                                     && sm.Site == site
                                     select sm).SingleOrDefault();
            return (sitemember != null);
        }

        public void CreateSite(Profile owner, Site site)
        {
            dc.Sites.InsertOnSubmit(site);
            
            dc.SiteMembers.InsertOnSubmit(new SiteMember()
            {
                MembershipID = owner.MembershipID,
                Site = site,
                AccessLevel = 10
            });

            dc.SubmitChanges();
        }

        public Site GetSiteByShortName(string shortName)
        {
            return (from site in dc.Sites where site.ShortName == shortName select site).SingleOrDefault();
        }

        public Site GetSiteByID(Guid id)
        {
            return (from site in dc.Sites where site.SiteID == id select site).SingleOrDefault();
        }

        public void DeleteSite(Site site)
        {
            dc.Courses.DeleteAllOnSubmit(site.Courses);
            dc.Terms.DeleteAllOnSubmit(site.Terms);
            dc.SiteMembers.DeleteAllOnSubmit(site.SiteMembers);
            dc.Invitations.DeleteAllOnSubmit(site.Invitations);
            dc.ProgramOutcomes.DeleteAllOnSubmit(site.ProgramOutcomes);
            foreach (var courseTerm in site.CourseTerms)
            {
                DeleteCourseTerm(courseTerm);
            }
            dc.Sites.DeleteOnSubmit(site);
        }

        // Service methods for Site controller
        public SiteDetailsViewModel GetSiteDetails(Site site)
        {
            SiteDetailsViewModel details = new SiteDetailsViewModel();
            var courses = GetAllCourseTerms(site);
            
            List<CourseTerm> userCTs = new List<CourseTerm>();
            List<CourseTermListItem> items = new List<CourseTermListItem>();
                
            
            foreach (var course in courses)
            {
                CourseTermListItem item = new CourseTermListItem();
                item.CourseTerm = course;
                if (IsCourseTermMember(course))
                {
                    item.StudentNotEnrolled = false;
                    userCTs.Add(course);
                }
                else
                {
                    item.StudentNotEnrolled = true;

                }

                if (course.Term.EndDate >= DateTime.Now)
                {
                    items.Add(item);
                }
            }

            details.UserCourseOfferings = userCTs;
            details.AllCourseOfferings = items;

            details.Site = site;

            return details;

        }
    }
}
