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
        public List<CourseTermMember> Excluded;
        public List<CourseTermMember> Students;
        public List<CourseTermMember> PowerUsers;
        public List<CourseTermMember> SuperUsers;
        public List<CourseTermMember> Owners;

        public CourseTermMemberViewModel(CourseTerm courseTerm, AssessTrackDataRepository repo)
        {
            Excluded = repo.GetExcludedUsersInCourseTerm(courseTerm);
            Students = repo.GetStudentsInCourseTerm(courseTerm);
            PowerUsers = repo.GetPowerUsersInCourseTerm(courseTerm);
            SuperUsers = repo.GetSuperUsersInCourseTerm(courseTerm);
            Owners = repo.GetOwnersInCourseTerm(courseTerm);
        }
    }

    public class CourseTermMemberController : ATController
    {
        //
        // GET: /CourseTermMember/

        public ActionResult Index(string siteShortName, string courseTermShortName)
        {
            return View(new CourseTermMemberViewModel(courseTerm,dataRepository));
        }

        //
        // GET: /CourseTermMember/Details/5

        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(courseTerm, id);

            return View(member);
        }

        
        //
        // GET: /CourseTermMember/Edit/5
 
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(courseTerm, id);
            return View(member);
        }

        //
        // POST: /CourseTermMember/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id, FormCollection collection)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(courseTerm, id);
            
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
