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
        public CourseTermMember GetCourseTermMemberByID(CourseTerm courseTerm, Guid id)
        {
            return (from ctm in courseTerm.CourseTermMembers
                    where ctm.CourseTermMemberID == id
                    select ctm).Single();
        }
    }


}
