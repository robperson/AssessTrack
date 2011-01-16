using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Filters;
using AssessTrack.Models;
using AssessTrack.Helpers;
using System.Data.Linq;

namespace AssessTrack.Controllers
{
    [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
    public class ProgramOutcomeController : ATController
    {
        //
        // GET: /ProgramOutcome/

        public ActionResult Index()
        {
            return View(site.ProgramOutcomes.ToList());
        }

        
        //
        // GET: /ProgramOutcome/Create

        public ActionResult Create()
        {
            ProgramOutcome outcome = new ProgramOutcome();
            return View(outcome);
        }

        //
        // POST: /ProgramOutcome/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ProgramOutcome newOutcome)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    newOutcome.Site = site;
                    site.ProgramOutcomes.Add(newOutcome);
                    //dataRepository.EnableDebugLogging();
                    dataRepository.Save();
                    return RedirectToAction("Index", new { siteShortName = site.ShortName});
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(newOutcome.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            return View(newOutcome);
        }

        //
        // GET: /ProgramOutcome/Edit/5

        public ActionResult Edit(Guid id)
        {
            ProgramOutcome outcome = dataRepository.Single<ProgramOutcome>(o => o.ProgramOutcomeID == id);
            if (outcome == null)
                return View("OutcomeNotFound");
            return View(outcome);

        }

        //
        // POST: /ProgramOutcome/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            ProgramOutcome outcome = dataRepository.Single<ProgramOutcome>(o => o.ProgramOutcomeID == id);
            if (outcome == null)
                return View("OutcomeNotFound");

            UpdateModel(outcome);
            if (ModelState.IsValid)
            {
                try
                {
                    dataRepository.Save();
                    return RedirectToAction("Index", new { siteShortName = site.ShortName});
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(outcome.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            return View(outcome);


        }

        //
        // GET: /ProgramOutcome/Delete/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Delete(Guid id)
        {
            ProgramOutcome outcome = dataRepository.Single<ProgramOutcome>(o => o.ProgramOutcomeID == id);
            if (outcome == null)
                return View("OutcomeNotFound");
            return View(outcome);
        }

        //
        // POST: /Term/Delete/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 3, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            ProgramOutcome outcome = dataRepository.Single<ProgramOutcome>(o => o.ProgramOutcomeID == id);
            if (outcome == null)
                return View("OutcomeNotFound");

            try
            {
                dataRepository.Remove(outcome);
                dataRepository.Save();
                FlashMessageHelper.AddMessage("\"" + outcome.Label + " - " + outcome.Description + "\" has been deleted.");
            }
            catch (Exception ex)
            {
                FlashMessageHelper.AddMessage("An error occurred: " + ex.Message);
                
            }
            return RedirectToAction("Index", new { siteShortName = site.ShortName });
        }

    }
}
