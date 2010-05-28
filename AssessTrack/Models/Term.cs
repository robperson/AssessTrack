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

namespace AssessTrack.Models
{
    [Bind(Include="StartDate,EndDate,Name")]
    public partial class Term
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

            if (Regex.IsMatch(Name,"assessmentTypes|terms|Details|Edit|Delete",RegexOptions.IgnoreCase))
                yield return new RuleViolation(@"Term cannot be named """+ Name +"\"", "Name");

            
            if (this.Site != null)
            {
                int nameCount = Site.Terms.Count(t => t.Name == Name);
                if (nameCount > 1)
                {
                    yield return new RuleViolation(@"A Term named """ + Name + "\" already exists for this Site", "Name");
                }
            }

            if (StartDate == null)
                yield return new RuleViolation("Start Date is required", "StartDate");

            if (EndDate == null)
                yield return new RuleViolation("End Date is required", "EndDate");

            if ((EndDate != null && StartDate != null)
                && (EndDate.CompareTo(StartDate) < 0))
            {
                yield return new RuleViolation("End Date must be later than Start Date", "EndDate");
            }

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }
    }
}
