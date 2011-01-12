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
            return View(dataRepository.GetSiteTerms(site));
        }

        //
        // GET: /Term/Details/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Details(string siteShortName, Guid id)
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

        //
        // GET: /Term/Create
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Create(string siteShortName)
        {
            Term term = new Term() { StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(5) };
            return View(term);
        } 

        //
        // POST: /Term/Create
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string siteShortName, Term term)
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

        //
        // GET: /Term/Edit/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Edit(string siteShortName, Guid id)
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

        //
        // POST: /Term/Edit/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string siteShortName, Guid id, FormCollection collection)
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

        //
        // GET: /Term/Delete/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Delete(string siteShortName, Guid id)
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

        //
        // POST: /Term/Delete/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(string siteShortName, Guid id, FormCollection collection)
        {
            Term term = dataRepository.GetTermByID(id);
            if (term != null)
            {
                try
                {
                    dataRepository.DeleteTerm(term);
                    dataRepository.Save();
                    FlashMessageHelper.AddMessage(term.Name + " has been deleted.");
                }
                catch (Exception ex)
                {
                    FlashMessageHelper.AddMessage("An error occurred: " + ex.Message);
                }

                return RedirectToAction("Index", new { siteShortName = siteShortName });
            }
            else
            {
                return View("TermNotFound");
            }
        }
    }
}
