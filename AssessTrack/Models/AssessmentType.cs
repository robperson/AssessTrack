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

namespace AssessTrack.Models
{
    [Bind(Include="Name,Weight,IsExtraCredit")]
    public partial class AssessmentType
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
    }
}
