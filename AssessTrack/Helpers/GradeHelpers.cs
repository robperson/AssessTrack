﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public static string GetPfme(double grade)
        {
            double pfme = (grade / 100) * 5;
            return pfme.ToString("{0:0.00}");
        }
    }
}
