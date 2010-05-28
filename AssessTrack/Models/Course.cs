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
using System.Collections.Generic;
using System.Data.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AssessTrack.Helpers;

namespace AssessTrack.Models
{
    [Bind(Include="Name,Description,ShortName")]
    public partial class Course
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

            if (Name.Equals("courses", StringComparison.CurrentCultureIgnoreCase))
                yield return new RuleViolation(@"Course cannot be named ""Courses""", "Name");

            if (ShortName.Equals("courses", StringComparison.CurrentCultureIgnoreCase))
                yield return new RuleViolation(@"Course cannot have Short Name ""Courses""", "ShortName");

            if (!Regex.IsMatch(ShortName, @"\A[a-zA-Z0-9_-]+\Z"))
                yield return new RuleViolation("Short Name can only contain letters, numbers, underscores (_) and dashes (-)", "ShortName");
            //TODO update this check to include Site constraint
            //Course nameCheckCourse = dataRepository.GetCourseByName(Name);
            //if (nameCheckCourse != null && nameCheckCourse.CourseID != CourseID)
            //{
            //    yield return new RuleViolation(Name + " already exists", "Name");
            //}

            //Course shortnameCheckCourse = dataRepository.GetCourseByShortName(ShortName);
            //if (shortnameCheckCourse != null && shortnameCheckCourse.CourseID != CourseID)
            //{
            //    yield return new RuleViolation(@"Short Name """ + ShortName + @""" already exists", "ShortName");
            //}

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }
    }
}
