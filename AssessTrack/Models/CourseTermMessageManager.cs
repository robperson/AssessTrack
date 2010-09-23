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
        public CourseTermMessage GetCourseTermMessageByID(Guid id)
        {
            return (from message in dc.CourseTermMessages
                    where message.MessageID == id
                    select message).SingleOrDefault();
        }

        public List<CourseTermMessage> GetRecentCourseTermMessages()
        {
            var messages = from message in dc.CourseTermMessages
                           from member in dc.CourseTermMembers
                           where member.MembershipID == UserHelpers.GetCurrentUserID()
                           && member.CourseTermID == message.CourseTermID
                           select message;
            return messages.OrderByDescending(msg => msg.CreatedDate).Take(10).ToList();
        }
    }


}
