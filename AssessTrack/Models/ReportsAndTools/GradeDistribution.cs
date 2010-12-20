using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Helpers;

namespace AssessTrack.Models.ReportsAndTools
{
    public class GradeDistribution
    {
        public int ACount;
        public int BCount;
        public int CCount;
        public int DCount;
        public int FCount;
        public int UngradedCount;
        public int TotalCount;

        public List<Profile> AStudents = new List<Profile>();
        public List<Profile> BStudents = new List<Profile>();
        public List<Profile> CStudents = new List<Profile>();
        public List<Profile> DStudents = new List<Profile>();
        public List<Profile> FStudents = new List<Profile>();
        public List<Profile> UngradedStudents = new List<Profile>();

        public GradeDistribution()
        {
            ACount = 0;
            BCount = 0;
            CCount = 0;
            DCount = 0;
            FCount = 0;
            UngradedCount = 0;
            TotalCount = 0;
        }

        public void AddGrade(string LetterGrade, Profile p)
        {
            if (LetterGrade == "A")
            {
                ACount++;
                if (p != null)
                {
                    AStudents.Add(p);
                }
            }
            else if (LetterGrade == "B")
            {
                BCount++;
                if (p != null)
                {
                    BStudents.Add(p);
                }
            }
            else if (LetterGrade == "C")
            {
                CCount++;
                if (p != null)
                {
                    CStudents.Add(p);
                }
            }
            else if (LetterGrade == "D")
            {
                DCount++;
                if (p != null)
                {
                    DStudents.Add(p);
                }
            }
            else if (LetterGrade == "F")
            {
                FCount++;
                if (p != null)
                {
                    FStudents.Add(p);
                }
            }
            else
            {
                UngradedCount++;
                if (p != null)
                {
                    UngradedStudents.Add(p);
                }
            }
            TotalCount++;
        }

        public void AddGrade(string LetterGrade)
        {
            AddGrade(LetterGrade, null);
        }

        public void AddGrade(double? Grade)
        {
            
            if (Grade.HasValue)
                AddGrade(GradeHelpers.GetFinalLetterGrade(Grade.Value));
            else
                AddGrade(string.Empty);
        }

        public void AddGrade(double? Grade, Profile p)
        {

            if (Grade.HasValue)
                AddGrade(GradeHelpers.GetFinalLetterGrade(Grade.Value),p);
            else
                AddGrade(string.Empty,p);
        }
    }
}
