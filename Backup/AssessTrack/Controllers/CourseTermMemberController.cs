using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Filters;
using AssessTrack.Helpers;

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
        public bool ShowDetails;

        public CourseTermMemberTable(string cap, List<CourseTermMember> members, bool details)
        {
            Caption = cap;
            Members = members;
            string linkformat = "mailto:{0}";
            string emails = string.Join(",", (from member in members select member.Profile.EmailAddress).ToArray());
            EmailAllLink = string.Format(linkformat, emails);
            ShowDetails = details;
        }
    }

    [ATAuth(AuthScope=AuthScope.CourseTerm,MaxLevel=10,MinLevel=5)]
    public class CourseTermMemberController : ATController
    {
        //
        // GET: /CourseTermMember/

        public ActionResult Index(string siteShortName, string courseTermShortName, bool? details)
        {
            List<CourseTermMemberTable> Tables = new List<CourseTermMemberTable>();
            Tables.Add(new CourseTermMemberTable("Students", dataRepository.GetStudentsInCourseTerm(courseTerm), details ?? false));
            Tables.Add(new CourseTermMemberTable("Power Users (TAs)", dataRepository.GetPowerUsersInCourseTerm(courseTerm), details ?? false));
            Tables.Add(new CourseTermMemberTable("Super Users (Instructors)", dataRepository.GetSuperUsersInCourseTerm(courseTerm), details ?? false));
            Tables.Add(new CourseTermMemberTable("Owners", dataRepository.GetOwnersInCourseTerm(courseTerm), false));
            Tables.Add(new CourseTermMemberTable("Excluded Users", dataRepository.GetExcludedUsersInCourseTerm(courseTerm), false));

            return View(new CourseTermMemberViewModel(Tables, courseTerm));
        }

        //
        // GET: /CourseTermMember/Details/5

        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(id);
            CourseTermMember curr = dataRepository.GetCourseTermMemberByMembershipID(courseTerm, UserHelpers.GetCurrentUserID());
            if (curr.AccessLevel <= member.AccessLevel && curr.MembershipID != member.MembershipID) //Can only view details people whose access level is lower than yours
            {
                FlashMessageHelper.AddMessage("You can only view details of your subordinates!");
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            return View(member);
        }

        
        //
        // GET: /CourseTermMember/Edit/5
 
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(id);
            CourseTermMember curr = dataRepository.GetCourseTermMemberByMembershipID(courseTerm, UserHelpers.GetCurrentUserID());
            if (curr.AccessLevel <= member.AccessLevel && curr.MembershipID != member.MembershipID) //Can only modify people whose access level is lower than yours
            {
                FlashMessageHelper.AddMessage("You can only modify your subordinates!");
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            if (curr.AccessLevel < 6 && member.AccessLevel > 1) //TAs can only modify students
            {
                FlashMessageHelper.AddMessage("You can only edit students!");
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            if (curr.AccessLevel < 6 && curr.MembershipID == member.MembershipID) //Can't edit yourself unless you're an instructor
            {
                FlashMessageHelper.AddMessage("You can't edit yourself!");
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }

            return View(member);
        }

        public ActionResult Unlock(string siteShortName, string courseTermShortName, Guid id)
        {
            if (UserHelpers.UnlockAccount(id))
            {
                FlashMessageHelper.AddMessage("Account Unlocked.");
            }
            else
            {
                FlashMessageHelper.AddMessage("Failed to unlock account.");
            }
            return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
        }

        //
        // POST: /CourseTermMember/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id, FormCollection collection)
        {
            CourseTermMember member = dataRepository.GetCourseTermMemberByID(id);
            CourseTermMember curr = dataRepository.GetCourseTermMemberByMembershipID(courseTerm, UserHelpers.GetCurrentUserID());
            if (curr.AccessLevel <= member.AccessLevel && curr.MembershipID != member.MembershipID) //Can only modify people whose access level is lower than yours
            {
                FlashMessageHelper.AddMessage("You can only modify your subordinates!");
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            if (curr.AccessLevel < 6 && member.AccessLevel > 1) //TAs can only modify students
            {
                FlashMessageHelper.AddMessage("You can only edit students!");
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }
            if (curr.AccessLevel < 6 && curr.MembershipID == member.MembershipID) //Can't edit yourself unless you're an instructor
            {
                FlashMessageHelper.AddMessage("You can't edit yourself!");
                return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
            }

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
