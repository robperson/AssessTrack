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

    public class ReportsController : ATController
    {
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 0, MaxLevel = 10)]
        public ActionResult StudentPerformance(string courseTermShortName, string siteShortName, Guid id /*ProfileID*/)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
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

            CourseTermMember member = (from ctm in courseTerm.CourseTermMembers
                                       where ctm.MembershipID == id
                                       select ctm).SingleOrDefault();
            model.FinalGrade = member.GetFinalGrade();
            model.FinalLetterGrade = member.GetFinalLetterGrade();
            List<CourseTermMember> students = courseTerm.GetMembers(1, 1);
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
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");

            
            return View(courseTerm.GetMembers(1,1));

        }

    }
}
