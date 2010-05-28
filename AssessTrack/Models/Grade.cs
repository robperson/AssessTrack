using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models
{
    public class Grade
    {
        public Assessment Assessment;
        public double Points;
        public double Percentage;

        public Grade(Assessment assessment, Profile profile)
        {
            Assessment = assessment;
            SubmissionRecord record = (from s in assessment.SubmissionRecords 
                                           where s.StudentID == profile.MembershipID
                                           orderby s.SubmissionDate descending
                                           select s).FirstOrDefault();
            if (record == null)
            {
                Points = 0;
                Percentage = 0;
            }
            else
            {
                Points = record.Score;
                Percentage = (record.Score / assessment.Weight) * 100;
            }
        }
    }
}
