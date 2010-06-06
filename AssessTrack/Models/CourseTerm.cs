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
using System.Data.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AssessTrack.Helpers;
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    [Bind(Include = "Name,Information,ShortName")]
    public partial class CourseTerm : IBackupItem
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();

            if (String.IsNullOrEmpty(Name))
                yield return new RuleViolation("Name is required", "Name");

            if (!Regex.IsMatch(ShortName, @"\A[a-zA-Z0-9_-]+\Z"))
                yield return new RuleViolation("Short Name can only contain letters, numbers, underscores (_) and dashes (-)", "ShortName");
            
            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }

        public List<CourseTermMember> GetMembers(int minLevel, int maxLevel)
        {
            return (from ctm in CourseTermMembers 
                    where ctm.AccessLevel >= minLevel 
                        && ctm.AccessLevel <= maxLevel
                        select ctm).ToList();
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
            XElement courseTerm =
                new XElement("courseterm",
                    new XElement("coursetermid", CourseTermID.ToString()),
                    new XElement("name", Name),
                    new XElement("shortname", ShortName),
                    new XElement("information", Information),
                    new XElement("instructor", Instructor.ToString()),
                    new XElement("courseid", CourseID.ToString()),
                    new XElement("termid", TermID.ToString()),
                    new XElement("siteid", SiteID.ToString()));
            return courseTerm;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                CourseTermID = new Guid(source.Element("coursetermid").Value);
                Name = source.Element("name").Value;
                ShortName = source.Element("shortname").Value;
                Information = source.Element("information").Value;
                Instructor = new Guid(source.Element("instructor").Value);
                CourseID = new Guid(source.Element("courseid").Value);
                TermID = new Guid(source.Element("termid").Value);
                SiteID = new Guid(source.Element("siteid").Value);
            }
            catch (Exception)
            {

                throw new Exception("Failed to deserialize CourseTerm entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.CourseTerms.InsertOnSubmit(this);
        }

        #endregion
    }
}
