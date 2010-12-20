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
using System.Data.Linq;
using System.Text.RegularExpressions;
using AssessTrack.Helpers;
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    [Bind(Include = "FirstName,LastName,SchoolIDNumber,Major")]
    public partial class Profile : IBackupItem
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();
            if (String.IsNullOrEmpty(FirstName))
                yield return new RuleViolation("First Name is required", "FirstName");
            if (String.IsNullOrEmpty(LastName))
                yield return new RuleViolation("Last Name is required", "LastName");

            if (FirstName.Length > 50)
                yield return new RuleViolation("First Name cannot be longer than 50 characters", "FirstName");
            if (LastName.Length > 50)
                yield return new RuleViolation("Last Name cannot be longer than 50 characters", "LastName");

            if (Major.Length > 100)
                yield return new RuleViolation("Major cannot be longer than 100 characters", "Major");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }

        private string GetEmailAddress()
        {
            MembershipUser member = Membership.GetUser(MembershipID);
            if (member == null)
            {
                return "";
            }
            else
            {
                return member.Email;
            }
        }

        public string EmailAddress
        {
            get { return GetEmailAddress(); }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        #region IBackupItem Members

        private Guid _objectID;
        public Guid objectID
        {
            get
            {
                return _objectID;
            }
            set
            {
                _objectID = value;
            }
        }

        public XElement Serialize()
        {
            XElement profile =
                new XElement("profile",
                    new XElement("membershipid", MembershipID),
                    new XElement("schoolidnumber", SchoolIDNumber),
                    new XElement("firstname", FirstName),
                    new XElement("lastname", LastName),
                    new XElement("accesslevel", AccessLevel.ToString()));
            return profile;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                MembershipID = new Guid(source.Element("membershipid").Value);
                SchoolIDNumber = source.Element("schoolidnumber").Value;
                FirstName = source.Element("firstname").Value;
                LastName = source.Element("lastname").Value;
                AccessLevel = (byte)Int16.Parse(source.Element("accesslevel").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize Profile entity");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.Profiles.InsertOnSubmit(this);
        }


        #endregion
    }
}
