using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Backup;
using System.Xml.Linq;
using System.Web.Security;

namespace AssessTrack.Models
{
    public class MembershipInfo : IBackupItem
    {
        public Guid MembershipID;
        public string Password;
        public string Username;
        public string Email;

        #region IBackupItem Members

        public Guid objectID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Xml.Linq.XElement Serialize()
        {
            XElement membershipInfo =
                new XElement("membershipinfo",
                    new XElement("membershipid", MembershipID.ToString()),
                    new XElement("password", Password),
                    new XElement("username", Username),
                    new XElement("email", Email));
            return membershipInfo;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                MembershipID = new Guid(source.Element("membershipid").Value);
                Password = source.Element("password").Value;
                Username = source.Element("username").Value;
                Email = source.Element("email").Value;
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize MembershipInfo entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            MembershipCreateStatus status;
            Membership.CreateUser(Username, Password, Email, "question", "answer", true, MembershipID, out status);
            if (status == MembershipCreateStatus.DuplicateUserName)
            {
                Username = Username + "-old";
                Membership.CreateUser(Username, Password, Email, "question", "answer", true, MembershipID, out status);
            }
            if (status != MembershipCreateStatus.Success)
                throw new Exception("Failed to create user " + Username + ".");
        }

        #endregion
    }
}
