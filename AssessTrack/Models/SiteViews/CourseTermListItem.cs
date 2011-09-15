using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssessTrack.Models.SiteViews
{
    public class CourseTermListItem
    {
        public CourseTerm CourseTerm { get; set; }
        public bool StudentNotEnrolled { get; set; }
    }
}
