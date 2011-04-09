using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Helpers;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AssessTrack.Models
{
    public partial class Invitation
    {
        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (string.IsNullOrEmpty(Email))
            {
                yield return new RuleViolation("You must specify an email.", "Email");
            }
            if (SiteAccessLevel > 10 || SiteAccessLevel < 0)
            {
                yield return new RuleViolation("Site access level must be between 0 and 10.", "SiteAccessLevel");
            }
            
            if (CourseTermID.HasValue && !CourseTermAccessLevel.HasValue)
            {
                yield return new RuleViolation("Course Term Access Level is required when Course Term is selected.", "CourseTermAccessLevel");
            }
            else if (CourseTermID.HasValue && (CourseTermAccessLevel.Value > 10 || CourseTermAccessLevel.Value < 0))
            {
                yield return new RuleViolation("Course Term Access Level must be between 0 and 10.", "CourseTermAccessLevel");
            }
            if (!Regex.IsMatch(Email,@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",RegexOptions.IgnoreCase))
            {
                yield return new RuleViolation("Email address is not valid.", "Email");
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
