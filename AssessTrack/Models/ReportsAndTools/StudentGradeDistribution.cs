using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.ReportsAndTools
{
    public class StudentGradeDistribution
    {
        public List<DateTime> dates;
        public List<Double> grades;

        public StudentGradeDistribution()
        {
            dates = new List<DateTime>();
            grades = new List<Double>();
        }

        public void AddGrade(Double grade, DateTime date)
        {
            dates.Add(date);
            grades.Add(grade);
        }
    }
}