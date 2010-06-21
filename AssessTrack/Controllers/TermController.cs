using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Helpers;
using AssessTrack.Filters;

namespace AssessTrack.Controllers
{
    public class TermController : ATController
    {
        //
        // GET: /Term/
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Index(string siteShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                return View(dataRepository.GetSiteTerms(site));
            }
            else
            {
                return View("SiteNotFound");
            }
        }

        //
        // GET: /Term/Details/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Details(string siteShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                Term term = dataRepository.GetTermByID(id);
                if (term != null)
                {
                    return View(term);
                }
                else
                {
                    return View("TermNotFound");
                }
            }
            else
            {
                return View("SiteNotFound");
            }
        }

        //
        // GET: /Term/Create
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Create(string siteShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                Term term = new Term() { StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(5) };
                return View(term);
            }
            else
            {
                return View("SiteNotFound");
            }
        } 

        //
        // POST: /Term/Create
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string siteShortName, Term term)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        term.Site = site;
                        dataRepository.CreateTerm(term);
                        return RedirectToAction("Index", new { siteShortName = siteShortName });
                    }
                    catch (RuleViolationException)
                    {
                        ModelState.AddModelErrors(term.GetRuleViolations());
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("_FORM", ex.Message);
                    }
                }
                return View(term);
            }
            else
            {
                return View("SiteNotFound");
            }
        }

        //
        // GET: /Term/Edit/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Edit(string siteShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                Term term = dataRepository.GetTermByID(id);
                if (term != null)
                {
                    return View(term);
                }
                else
                {
                    return View("TermNotFound");
                }
            }
            else
            {
                return View("SiteNotFound");
            }
        }

        //
        // POST: /Term/Edit/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string siteShortName, Guid id, FormCollection collection)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site != null)
            {
                Term term = dataRepository.GetTermByID(id);
                if (term != null)
                {
                    UpdateModel(term);
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            dataRepository.Save();

                            return RedirectToAction("Details", new { siteShortName = siteShortName, id = term.TermID });
                        }
                        catch (RuleViolationException)
                        {
                            ModelState.AddModelErrors(term.GetRuleViolations());
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("_FORM", ex);
                        }
                    }
                    return View(term);
                }
                else
                {
                    return View("TermNotFound");
                }
            }
            else
            {
                return View("SiteNotFound");
            }
        }
    }
}
