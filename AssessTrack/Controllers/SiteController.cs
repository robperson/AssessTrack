using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Helpers;
using AssessTrack.Models;
using AssessTrack.Filters;
using AssessTrack.Models.ViewModels;

namespace AssessTrack.Controllers
{
    public class SiteViewModel
    {
        public SelectList SiteList;
        public List<Site> Sites;

        public SiteViewModel(IEnumerable<Site> sites)
        {
            SiteList = new SelectList(sites,"SiteID","Title");
            Sites = sites.ToList();
        }
    }
    
    public class SiteController : ATController
    {
        //
        // GET: /Site/
        [ATAuth(AuthScope = AuthScope.Application, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Index()
        {
            return View(dataRepository.GetUserSites(UserHelpers.GetCurrentUserID()));
        }

        //
        // GET: /Site/Details/5
        [ATAuth(AuthScope = AuthScope.Application, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Details(string siteShortName)
        {
            return View(site);
        }

        //
        // GET: /Site/Create
        [ATAuth(AuthScope=AuthScope.Application,MinLevel=9,MaxLevel=10)]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Site/Create
        [ATAuth(AuthScope = AuthScope.Application, MinLevel = 9, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Site newsite = new Site();
                UpdateModel(newsite);
                if (ModelState.IsValid)
                {
                    Profile profile = dataRepository.GetLoggedInProfile();
                    dataRepository.CreateSite(profile, newsite);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelErrors(newsite.GetRuleViolations());
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Site/Edit/5
        [ATAuth(AuthScope = AuthScope.Application, MinLevel = 9, MaxLevel = 10)]
        public ActionResult Edit(string siteShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            return View(site);
        }

        //
        // POST: /Site/Edit/5
        [ATAuth(AuthScope = AuthScope.Application, MinLevel = 9, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string siteShortName, FormCollection collection)
        {
            try
            {
                UpdateModel(site);
                dataRepository.Save();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [ATAuth(AuthScope = AuthScope.Application, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Join()
        {
            return View(new SiteViewModel(dataRepository.GetUnEnrolledSites(UserHelpers.GetCurrentUserID())));
        }

        [ATAuth(AuthScope = AuthScope.Application, MinLevel = 0, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Join(Guid id,string password)
        {
            try
            {
                Site site = dataRepository.GetSiteByID(id);
                if (site == null)
                    return View("SiteNotFound");
                if (!string.IsNullOrEmpty(site.Password) &&
                    (site.Password != password))
                {
                    ModelState.AddModelError("_FORM", "Incorrect Password.");
                    return View(new SiteViewModel(dataRepository.GetUnEnrolledSites(UserHelpers.GetCurrentUserID())));
                }
                if (dataRepository.JoinSite(site))
                    return RedirectToAction("Index");
                else
                    return View("AlreadySiteMember");
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(new SiteViewModel(dataRepository.GetUnEnrolledSites(UserHelpers.GetCurrentUserID())));
        }

    }
}
