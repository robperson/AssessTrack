using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.SiteViews
{
    public class SiteDetailsViewModel
    {
        public Site Site { get; set; }
        public IEnumerable<CourseTerm> UserCourseOfferings { get; set; }
        public IEnumerable<CourseTermListItem> AllCourseOfferings { get; set; }
    }
}