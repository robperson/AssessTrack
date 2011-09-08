using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Helpers;
using AssessTrack.Models;

namespace AssessTrack.Controllers
{
    public class CourseTermFileController : ATController
    {
        //
        // GET: /CourseTermFile/

        public ActionResult Index()
        {
            var files = from file in courseTerm.CourseTermFiles
                        select file.File;

            return View(files);
        }

        public ActionResult Upload()
        {
            AssessTrack.Models.File file = new AssessTrack.Models.File();
            return View(file);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(AssessTrack.Models.File file)
        {
            try
            {
                FileUploader.UpdateFile("FileUpload", Request, file);
                dataRepository.SaveFile(file);
                CourseTermFile ctFile = new CourseTermFile();
                ctFile.File = file;
                ctFile.CourseTerm = courseTerm;

                dataRepository.Save();

                FlashMessageHelper.AddMessage("File uploaded.");

            }
            catch (RuleViolationException)
            {
                ModelState.AddModelErrors(file.GetRuleViolations());
                return View(file);
            }
            catch
            {
                ModelState.AddModelError("_FORM", "An unexpected error occured.");
            }
            return RedirectToAction("Index", new { siteShortName = site.ShortName, courseTermShortName = courseTerm.ShortName });
        }

        public ActionResult Edit(Guid id)
        {
            AssessTrack.Models.File file = dataRepository.GetFileByID(id);
            if (file == null)
                return View("FileNotFound");
            return View(file);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Guid id, FormCollection input)
        {
            AssessTrack.Models.File file = dataRepository.GetFileByID(id);
            if (file == null)
                return View("FileNotFound");
            try
            {
                UpdateModel(file);
                FileUploader.UpdateFile("FileUpload", Request, file);
                //dataRepository.SaveFile(file);
                
                dataRepository.Save();

                FlashMessageHelper.AddMessage("File updated.");

            }
            catch (RuleViolationException)
            {
                ModelState.AddModelErrors(file.GetRuleViolations());
                return View(file);
            }
            catch
            {
                ModelState.AddModelError("_FORM", "An unexpected error occured.");
            }
            return RedirectToAction("Index", new { siteShortName = site.ShortName, courseTermShortName = courseTerm.ShortName });
        }

        public ActionResult Delete(Guid id)
        {
            AssessTrack.Models.File file = dataRepository.GetFileByID(id);
            if (file == null)
                return View("FileNotFound");
            return View(file);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id, FormCollection input)
        {
            AssessTrack.Models.File file = dataRepository.GetFileByID(id);
            if (file == null)
                return View("FileNotFound");
            try
            {
                dataRepository.DeleteFile(file);
                FlashMessageHelper.AddMessage(file.Name + " has been deleted.");
                return RedirectToAction("Index", new { siteShortName = site.ShortName, courseTermShortName = courseTerm.ShortName });
            }
            catch
            {
                throw;
            }
        }
    }
}
