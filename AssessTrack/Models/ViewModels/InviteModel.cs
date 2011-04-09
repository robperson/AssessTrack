using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessTrack.Models.ViewModels
{
    [Bind(Include="SiteAccessLevel,CourseTermAccessLevel,Email,CourseTermID")]
    public class InviteModel
    {
        public int SiteAccessLevel
        {
            get;
            set;
        }

        public int? CourseTermAccessLevel
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public Guid? CourseTermID
        {
            get;
            set;
        }

        public SelectList CourseTermList;

        public InviteModel(IEnumerable<CourseTerm> courseTerms)
        {
            SetCourseTermList(courseTerms);
        }

        public void SetCourseTermList(IEnumerable<CourseTerm> courseTerms)
        {
            CourseTermList = new SelectList(courseTerms, "CourseTermID", "Name");
        }

        public InviteModel()
        {
            SiteAccessLevel = -1;
        }
    }
}
