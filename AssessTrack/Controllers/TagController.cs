using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Helpers;
using AssessTrack.Filters;
using AssessTrack.Models.ViewModels;

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

    
    public class TagController : ATController
    {
        //
        // GET: /AssessmentType/
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Index(string siteShortName, string courseTermShortName)
        {
            return View(courseTerm.Tags.ToList());
        }

        //
        // GET: /AssessmentType/Details/5
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Details(string siteShortName, string courseTermShortName, Guid id)
        {
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
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Create(string courseTermShortName, string siteShortName)
        {
            Tag newTag = new Tag();
            TagEditViewModel model = new TagEditViewModel(newTag, site.ProgramOutcomes.ToList());
            return View(model);
        } 

        //
        // POST: /AssessmentType/Create
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string courseTermShortName, string siteShortName, FormCollection input)
        {
            Tag newTag = new Tag();
            UpdateModel(newTag);
            if (ModelState.IsValid)
            {
                    try
                    {
                        newTag.CreatedBy = dataRepository.GetLoggedInProfile().MembershipID;
                        courseTerm.Tags.Add(newTag);
                        newTag.Name = newTag.Name.Trim();
                        if (newTag.IsCourseOutcome)
                        {
                            foreach (var outcome in site.ProgramOutcomes)
                            {

                                string checkedState = input[outcome.ProgramOutcomeID.ToString()];
                                if (checkedState != "false")
                                {
                                    TagProgramOutcome tpo = new TagProgramOutcome()
                                    {
                                        Tag = newTag,
                                        ProgramOutcomeID = outcome.ProgramOutcomeID
                                    };
                                }
                            }
                        }
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
            TagEditViewModel model = new TagEditViewModel(newTag, site.ProgramOutcomes.ToList());
            return View(model);
        }

        //
        // GET: /AssessmentType/Edit/5
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id)
        {
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");
            TagEditViewModel model = new TagEditViewModel(tag, site.ProgramOutcomes.ToList());
            return View(model);
        }

        //
        // POST: /AssessmentType/Edit/5
        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 3, MaxLevel = 10)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string courseTermShortName, string siteShortName, Guid id, FormCollection collection)
        {
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");
                    
            UpdateModel(tag);
            tag.Name = tag.Name.Trim();
            if (ModelState.IsValid)
            {
                try
                {
                    if (tag.IsCourseOutcome)
                    {
                        foreach (var outcome in site.ProgramOutcomes)
                        {
                            string checkedState = collection[outcome.ProgramOutcomeID.ToString()];
                            TagProgramOutcomeRelationship rel = new TagProgramOutcomeRelationship(tag, outcome);
                            if (checkedState != "false")
                            {
                                if (!rel.AreRelated)
                                {
                                    TagProgramOutcome tpo = new TagProgramOutcome()
                                        {
                                            Tag = tag,
                                            ProgramOutcome = outcome
                                        };
                                }
                            }
                            else
                            {
                                if (rel.AreRelated)
                                {
                                    TagProgramOutcome tpo = dataRepository.Single<TagProgramOutcome>(t => t.ProgramOutcome == outcome && t.Tag == tag);
                                    dataRepository.Remove<TagProgramOutcome>(tpo);
                                }
                            }
                        }
                    }
                    dataRepository.Save();
                    return RedirectToAction("Index", new { siteShortName = siteShortName, courseTermShortName = courseTermShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(tag.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex);
                }
            }
            TagEditViewModel model = new TagEditViewModel(tag, site.ProgramOutcomes.ToList());
            return View(model);
        }

        //
        // GET: /Tag/Delete/5

        public ActionResult Delete(Guid id)
        {
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");
            return View(tag);
        }

        //
        // POST: /Tag/Delete/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id, FormCollection input)
        {
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");
            try
            {
                dataRepository.DeleteTag(tag);
                dataRepository.Save();
                FlashMessageHelper.AddMessage("Tag Deleted Successfully");
                return RedirectToAction("Index", new { siteShortName = site.ShortName, courseTermShortName = courseTerm.ShortName });
            }
            catch (Exception ex)
            {
                FlashMessageHelper.AddMessage("An error occurred: " + ex.Message);
            }
            return View(tag);
        }

        [ATAuth(AuthScope = AuthScope.CourseTerm, MinLevel = 1, MaxLevel = 10)]
        public ActionResult Tutorial(Guid id)
        {
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");

            return View(tag);
        }
    }
}
