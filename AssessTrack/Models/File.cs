using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessTrack.Helpers;
using System.Data.Linq;

namespace AssessTrack.Models
{
    [Bind(Include="Title,Description")]
    public partial class File
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (Description != null && Description.Length > 300)
            {
                yield return new RuleViolation("Description cannot be longer than 300 characters.", "Description");
            }

            if (Title != null && Title.Length > 300)
            {
                yield return new RuleViolation("Title cannot be longer than 100 characters.", "Title");
            }

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
    }
}
