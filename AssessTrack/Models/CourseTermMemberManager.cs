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
        public CourseTermMember GetCourseTermMemberByID(Guid id)
        {
            return (from ctm in dc.CourseTermMembers
                    where ctm.CourseTermMemberID == id
                    select ctm).SingleOrDefault();
        }

        public CourseTermMember GetCourseTermMemberByMembershipID(CourseTerm ct, Guid id)
        {
            return (from ctm in ct.CourseTermMembers
                    where ctm.MembershipID == id
                    select ctm).SingleOrDefault();
        }

        public bool IsUserCourseTermMember(CourseTerm ct, Guid id)
        {
            CourseTermMember m = GetCourseTermMemberByMembershipID(ct, id);
            return (m != null);            
        }

        public bool IsCurrentUserCourseTermMember(CourseTerm ct)
        {
            try
            {
                return IsUserCourseTermMember(ct, UserHelpers.GetCurrentUserID());
            }
            catch
            {
                return false;
            }
        }
    }


}
