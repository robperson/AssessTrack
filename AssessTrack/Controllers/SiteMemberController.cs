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
    public class SiteMemberViewModel
    {
        public List<SiteMemberTable> Tables;
        public Site Site;

        public SiteMemberViewModel(List<SiteMemberTable> tables, Site site)
        {
            Tables = tables;
            Site = site;
        }
    }

    public class SiteMemberTable
    {
        public string Caption;
        public List<SiteMember> Members;
        public string EmailAllLink;
        public bool ShowDetails;

        public SiteMemberTable(string cap, List<SiteMember> members, bool details)
        {
            Caption = cap;
            Members = members;
            string linkformat = "mailto:{0}";
            string emails = string.Join(",", (from member in members select member.Profile.EmailAddress).ToArray());
            EmailAllLink = string.Format(linkformat, emails);
            ShowDetails = details;
        }
    }

    [ATAuth(AuthScope = AuthScope.Site, MaxLevel = 10, MinLevel = 5)]
    public class SiteMemberController : ATController
    {
        //
        // GET: /CourseTermMember/

        public ActionResult Index(string siteShortName, string courseTermShortName, bool? details)
        {
            List<SiteMemberTable> Tables = new List<SiteMemberTable>();
            Tables.Add(new SiteMemberTable("Students", site.GetMembers(1,1), details ?? false));
            Tables.Add(new SiteMemberTable("All Faculty", site.GetMembers(5, 10), false));
            Tables.Add(new SiteMemberTable("Curriculum Committee", site.GetMembers(6, 6), false));
            Tables.Add(new SiteMemberTable("Assessment Committee", site.GetMembers(7, 7), false));
            Tables.Add(new SiteMemberTable("Assessment Coordinator", site.GetMembers(8, 8), false));
            Tables.Add(new SiteMemberTable("Department Chair", site.GetMembers(9, 9), false));
            Tables.Add(new SiteMemberTable("Site Admin", site.GetMembers(10, 10), false));
            Tables.Add(new SiteMemberTable("Excluded", site.GetMembers(0, 0), false));

            return View(new SiteMemberViewModel(Tables, site));
        }

        //
        // GET: /CourseTermMember/Details/5

        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            SiteMember member = dataRepository.GetSiteMemberByID(id);

            return View(member);
        }


        //
        // GET: /CourseTermMember/Edit/5

        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id)
        {
            SiteMember member = dataRepository.GetSiteMemberByID(id);
            return View(member);
        }

        //
        // POST: /CourseTermMember/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string siteShortName, string courseTermShortName, Guid id, FormCollection collection)
        {
            SiteMember member = dataRepository.GetSiteMemberByID(id);

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
    }
}
