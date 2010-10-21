using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models
{
    public class Grade
    {
        public Assessment Assessment;
        public SubmissionRecord SubmissionRecord;
        public bool IsLate;
        public double Points;
        public double Percentage;
        public Profile Student;
        public Grade(Assessment assessment, Profile profile)
        {
            IsLate = false;
            Assessment = assessment;
            Student = profile;
            SubmissionRecord record = (from s in assessment.SubmissionRecords 
                                           where s.StudentID == profile.MembershipID
                                           orderby s.Score descending
                                           select s).FirstOrDefault();
            if (record == null)
            {
                Points = 0;
                Percentage = 0;
                SubmissionRecord = null;
            }
            else
            {
                Points = record.Score;
                Percentage = (record.Score / assessment.Weight) * 100;
                SubmissionRecord = record;
                if (record.SubmissionDate.CompareTo(assessment.DueDate) > 0)
                {
                    IsLate = true;
                }
            }
        }
    }
}
