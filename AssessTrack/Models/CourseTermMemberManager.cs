using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Transactions;
using AssessTrack.Helpers;
using System.Data.Linq;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public double GetFinalGrade(CourseTermMember member)
        {
            if (member.AccessLevel != 1)
            {
                throw new Exception("Only Students have Final Grades.");
            }

            var loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<AssessmentType>(at => at.Assessments);
            loadOptions.LoadWith<Assessment>(asmt => asmt.SubmissionRecords);
            loadOptions.LoadWith<Assessment>(asmt => asmt.Questions);
            loadOptions.LoadWith<SubmissionRecord>(sr => sr.Responses);
            dc.LoadOptions = loadOptions;
            double finalgrade = 0.0;
            double coursTermPoints = 0.0;
            double courseTermMaxPoints = 0.0;
            var assessmentTypes = from asmntType in dc.AssessmentTypes
                                  where asmntType.CourseTermID == member.CourseTermID
                                  && !asmntType.QuestionBank
                                  select asmntType;
            foreach (AssessmentType asmtType in assessmentTypes)
            {
                double typePoints = 0.0; //total points scored by student
                double typeMaxPoints = 0.0; //maximum points achievable

                foreach (Assessment assessment in asmtType.Assessments)
                {

                    SubmissionRecord record = (from s in assessment.SubmissionRecords
                                               where s.StudentID == member.MembershipID
                                               orderby s.SubmissionDate descending
                                               select s).FirstOrDefault();
                    if (record != null)
                    {
                        typePoints += record.Score;
                    }

                    typeMaxPoints += assessment.Weight;
                }
                if (typeMaxPoints > 0.0)
                {
                    courseTermMaxPoints += asmtType.Weight;
                }
                else if (typePoints > 0.0)
                //if all Assessments are extra credit, 
                //set typeMaxPoints to 1 so we have something to divide by
                {
                    courseTermMaxPoints += asmtType.Weight;
                    typeMaxPoints = 1.0;
                }
                else
                {
                    // no scores to add, so continue
                    continue;
                }

                coursTermPoints += ((typePoints / typeMaxPoints) * asmtType.Weight);

            }
            finalgrade = ((coursTermPoints / courseTermMaxPoints) * 100);
            return finalgrade;
        }

        public CourseTermMember GetCourseTermMemberByID(Guid id)
        {
            return (from ctm in dc.CourseTermMembers
                    where ctm.CourseTermMemberID == id
                    select ctm).SingleOrDefault();
        }

        public CourseTermMember GetCourseTermMemberByMembershipID(CourseTerm ct, Guid id)
        {
            return (from ctm in ct.CourseTermMembers
                    where ctm.MembershipID == id
                    select ctm).SingleOrDefault();
        }

        public bool IsUserCourseTermMember(CourseTerm ct, Guid id)
        {
            CourseTermMember m = GetCourseTermMemberByMembershipID(ct, id);
            return (m != null);            
        }

        public bool IsCurrentUserCourseTermMember(CourseTerm ct)
        {
            try
            {
                return IsUserCourseTermMember(ct, UserHelpers.GetCurrentUserID());
            }
            catch
            {
                return false;
            }
        }
    }


}
