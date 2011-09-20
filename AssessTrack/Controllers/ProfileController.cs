using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Filters;
using AssessTrack.Helpers;
using AssessTrack.Models.ViewModels;
using AssessTrack.Models.Home;

namespace AssessTrack.Controllers
{
    public class ProfileViewModel
    {
        public List<ProfileTable> Tables;

        public ProfileViewModel(List<ProfileTable> tables)
        {
            Tables = tables;
        }
    }

    public class ProfileTable
    {
        public string Caption;
        public List<Profile> Members;
        public string EmailAllLink;
        public bool ShowDetails;

        public ProfileTable(string cap, List<Profile> members, bool details)
        {
            Caption = cap;
            Members = members;
            string linkformat = "mailto:{0}";
            string emails = string.Join(",", (from member in members select member.EmailAddress).ToArray());
            EmailAllLink = string.Format(linkformat, emails);
            ShowDetails = details;
        }
    }

    [ATAuth(AuthScope = AuthScope.Application, MaxLevel = 10, MinLevel = 5)]
    public class ProfileController : ATController
    {
        //
        // GET: /Profile/

        public ActionResult Index(bool? details)
        {
            List<ProfileTable> Tables = new List<ProfileTable>();
            Tables.Add(new ProfileTable("Standard Users", dataRepository.GetAllProfiles(1,1), details ?? false));
            Tables.Add(new ProfileTable("Power Users", dataRepository.GetAllProfiles(2, 10), false));
            Tables.Add(new ProfileTable("Excluded Users", dataRepository.GetAllProfiles(0, 0), false));


            return View(new ProfileViewModel(Tables));
        }


        //
        // GET: /Profile/Details/5

        public ActionResult Details(Guid id)
        {
            Profile member = dataRepository.GetProfileByID(id);
            List<CourseTerm> cTerms = dataRepository.GetOtherUserCourseTerms(0, id);
            ProfileDetailsViewModel model = new ProfileDetailsViewModel(member, cTerms);
            return View(model);
        }


        //
        // GET: /Profile/Edit/5

        public ActionResult Edit(Guid id)
        {
            Profile member = dataRepository.GetProfileByID(id);
            return View(member);
        }

        //
        // POST: /Profile/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            Profile member = dataRepository.GetProfileByID(id);

            try
            {
                member.AccessLevel = Convert.ToByte(collection["AccessLevel"]);
                dataRepository.Save();

                return RedirectToAction("Index");
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
