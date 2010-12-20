using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Models;

namespace AssessTrack.Helpers
{
    public static class GradeHelpers
    {
        public static string GetFormattedGrade(double grade)
        {
            return string.Format("{0:0.00}% ({1})", grade, GetFinalLetterGrade(grade));
        }        

        public static string GetFinalLetterGrade(double grade)
        {
            if (grade >= 90.0)
            {
                return "A";
            }
            else if (grade < 90.0 && grade >= 80.0)
            {
                return "B";
            }
            else if (grade < 80.0 && grade >= 70.0)
            {
                return "C";
            }
            else if (grade < 70.0 && grade >= 60.0)
            {
                return "D";
            }
            else
            {
                return "F";
            }
        }

        public static string PrintPfme(double grade)
        {
            double pfme = GetPfme(grade);
            return pfme.ToString("0.00");
        }

        public static double GetPfme(double grade)
        {
            if (grade >= 90.0)
            {
                return 5.0;
            }
            else if (grade < 90.0 && grade >= 80.0)
            {
                return 4.0;
            }
            else if (grade < 80.0 && grade >= 70.0)
            {
                return 3.0;
            }
            else if (grade < 70.0 && grade >= 60.0)
            {
                return 2.0;
            }
            else
            {
                return 1.0;
            }
        }
    }
}
