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
    public class CourseTermViewModel
    {
        public SelectList CourseList;
        public SelectList TermList;
        public CourseTerm CourseTerm;
        
        public CourseTermViewModel(Site site, CourseTerm courseTerm)
        {
            CourseList = new SelectList(site.Courses.ToList(), "CourseID", "Name");
            TermList = new SelectList(site.Terms.ToList(), "TermID", "Name");
            CourseTerm = courseTerm;
        }

        public CourseTermViewModel(Site site, CourseTerm courseTerm, Guid courseid, Guid termid)
        {
            CourseList = new SelectList(site.Courses.ToList(), "CourseID", "Name", courseid);
            TermList = new SelectList(site.Terms.ToList(), "TermID", "Name", termid);
            CourseTerm = courseTerm;
        }
    }

    public class CourseTermJoinModel
    {
        public SelectList CourseTermsList;
        public CourseTermJoinModel(IEnumerable<CourseTerm> courseTerms, CourseTerm selected)
        {
            if (selected != null)
            {
                CourseTermsList = new SelectList(courseTerms, "CourseTermID", "Name", selected.CourseTermID);
            }
            else
            {
                CourseTermsList = new SelectList(courseTerms, "CourseTermID", "Name");
            }
        }
    }

    public class CourseTermIndexModel
    {
        public CourseTerm CourseTerm;
        public bool UserEnrolled;

        public CourseTermIndexModel(CourseTerm ct, bool userEnrolled)
        {
            CourseTerm = ct;
            UserEnrolled = userEnrolled;
        }
    }

    public class CourseTermController : ATController
    {
        //
        // GET: /CourseTerm/
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Index(string siteShortName)
        {
            List<CourseTermIndexModel> courseTermList = new List<CourseTermIndexModel>();
            foreach (var ct in site.CourseTerms)
            {
                courseTermList.Add(new CourseTermIndexModel(ct, dataRepository.IsCurrentUserCourseTermMember(ct))); 
            }
            return View(courseTermList);
        }

        //
        // GET: /CourseTerm/
        [ATAuth(AuthScope = AuthScope.Application, MinLevel = 1, MaxLevel = 10)]
        public ActionResult MyCourseEnrollments()
        {
            return View(dataRepository.GetUserCourseTerms());
        }

        

        //
        // GET: /CourseTerm/Details/5
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Details(string siteShortName, string courseTermShortName)
        {
            return View(courseTerm);
        }

        //
        // GET: /CourseTerm/Create
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Create(string siteShortName)
        {
            CourseTerm courseTerm = new CourseTerm();
            return View(new CourseTermViewModel(site,courseTerm));
        } 

        //
        // POST: /CourseTerm/Create

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Create(string siteShortName, Guid CourseID, Guid TermID, CourseTerm courseTerm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    courseTerm.CourseID = CourseID;
                    courseTerm.TermID = TermID;
                    courseTerm.Site = site;
                    dataRepository.CreateCourseTerm(courseTerm);
                    
                    AssessTrack.Models.File file = FileUploader.GetFile("Syllabus",Request);
                    
                    if (file != null)
                    {
                        courseTerm.File = file;
                        FileUploader.SaveFile(dataRepository, file);
                        dataRepository.Save();
                        FlashMessageHelper.AddMessage("New syllabus uploaded.");
                    }
                    return RedirectToAction("Index", new { siteShortName = siteShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(courseTerm.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex.Message);
                }
            }
            return View(new CourseTermViewModel(site, courseTerm, CourseID, TermID));
        }

        //
        // GET: /CourseTerm/Edit
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Edit()
        {
            return View(new CourseTermViewModel(site, courseTerm,courseTerm.CourseID,courseTerm.TermID));
        }

        //
        // POST: /CourseTerm/Edit

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Edit(string siteShortName, Guid CourseID, Guid TermID)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UpdateModel(courseTerm);
                    AssessTrack.Models.File file; // = FileUploader.GetFile("Syllabus",Request);
                    if (courseTerm.File == null)
                    {
                        file = FileUploader.GetFile("Syllabus", Request);
                        FileUploader.SaveFile(dataRepository, file);
                    }
                    else
                    {
                        file = courseTerm.File;
                        FileUploader.UpdateFile("Syllabus", Request, file);
                    }
                    if (file != null)
                    {
                        courseTerm.File = file;
                        
                        FlashMessageHelper.AddMessage("New syllabus uploaded.");
                    }
                    dataRepository.Save();
                    FlashMessageHelper.AddMessage(courseTerm.Name + " has been updated.");
                    return RedirectToAction("Index", new { siteShortName = siteShortName });
                }
                catch (RuleViolationException)
                {
                    ModelState.AddModelErrors(courseTerm.GetRuleViolations());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", ex.Message);
                }
            }
            return View(new CourseTermViewModel(site, courseTerm, CourseID, TermID));
        }

        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Join()
        {
            return View(new CourseTermJoinModel(site.CourseTerms.AsEnumerable(),courseTerm));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 0, MaxLevel = 10)]
        public ActionResult Join(string siteShortName, Guid id, string password)
        {
            try
            {
                CourseTerm courseTerm = dataRepository.GetCourseTermByID(site,id);
                if (courseTerm == null)
                    return View("CourseTermNotFound");
                if (!string.IsNullOrEmpty(courseTerm.Password) &&
                    (courseTerm.Password != password))
                {
                    ModelState.AddModelError("_FORM", "Incorrect Password.");
                    return View(new CourseTermJoinModel(site.CourseTerms.AsEnumerable(),courseTerm));
                }
                if (dataRepository.JoinCourseTerm(courseTerm))
                    return RedirectToAction("Index", new { siteShortName = siteShortName });
                else
                    return View("AlreadyCourseTermMember");
                
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        //
        // GET: /CourseTerm/Edit
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 1, MaxLevel = 10)]
        public ActionResult GetSyllabus()
        {
            
            if (courseTerm.File != null)
            {
                return File(courseTerm.File.Data.ToArray(), courseTerm.File.Mimetype, courseTerm.File.Name);
            }
            else
            {
                FlashMessageHelper.AddMessage("No Syllabus.");
                return RedirectToAction("Index", new { siteShortName = site.ShortName });
            }
        }

        //
        // GET: /CourseTerm/Delete
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Delete()
        {
            return View(courseTerm);
        }

        //
        // POST: /CourseTerm/Edit

        [AcceptVerbs(HttpVerbs.Post)]
        [ATAuth(AuthScope = AuthScope.Site, MinLevel = 10, MaxLevel = 10)]
        public ActionResult Delete(FormCollection input)
        {
            try
            {
                dataRepository.DeleteCourseTerm(courseTerm);
                dataRepository.Save();
                FlashMessageHelper.AddMessage(courseTerm.Name + " has been deleted.");
            }
            catch (Exception ex)
            {
                FlashMessageHelper.AddMessage("An error occurred: " + ex.Message);
            }

            return RedirectToAction("Index", new { siteShortName = site.ShortName });
        }
    }
}
