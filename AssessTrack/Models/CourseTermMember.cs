using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Backup;
using System.Xml.Linq;
using System.Web.Mvc;

namespace AssessTrack.Models
{
    [Bind(Include="AccessLevel")]
    public partial class CourseTermMember : IBackupItem
    {
        #region IBackupItem Members

        public Guid objectID
        {
            get;
            set;
        }

        public System.Xml.Linq.XElement Serialize()
        {
            XElement coursetermmember = new XElement("coursetermmember",
                new XElement("coursetermmemberid", CourseTermMemberID),
                new XElement("coursetermid", CourseTermID),
                new XElement("membershipid", MembershipID.ToString()),
                new XElement("accesslevel", AccessLevel.ToString()));
            return coursetermmember;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                CourseTermMemberID = new Guid(source.Element("coursetermmemberid").Value);
                CourseTermID = new Guid(source.Element("coursetermid").Value);
                MembershipID = new Guid(source.Element("membershipid").Value);
                AccessLevel = byte.Parse(source.Element("accesslevel").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize CourseTermMember entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.CourseTermMembers.InsertOnSubmit(this);
        }

        #endregion
    }
}
