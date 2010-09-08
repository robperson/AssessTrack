using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models
{
    public class GradeSection
    {
        public AssessmentType AssessmentType;
        public double TotalPoints;
        public double MaxPoints;
        public double Percentage;
        public List<Grade> Grades;

        public GradeSection(AssessmentType assessmentType, Profile profile)
        {
            AssessmentType = assessmentType;
            TotalPoints = MaxPoints = 0;
            Grades = new List<Grade>();
            foreach (Assessment assessment in assessmentType.Assessments.Where(assmnt => assmnt.IsVisible).OrderBy(atype => atype.DueDate))
            {
                Grade grade = new Grade(assessment, profile);
                Grades.Add(grade);
                TotalPoints += grade.Points;
                if (grade.SubmissionRecord != null)
                {
                    MaxPoints += assessment.Weight;
                }
            }
            Percentage = (TotalPoints / MaxPoints) * 100;
        }
    }
}
