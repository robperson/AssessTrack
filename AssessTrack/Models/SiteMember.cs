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
    public partial class SiteMember : IBackupItem
    {
        #region IBackupItem Members

        public Guid objectID
        {
            get;
            set;
        }

        public System.Xml.Linq.XElement Serialize()
        {
            XElement sitemember =
                new XElement("sitemember",
                    new XElement("sitememberid", SiteMemberID.ToString()),
                    new XElement("siteid", SiteID.ToString()),
                    new XElement("membershipid", MembershipID.ToString()),
                    new XElement("accesslevel", AccessLevel.ToString()));
            return sitemember;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                SiteMemberID = new Guid(source.Element("sitememberid").Value);
                SiteID = new Guid(source.Element("siteid").Value);
                MembershipID = new Guid(source.Element("membershipid").Value);
                AccessLevel = byte.Parse(source.Element("accesslevel").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize SiteMember entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.SiteMembers.InsertOnSubmit(this);
        }

        #endregion
    }
}
