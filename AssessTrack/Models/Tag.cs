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
    [Bind(Include="Description,Name")]
    public partial class Tag : IBackupItem
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

            if (Name != null && Name.Length > 50)
                yield return new RuleViolation("Name cannot be longer than 50 characters", "Name");

            if (Description != null && Description.Length > 100)
                yield return new RuleViolation("Description cannot be longer than 100 characters", "Description");

            if (CourseTerm != null)
            {
                int nameCount = CourseTerm.Tags.Count(t => t.Name == Name);
                if (nameCount > 1)
                {
                    yield return new RuleViolation(@"A Tag named """ + Name + "\" already exists for this Course/Term", "Name");
                }
            }

            

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }

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

        public XElement Serialize()
        {
            XElement tag =
                new XElement("tag",
                    new XElement("tagid", TagID.ToString()),
                    new XElement("name", Name),
                    new XElement("description", Description),
                    new XElement("createdby", CreatedBy.ToString()),
                    new XElement("coursetermid", CourseTermID.ToString()));
            return tag;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                TagID = new Guid(source.Element("tagid").Value);
                Name = source.Element("name").Value;
                Description = source.Element("description").Value;
                CreatedBy = new Guid(source.Element("createdby").Value);
                CourseTermID = new Guid(source.Element("coursetermid").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize Tag entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.Tags.InsertOnSubmit(this);
        }

        #endregion
    }
}
