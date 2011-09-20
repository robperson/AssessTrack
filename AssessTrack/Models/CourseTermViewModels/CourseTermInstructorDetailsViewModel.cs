using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Models.ReportsAndTools;

namespace AssessTrack.Models.CourseTermViewModels
{
    public class CourseTermInstructorDetailsViewModel
    {
        public CourseTerm CourseTerm;
        public GradeDistribution GradeDistribution;
        public List<Assessment> UngradedAssessments;
    }
}