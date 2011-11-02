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
        public double Weight;

        public GradeSection(AssessmentType assessmentType, Profile profile, AssessTrackDataRepository repo,bool includeExtraCredit)
        {
            AssessmentType = assessmentType;
            TotalPoints = MaxPoints = 0;
            Grades = new List<Grade>();
            CourseTermMember member = repo.GetCourseTermMemberByMembershipID(assessmentType.CourseTerm, profile.MembershipID);
            foreach (Assessment assessment in repo.GetAllNonTestBankAssessments(assessmentType.CourseTerm,includeExtraCredit,assessmentType,member))
            {
                Grade grade = new Grade(assessment, profile);
                Grades.Add(grade);
                
                if ((grade.SubmissionRecord != null && grade.SubmissionRecord.GradedBy != null) ||
                    (DateTime.Now.CompareTo(assessment.DueDate) > 0 && grade.SubmissionRecord == null)
                    )
                {
                    MaxPoints += assessment.Weight;
                    TotalPoints += grade.Points;
                }
            }
            //if (TotalPoints > 0 && MaxPoints == 0) //if everything is extra credit
            //{
            //    MaxPoints = 1; //to avoid division by zero
            //}

            if (MaxPoints == 0)
            {
                Percentage = 0;
                Weight = 0;
            }
            else
            {
                Percentage = (TotalPoints / MaxPoints) * 100;
                Weight = assessmentType.Weight;
            }
        }

        public GradeSection(AssessmentType assessmentType, Profile profile, AssessTrackDataRepository repo, bool includeExtraCredit, DateTime date)
        {
            AssessmentType = assessmentType;
            TotalPoints = MaxPoints = 0;
            Grades = new List<Grade>();
            CourseTermMember member = repo.GetCourseTermMemberByMembershipID(assessmentType.CourseTerm, profile.MembershipID);
            List<Assessment> assessments = repo.GetAllNonTestBankAssessments(assessmentType.CourseTerm, includeExtraCredit, assessmentType, member).Where(test => test.DueDate.CompareTo(date) < 0).ToList();
            foreach (Assessment assessment in assessments)
            {
                Grade grade = new Grade(assessment, profile);
                Grades.Add(grade);

                if ((grade.SubmissionRecord != null && grade.SubmissionRecord.GradedBy != null) ||
                    (DateTime.Now.CompareTo(assessment.DueDate) > 0 && grade.SubmissionRecord == null))
                {
                    MaxPoints += assessment.Weight;
                    TotalPoints += grade.Points;
                }
            }
            //if (TotalPoints > 0 && MaxPoints == 0) //if everything is extra credit
            //{
            //    MaxPoints = 1; //to avoid division by zero
            //}

            if (MaxPoints == 0)
            {
                Percentage = 0;
                Weight = 0;
            }
            else
            {
                Percentage = (TotalPoints / MaxPoints) * 100;
                Weight = assessmentType.Weight;
            }
        }
    
    }
}
