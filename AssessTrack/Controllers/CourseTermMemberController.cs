using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;

namespace AssessTrack.Controllers
{
    public class CourseTermMemberViewModel
    {
        public List<CourseTermMemberTable> Tables;
        public CourseTerm CourseTerm;

        public CourseTermMemberViewModel(List<CourseTermMemberTable> tables, CourseTerm courseTerm)
        {
            Tables = tables;
            CourseTerm = courseTerm;
        }
    }

    public class CourseTermMemberTable
    {
        public string Caption;
        public List<CourseTermMember> Members;
        public string EmailAllLink;

        public CourseTermMemberTable(string cap, List<CourseTermMember> members)
        {
            Caption = cap;
            Members = members;
            string linkformat = "mailto:{0}";
            string emails = string.Join(",", (from member in members select member.Profile.EmailAddress).ToArray());
            EmailAllLink = string.Format(linkformat, emails);
        }
    }

    public class CourseTermMemberController : ATController
    {
        //
        // GET: /CourseTermMember/

        public ActionResult Index(string siteShortName, string courseTermShortName)
        {
            List<CourseTermMemberTable> Tables = new List<CourseTermMemberTable>();
            Tables.Add(new CourseTermMemberTable("Students", dataRepository.GetStudentsInCourseTerm(courseTerm)));
            Tables.Add(new CourseTermMemberTable("Power Users (TAs)", dataRepository.GetPowerUsersInCourseTerm(courseTerm)));
            Tables.Add(new CourseTermMemberTable("Super Users (Instructors)", dataRepository.GetSuperUsersInCourseTerm(courseTerm)));
            Tables.Add(new CourseTermMemberTable("Owners", dataRepository.GetOwnersInCourseTerm(courseTerm)));
            Tables.Add(new CourseTermMemberTable("Excluded Users", dataRepository.GetExcludedUsersInCourseTerm(courseTerm)));
            return View(new CourseTermMemberViewModel(Tables, courseTerm));
        }

        //
        // GET: /CourseTermMember/Details/5

        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(id);

            return View(member);
        }

        
        //
        // GET: /CourseTermMember/Edit/5
 
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(id);
            return View(member);
        }

        //
        // POST: /CourseTermMember/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id, FormCollection collection)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(id);
            
            try
            {
                UpdateModel(member);
                dataRepository.Save();

                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            catch
            {
                return View(member);
            }
        }
    }
}
