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
    public class TagDetailsModel
    {
        public List<Assessment> Assessments;
        public List<Question> Questions;
        public List<Answer> Answers;
        public Tag Tag;

        public TagDetailsModel(Tag tag, 
            List<Assessment> assessments, 
            List<Question> questions,
            List<Answer> answers)
        {
            Tag = tag;
            Assessments = assessments;
            Questions = questions;
            Answers = answers;
        }
    }

    [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
    public class TagController : ATController
    {
        //
        // GET: /AssessmentType/

        public ActionResult Index(string siteShortName, string courseTermShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site,courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            return View(courseTerm.Tags.ToList());
        }

        //
        // GET: /AssessmentType/Details/5

        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");

            return View(new TagDetailsModel(tag, 
                        dataRepository.GetTaggedAssessments(tag), 
                        dataRepository.GetTaggedQuestions(tag),
                        dataRepository.GetTaggedAnswers(tag)));
        }

        //
        // GET: /AssessmentType/Create

        public ActionResult Create(string courseTermShortName, string siteShortName)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");

            Tag newTag = new Tag();
            return View(newTag);
        } 

        //
        // POST: /AssessmentType/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string courseTermShortName, string siteShortName, Tag newTag)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            if (ModelState.IsValid)
            {
                    try
                    {
                        newTag.Profile = dataRepository.GetLoggedInProfile();
                        courseTerm.Tags.Add(newTag);
                        dataRepository.Save();
                        return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                    }
                    catch (RuleViolationException)
                    {
                        ModelState.AddModelErrors(newTag.GetRuleViolations());
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("_FORM", ex);
                    }
            }
            return View(newTag);
        }

        //
        // GET: /AssessmentType/Edit/5
 
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");
            return View(tag);

        }

        //
        // POST: /AssessmentType/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id, FormCollection collection)
        {
            Site site = dataRepository.GetSiteByShortName(siteShortName);
            if (site == null)
                return View("SiteNotFound");
            CourseTerm courseTerm = dataRepository.GetCourseTermByShortName(site, courseTermShortName);
            if (courseTerm == null)
                return View("CourseTermNotFound");
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");
                    
            UpdateModel(tag);
            if (ModelState.IsValid)
            {
                try
                {
                    dataRepository.Save();
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(tag.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("*", ex);
                }
            }
            return View(tag);
                

        }
    }
}
