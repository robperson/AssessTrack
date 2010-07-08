using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Filters;
using AssessTrack.Models;

namespace AssessTrack.Controllers
{
    public class PerformanceReportModel
    {
        public List<GradeSection> GradeSections;
        public Profile Profile;
        public double FinalGrade;
        public string FinalLetterGrade;
        public PerformanceReportModel(List<GradeSection> sections, Profile profile)
        {
            GradeSections = sections;
            Profile = profile;
        }

        public Profile NextStudent;
        public Profile PreviousStudent;
    }

    public class StudentsReportModel
    {
        public string CourseTermName;
        public List<CourseTermMember> Students;

        public StudentsReportModel(string name, List<CourseTermMember> students)
        {
            CourseTermName = name;
            Students = students;
        }
    }

    public class ReportsController : ATController
    {
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students(string siteShortName, string courseTermShortName)
        {
            return View(new StudentsReportModel(courseTerm.Name,dataRepository.GetStudentsInCourseTerm(courseTerm)));
        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 0, MaxLevel = 10)]
        public ActionResult StudentPerformance(string courseTermShortName, string siteShortName, Guid id /*ProfileID*/)
        {
            Profile profile = dataRepository.GetProfileByID(id);
            if (profile == null)
                return View("ProfileNotFound");
            List<GradeSection> sections = new List<GradeSection>();
            foreach (AssessmentType type in courseTerm.AssessmentTypes.Where(type => !type.QuestionBank))
            {
                GradeSection section = new GradeSection(type, profile);
                sections.Add(section);
            }
            PerformanceReportModel model = new PerformanceReportModel(sections, profile);

            CourseTermMember member = dataRepository.GetCourseTermMemberByID(courseTerm, id);
            model.FinalGrade = member.GetFinalGrade();
            model.FinalLetterGrade = member.GetFinalLetterGrade();
            List<CourseTermMember> students = dataRepository.GetStudentsInCourseTerm(courseTerm);
            int currentStudentIndex = students.IndexOf(member);
            if (currentStudentIndex > 0)
            {
                model.PreviousStudent = students[currentStudentIndex - 1].Profile;
            }
            else if (currentStudentIndex == 0)
            {
                model.PreviousStudent = students[students.Count - 1].Profile;
            }

            if (currentStudentIndex < students.Count - 1)
            {
                model.NextStudent = students[currentStudentIndex + 1].Profile;
            }
            else if (currentStudentIndex == 0)
            {
                model.NextStudent = students[0].Profile;
            }

            return View(model);

        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 0, MaxLevel = 10)]
        public ActionResult FinalGrades(string courseTermShortName, string siteShortName)
        {
            return View(dataRepository.GetStudentsInCourseTerm(courseTerm));
        }

    }
}
