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
    [Bind(Include="Name,Description,ShortName")]
    public partial class Course: IBackupItem
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

            if (Name.Equals("courses", StringComparison.CurrentCultureIgnoreCase))
                yield return new RuleViolation(@"Course cannot be named ""Courses""", "Name");

            if (ShortName.Equals("courses", StringComparison.CurrentCultureIgnoreCase))
                yield return new RuleViolation(@"Course cannot have Short Name ""Courses""", "ShortName");

            if (!Regex.IsMatch(ShortName, @"\A[a-zA-Z0-9_-]+\Z"))
                yield return new RuleViolation("Short Name can only contain letters, numbers, underscores (_) and dashes (-)", "ShortName");
            //TODO update this check to include Site constraint
            //Course nameCheckCourse = dataRepository.GetCourseByName(Name);
            //if (nameCheckCourse != null && nameCheckCourse.CourseID != CourseID)
            //{
            //    yield return new RuleViolation(Name + " already exists", "Name");
            //}

            //Course shortnameCheckCourse = dataRepository.GetCourseByShortName(ShortName);
            //if (shortnameCheckCourse != null && shortnameCheckCourse.CourseID != CourseID)
            //{
            //    yield return new RuleViolation(@"Short Name """ + ShortName + @""" already exists", "ShortName");
            //}

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
            {
                RuleViolation first = GetRuleViolations().First();
                throw new RuleViolationException(first.ErrorMessage);
            }
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
            XElement course =
                new XElement("course",
                    new XElement("courseid", CourseID.ToString()),
                    new XElement("name", Name),
                    new XElement("description", Description),
                    new XElement("shortname", ShortName),
                    new XElement("siteid", SiteID));
            return course;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                CourseID = new Guid(source.Element("courseid").Value);
                Name = source.Element("name").Value;
                Description = source.Element("description").Value;
                ShortName = source.Element("shortname").Value;
                SiteID = new Guid(source.Element("siteid").Value);
            }
            catch
            {
                throw new Exception("Failed to deserialize Course entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.Courses.InsertOnSubmit(this);
        }

        #endregion
    }
}
