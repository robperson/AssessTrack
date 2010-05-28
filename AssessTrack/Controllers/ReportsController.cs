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
        public PerformanceReportModel(List<GradeSection> sections, Profile profile)
        {
            GradeSections = sections;
            Profile = profile;
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
            foreach (AssessmentType type in courseTerm.AssessmentTypes)
            {
                GradeSection section = new GradeSection(type, profile);
                sections.Add(section);
            }
            PerformanceReportModel model = new PerformanceReportModel(sections, profile);
            return View(model);

        }

    }
}
