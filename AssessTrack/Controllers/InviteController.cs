using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Helpers;
using AssessTrack.Models.ViewModels;
using System.Net.Mail;

namespace AssessTrack.Controllers
{
    [Authorize()]
    public class InviteController : ATController
    {
        //
        // GET: /Invite/

        public ActionResult Index()
        {
            return View(site.Invitations.ToList());
        }

        //
        // GET: /Invite/Create

        public ActionResult Create()
        {
            IEnumerable<CourseTerm> courseTerms = dataRepository.GetAllCourseTerms(site);

            InviteModel model = new InviteModel(courseTerms);
            model.CourseTermAccessLevel = null;
            model.SiteAccessLevel = 1;
            return View(model);
        }

        //
        // POST: /Invite/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection input)
        {
            InviteModel invite = new InviteModel();
            UpdateModel(invite, "InviteModel");
            if (ModelState.IsValid)
            {
                //If a user is already registered with this email
                //create the appropriate membership
                if (UserHelpers.IsEmailRegistered(invite.Email))
                {
                    Guid profileid = UserHelpers.GetIDFromEmail(invite.Email);
                    Profile profile = dataRepository.GetProfileByID(profileid);
                    //Create site membership if user is not a site member already
                    SiteMember existingSiteMember = dataRepository.Single<SiteMember>(sm => sm.SiteID == site.SiteID && sm.MembershipID == profileid);
                    if (existingSiteMember == null)
                    {
                        SiteMember sitemember = new SiteMember()
                        {
                            AccessLevel = (byte)invite.SiteAccessLevel,
                            Site = site,
                            Profile = profile
                        };

                        
                    }
                    else
                    {
                        existingSiteMember.AccessLevel = (byte)invite.SiteAccessLevel;
                    }


                    //Create courseterm member if user is not already a member
                    if (invite.CourseTermID != null)
                    {
                        CourseTermMember existingCourseTermMember = dataRepository.Single<CourseTermMember>(ctm => ctm.CourseTermID == invite.CourseTermID && ctm.MembershipID == profileid);
                        if (existingCourseTermMember == null)
                        {
                            CourseTermMember ctmember = new CourseTermMember()
                            {
                                AccessLevel = (byte)invite.CourseTermAccessLevel.Value,
                                CourseTermID = invite.CourseTermID.Value,
                                Profile = profile
                            };
                        }
                        else
                        {
                            existingCourseTermMember.AccessLevel = (byte)invite.CourseTermAccessLevel;
                        }
                    }


                    dataRepository.Save();
                    FlashMessageHelper.AddMessage(string.Format(@"""{0}"" is already registered. They have been granted the requested permissions.", invite.Email));
                    return RedirectToAction("Index", new { siteShortName = site.ShortName });
                }
                Invitation inv = new Invitation();
                try
                {
                    inv = new Invitation()
                    {
                        Accepted = false,
                        CourseTermAccessLevel = (byte?)invite.CourseTermAccessLevel,
                        CourseTermID = (Guid?)invite.CourseTermID,
                        Email = invite.Email,
                        Site = site,
                        SiteAccessLevel = (byte)invite.SiteAccessLevel
                    };
                    
                    site.Invitations.Add(inv);
                    dataRepository.Save();
                    if (!inv.CourseTermID.HasValue)
                    {
                        EmailHelper.SendInvitationEmail(inv.Email, site.Title, inv.InvitationID);
                    }
                    else
                    {
                        CourseTerm ct = dataRepository.GetCourseTermByID(inv.CourseTermID.Value);
                        //If the CourseTerm can't be found, an exception should be raised when we try to save
                        //the Invitation
                        EmailHelper.SendInvitationEmail(inv.Email, site.Title, ct.Name, inv.InvitationID);
                            
                    }
                    FlashMessageHelper.AddMessage("Invite created successfully.");
                    return RedirectToAction("Index", new { siteShortName = site.ShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(inv.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            IEnumerable<CourseTerm> courseTerms = dataRepository.GetAllCourseTerms(site);
            invite.SetCourseTermList(courseTerms);
            return View(invite);
        }

    }
}
