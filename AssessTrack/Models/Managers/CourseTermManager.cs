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
using System.Collections.Generic;
using System.Transactions;
using AssessTrack.Helpers;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public void CreateCourseTerm(CourseTerm courseTerm)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Guid userid = GetLoggedInProfile().MembershipID;
                dc.CourseTerms.InsertOnSubmit(courseTerm);
                CourseTermMember member = new CourseTermMember() 
                { 
                    CourseTerm = courseTerm, 
                    MembershipID = userid,
                    AccessLevel = 10
                };
                dc.CourseTermMembers.InsertOnSubmit(member);
                dc.SubmitChanges();

                scope.Complete();
            }
        }

        public CourseTerm GetCourseTermByShortName(Site site, string shortName)
        {
            return (from courseterm in site.CourseTerms
                    where courseterm.ShortName == shortName
                    select courseterm).SingleOrDefault();
        }

        public CourseTerm GetCourseTermByID(Site site, Guid id)
        {
            return (from courseterm in site.CourseTerms
                    where courseterm.CourseTermID == id
                    select courseterm).SingleOrDefault();
        }

        public CourseTerm GetCourseTermByID(Guid id)
        {
            return (from courseterm in dc.CourseTerms
                    where courseterm.CourseTermID == id
                    select courseterm).SingleOrDefault();
        }

        public bool IsCourseTermMember(CourseTerm courseTerm, Profile profile)
        {
            CourseTermMember ctMember = (from ctm in dc.CourseTermMembers
                                         where ctm.CourseTerm == courseTerm
                                         && ctm.Profile == profile
                                         select ctm).SingleOrDefault();
            return (ctMember != null);
        }

        public bool JoinCourseTerm(CourseTerm courseTerm)
        {
            Profile profile = GetLoggedInProfile();
            if (IsCourseTermMember(courseTerm, profile))
                return false;
            CourseTermMember member = new CourseTermMember()
            {
                CourseTerm = courseTerm,
                Profile = profile,
                AccessLevel = 1
            };
            dc.CourseTermMembers.InsertOnSubmit(member);
            dc.SubmitChanges();
            return true;
        }

        public List<CourseTerm> GetUserCourseTerms(Site site)
        {
            return (from ct in site.CourseTerms
                    join ctm in dc.CourseTermMembers
                    on ct.CourseTermID equals ctm.CourseTermID
                    where ctm.MembershipID == UserHelpers.GetCurrentUserID()
                    select ct).ToList();
        }

        public List<CourseTerm> GetUserCourseTerms()
        {
            return GetUserCourseTerms(0);
        }
        public List<CourseTerm> GetUserCourseTerms(int minAccessLevel)
        {
            return (from ct in dc.CourseTerms
                    join ctm in dc.CourseTermMembers
                    on ct.CourseTermID equals ctm.CourseTermID
                    where ctm.MembershipID == UserHelpers.GetCurrentUserID()
                    && ctm.AccessLevel >= minAccessLevel
                    select ct).ToList();
        }

        public List<CourseTermMember> GetExcludedUsersInCourseTerm(CourseTerm courseTerm)
        {
            return courseTerm.GetMembers(0, 0);
        }

        public List<CourseTermMember> GetStudentsInCourseTerm(CourseTerm courseTerm)
        {
            return courseTerm.GetMembers(1, 1);
        }

        public List<Profile> GetStudentProfiles(CourseTerm courseTerm)
        {
            return courseTerm.GetMemberProfiles(1, 1);
        }

        public List<CourseTermMember> GetPowerUsersInCourseTerm(CourseTerm courseTerm)
        {
            return courseTerm.GetMembers(2, 5);
        }

        public List<CourseTermMember> GetSuperUsersInCourseTerm(CourseTerm courseTerm)
        {
            return courseTerm.GetMembers(6, 9);
        }

        public List<CourseTermMember> GetOwnersInCourseTerm(CourseTerm courseTerm)
        {
            return courseTerm.GetMembers(10, 10);
        }
    }


}
