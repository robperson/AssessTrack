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
    [Bind(Include="Description,Name")]
    public partial class Tag
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
    }
}
