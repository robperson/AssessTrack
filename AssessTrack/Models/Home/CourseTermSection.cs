using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Helpers;

namespace AssessTrack.Models.Home
{
    public class CourseTermSection
    {
        public CourseTerm CourseTerm;
        public string Grade { get; private set; }
        public bool DisplayGrade { get; private set; }

        public CourseTermSection(CourseTerm ct)
        {
            AssessTrackDataRepository repo = new AssessTrackDataRepository();
            CourseTerm = ct;
            CourseTermMember member = repo.GetCourseTermMemberByMembershipID(ct,UserHelpers.GetCurrentUserID());
            DisplayGrade = false;
            if (member.AccessLevel == 1)
            {
                Grade = member.GetFormattedGrade();
            }
            else if (DisplayGrade)
            {
                Grade = GradeHelpers.GetFormattedGrade(ct.GetAverageGrade());
            }
            else
            {
                Grade = "";
            }
        }
    }
}
