using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Helpers;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace AssessTrack.Models
{
    [Bind(Include="Label,Description")]
    public partial class ProgramOutcome
    {
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (string.IsNullOrEmpty(Label))
            {
                yield return new RuleViolation("You must specify a label.", "Label");
            }
            if (Label.Length > 50)
            {
                yield return new RuleViolation("Label cannot be longer than 50 characters.", "Label");
            }
            if (string.IsNullOrEmpty(Description))
            {
                yield return new RuleViolation("You must specify a Description.", "Description");
            }
            if (Description.Length > 300)
            {
                yield return new RuleViolation("Description cannot be longer than 50 characters.", "Description");
            }

            yield break;
        }

        public bool IsValid 
        {
            get { return GetRuleViolations().Count() == 0; } 
        }

        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            if (!IsValid)
            {
                RuleViolation first = GetRuleViolations().First();
                throw new RuleViolationException(first.ErrorMessage);
            }
        }

       
      
    }
}
