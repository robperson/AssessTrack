using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessTrack.Helpers;
using System.Data.Linq;

namespace AssessTrack.Models
{
    [Bind(Include="AssessmentID,StudentID,DueDate")]
    public partial class SubmissionException
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();
            if (DueDate.CompareTo(DateTime.Now) < 1)
            {
                yield return new RuleViolation("DueDate must be in the future!", "DueDate");
            }
            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Delete)
                return;
            if (!IsValid)
            {
                RuleViolation first = GetRuleViolations().First();
                throw new RuleViolationException(first.ErrorMessage);
            }
        }
    }
}
