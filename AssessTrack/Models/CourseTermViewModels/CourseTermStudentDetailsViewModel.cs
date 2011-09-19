using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.CourseTermViewModels
{
    public class CourseTermStudentDetailsViewModel
    {
        public string CurrentGrade;
        public List<CourseTermMessage> RecentMessages;
        public List<Assessment> UpcomingAssessments;
        public CourseTerm CourseTerm;
    }
}