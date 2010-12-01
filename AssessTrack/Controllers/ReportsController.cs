using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Filters;
using AssessTrack.Models;
using AssessTrack.Helpers;
using AssessTrack.Models.ReportsAndTools;

namespace AssessTrack.Controllers
{
    public class PerformanceReportModel
    {
        public List<GradeSection> GradeSections;
        public Profile Profile;
        public double FinalGrade;
        public string FinalLetterGrade;
        public double TotalWeight;
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

    public class AssessmentGradeDistributionModel
    {
        public Assessment Assessment;
        public GradeDistribution GradeDistribution;
    }

    [Authorize()]
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

        public ActionResult GradeSheetOverview()
        {
            List<Assessment> assessments = dataRepository.GetAllNonTestBankAssessments(courseTerm,false);
            List<Profile> profiles = dataRepository.GetStudentProfiles(courseTerm);

             
            Func<Assessment, string> ylabel = (a => a.Name);
            Func<Assessment, string> ydetail = (a => dataRepository.GetWeightedPointValue(a).ToString("0.00"));
            Func<Assessment, double> yval = a => dataRepository.GetWeightedPointValue(a);
            Func<Profile,string> xlabel = p => p.FullName;
            Func<Profile,Assessment,double> cellval = (p,a) => 
                {
                    Grade g = new Grade(a,p);
                    double wv = dataRepository.GetWeightedPointValue(a);
                    return (a.IsExtraCredit)? g.Points : (g.Percentage/100.0) * wv;
                };
            bool showcoltotals = true;
            bool showcolavgs = false;
            bool showcolpfmes = false;
            bool showcolgrade = true;
            bool showrowavgs = true;

            GridReport<Profile, Assessment> report = new GridReport<Profile, Assessment>(profiles,
                assessments,
                ylabel, ydetail, yval,
                xlabel, cellval,
                showcoltotals, showcolavgs, showcolpfmes, showcolgrade, showrowavgs);

            return View(report);

        }

        public ActionResult CourseOutcomeSummary()
        {
            List<Tag> tags = dataRepository.GetCourseOutcomes(courseTerm,false);
            List<Profile> profiles = dataRepository.GetStudentProfiles(courseTerm);


            
            Func<Tag, string> ylabel = tag =>
                {
                    string tagname = string.IsNullOrEmpty(tag.DescriptiveName) ? tag.Name : tag.DescriptiveName;
                    string url = Url.Action("CourseOutcomeDetails", "Reports", new { id = tag.TagID, siteShortName = site.ShortName, courseTermShortName = courseTerm.ShortName });
                    return string.Format(@"<a href=""{0}"" title=""Click to view Course Outcome Details Report"">{1}</a>",url,tagname);
                };
            
            
            Func<Profile, string> xlabel = p => p.FullName;
            Func<Profile, Tag, double> cellval = (p, t) =>
            {
                
                return dataRepository.GetPfmeForTag(t,p);
            };
            bool showcoltotals = false;
            bool showcolavgs = true;
            bool showcolpfmes = false;
            bool showcolgrade = false;
            bool showrowavgs = true;

            IGridReport report = new GridReport<Profile, Tag>(profiles,
                tags,
                ylabel, null, null,
                xlabel, cellval,
                showcoltotals, showcolavgs, showcolpfmes, showcolgrade,showrowavgs);

            return View(report);

        }

        public ActionResult CourseOutcomeDetails(Guid id)
        {
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            List<ITaggable> taggeditems = dataRepository.GetTaggedItems(tag);
            List<Profile> profiles = dataRepository.GetStudentProfiles(courseTerm);


            Func<ITaggable, string> ylabel = (tagged => tagged.Name);
            Func<ITaggable, double> yval = (tagged => tagged.Weight);
            Func<ITaggable, string> ydetail = (tagged => tagged.Weight.ToString("0.00"));


            Func<Profile, string> xlabel = p => p.FullName;
            Func<Profile, ITaggable, double> cellval = (p, t) =>
            {

                return t.Score(p);
            };
            bool showcoltotals = true;
            bool showcolavgs = false;
            bool showcolpfmes = true;
            bool showcolgrade = false;
            bool showrowavgs = true;

            IGridReport report = new GridReport<Profile, ITaggable>(profiles,
                taggeditems,
                ylabel, ydetail, yval,
                xlabel, cellval,
                showcoltotals, showcolavgs, showcolpfmes, showcolgrade, showrowavgs);

            CourseOutcomeDetailsModel model = new CourseOutcomeDetailsModel() { CourseOutcome = tag, Report = report };
            return View(model);

        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 0, MaxLevel = 10)]
        public ActionResult StudentPerformance(string courseTermShortName, string siteShortName, Guid id /*ProfileID*/)
        {
            Profile profile = dataRepository.GetProfileByID(id);
            double totalWeight = 0;
            if (profile == null)
                return View("ProfileNotFound");
            if (!AuthHelper.IsCurrentStudentOrUserIsAdmin(courseTerm, id))
            {
                return View("NotAuthorized");
            }
            List<GradeSection> sections = new List<GradeSection>();
            foreach (AssessmentType type in dataRepository.GetNonTestBankAssessmentTypes(courseTerm))
            {
                GradeSection section = new GradeSection(type, profile,dataRepository,true);
                totalWeight += section.Weight;
                sections.Add(section);
                
            }
            PerformanceReportModel model = new PerformanceReportModel(sections, profile);
            model.TotalWeight = totalWeight;
            CourseTermMember member = dataRepository.GetCourseTermMemberByMembershipID(courseTerm, id);
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
            else if (currentStudentIndex == students.Count - 1)
            {
                model.NextStudent = students[0].Profile;
            }

            return View(model);

        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 5, MaxLevel = 10)]
        public ActionResult FinalGrades(string courseTermShortName, string siteShortName)
        {
            return View(dataRepository.GetStudentsInCourseTerm(courseTerm));
        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 5, MaxLevel = 10)]
        public ActionResult StrugglingStudents(string courseTermShortName, string siteShortName)
        {
            List<CourseTermMember> students = dataRepository.GetStudentsInCourseTerm(courseTerm);
            List<CourseTermMember> strugglingStudents = (from student in students
                                                         where student.GetFinalGrade() < 70
                                                         select student).ToList();

            return View(strugglingStudents);
        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 5, MaxLevel = 10)]
        public ActionResult ClassGradeDistribution()
        {
            List<CourseTermMember> students = dataRepository.GetStudentsInCourseTerm(courseTerm);
            GradeDistribution dist = new GradeDistribution();
            foreach (var student in students)
            {
                dist.AddGrade(student.GetFinalGrade(),student.Profile);
            }

            return View(dist);
        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 5, MaxLevel = 10)]
        public ActionResult AssessmentGradeDistribution(Guid? id)
        {
            List<CourseTermMember> students = dataRepository.GetStudentsInCourseTerm(courseTerm);
            if (id == null)
            {
                return View("AssessmentNotFound");
            }
            Assessment a = dataRepository.GetAssessmentByID(courseTerm, id.Value);
            GradeDistribution dist = new GradeDistribution();
            foreach (var student in students)
            {
                Grade g = new Grade(a, student.Profile);
                if (g.SubmissionRecord != null)
                {
                    dist.AddGrade(g.Percentage,student.Profile);
                }
                else
                {
                    dist.AddGrade(new Nullable<double>(),student.Profile);
                }
            }
            AssessmentGradeDistributionModel model = new AssessmentGradeDistributionModel();
            model.GradeDistribution = dist;
            model.Assessment = a;
            return View(model);
        }

    }
}
