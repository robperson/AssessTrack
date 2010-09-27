using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Backup;
using System.Xml.Linq;
using System.Web.Mvc;
using AssessTrack.Helpers;

namespace AssessTrack.Models
{
    [Bind(Include="AccessLevel,AccessCode")]
    public partial class CourseTermMember : IBackupItem
    {
        public string GetFormattedGrade()
        {
            return GradeHelpers.GetFormattedGrade(GetFinalGrade());
        }

        public string GetFinalLetterGrade()
        {
            return GradeHelpers.GetFinalLetterGrade(GetFinalGrade());
        }

        public double GetFinalGrade()
        {
            if (this.AccessLevel != 1)
            {
                throw new Exception("Only Students have Final Grades.");
            }
            
            double finalgrade = 0.0;
            double coursTermPoints = 0.0;
            double courseTermMaxPoints = 0.0;
            foreach (AssessmentType asmtType in this.CourseTerm.AssessmentTypes.Where(type => !type.QuestionBank))
            {
                
                GradeSection section = new GradeSection(asmtType, this.Profile);
                if (section.Weight > 0)
                {
                    coursTermPoints += ((section.TotalPoints / section.MaxPoints) * asmtType.Weight);
                    courseTermMaxPoints += section.Weight;
                }
            }
            finalgrade = ((coursTermPoints / courseTermMaxPoints) * 100);
            if (finalgrade >= 0)
            {
                return finalgrade;
            }
            return 0.0;
        }

        public string FullName
        {
            get { return UserHelpers.GetFullNameForID(MembershipID); }
        }

        #region IBackupItem Members

        public Guid objectID
        {
            get;
            set;
        }

        public System.Xml.Linq.XElement Serialize()
        {
            XElement coursetermmember = new XElement("coursetermmember",
                new XElement("coursetermmemberid", CourseTermMemberID),
                new XElement("coursetermid", CourseTermID),
                new XElement("membershipid", MembershipID.ToString()),
                new XElement("accesslevel", AccessLevel.ToString()));
            return coursetermmember;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                CourseTermMemberID = new Guid(source.Element("coursetermmemberid").Value);
                CourseTermID = new Guid(source.Element("coursetermid").Value);
                MembershipID = new Guid(source.Element("membershipid").Value);
                AccessLevel = byte.Parse(source.Element("accesslevel").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize CourseTermMember entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.CourseTermMembers.InsertOnSubmit(this);
        }

        #endregion
    }
}
