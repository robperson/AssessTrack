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
    [Bind(Include = "FirstName,LastName,SchoolIDNumber")]
    public partial class Profile
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();
            if (String.IsNullOrEmpty(FirstName))
                yield return new RuleViolation("First Name is required", "FirstName");
            if (String.IsNullOrEmpty(LastName))
                yield return new RuleViolation("Last Name is required", "LastName");

            if (FirstName.Length > 50)
                yield return new RuleViolation("First Name cannot be longer than 50 characters", "FirstName");
            if (LastName.Length > 50)
                yield return new RuleViolation("Last Name cannot be longer than 50 characters", "LastName");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }
    }
}
