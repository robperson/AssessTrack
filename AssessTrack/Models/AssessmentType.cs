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
using AssessTrack.Helpers;
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    [Bind(Include="Name,Weight,IsExtraCredit")]
    public partial class AssessmentType: IBackupItem
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

            if (CourseTerm != null)
            {
                int nameCount = CourseTerm.AssessmentTypes.Count(at => at.Name == Name);
                if (nameCount > 1)
                {
                    yield return new RuleViolation(@"An AssessmentType named """ + Name + "\" already exists for this course/term", "Name");
                }
            }

            if (Weight < 0)
                yield return new RuleViolation("Weight cannot be negative", "Weight");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }

        #region IBackupItem Members

        public XElement Serialize()
        {
            XElement assessmentType =
                new XElement("assessmenttype",
                    new XElement("assessmenttypeid", AssessmentTypeID.ToString()),
                    new XElement("name", Name),
                    new XElement("weight", Weight.ToString()),
                    new XElement("isextracredit", IsExtraCredit.ToString()),
                    new XElement("coursetermid", CourseTermID.ToString()));
            return assessmentType;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                AssessmentTypeID = new Guid(source.Element("assessmenttypeid").Value);
                Name = source.Element("name").Value;
                Weight = Convert.ToDouble(source.Element("weight").Value);
                IsExtraCredit = bool.Parse(source.Element("isextracredit").Value);
                CourseTermID = new Guid(source.Element("coursetermid").Value);
            }
            catch
            {
                throw new Exception("Failed to deserialize AssessmentType entity.");
            }
        }

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

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.AssessmentTypes.InsertOnSubmit(this);
        }

        #endregion
    }
}
