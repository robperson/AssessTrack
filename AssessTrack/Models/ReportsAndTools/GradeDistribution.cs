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

        public void AddGrade(string LetterGrade)
        {
            if (LetterGrade == "A")
            {
                ACount++;
            }
            else if (LetterGrade == "B")
            {
                BCount++;
            }
            else if (LetterGrade == "C")
            {
                CCount++;
            }
            else if (LetterGrade == "D")
            {
                DCount++;
            }
            else if (LetterGrade == "F")
            {
                FCount++;
            }
            else
            {
                UngradedCount++;
            }
            TotalCount++;
        }

        public void AddGrade(double? Grade)
        {
            
            if (Grade.HasValue)
                AddGrade(GradeHelpers.GetFinalLetterGrade(Grade.Value));
            else
                AddGrade(string.Empty);
        }
    }
}
