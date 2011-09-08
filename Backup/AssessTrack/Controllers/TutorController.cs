using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Filters;
using AssessTrack.Models;
using AssessTrack.Helpers;
using AssessTrack.Models.ViewModels;

namespace AssessTrack.Controllers
{
    [ATAuth(AuthScope=AuthScope.CourseTerm,MinLevel=1,MaxLevel=1)]
    public class TutorController : ATController
    {
        //
        // GET: /Tutor/

        public ActionResult Index()
        {
            var tags = dataRepository.GetStrugglingTags(dataRepository.GetLoggedInProfile(), courseTerm);

            return View(tags);
        }

        public ActionResult TagReview(Guid id)
        {
            Tag tag = dataRepository.GetTagByID(courseTerm, id);
            if (tag == null)
                return View("TagNotFound");
            var tagged = dataRepository.GetTaggedItems(tag);
            Profile p = dataRepository.GetLoggedInProfile();
            var failedItems = tagged.Where(t => t.Score(p) < 70);
            TagReviewViewModel model = new TagReviewViewModel()
            {
                Tag = tag,
                Assessments = failedItems.Where(t => t is Assessment).ToList(),
                Questions = failedItems.Where(t => t is Question).ToList(),
                Answers = failedItems.Where(t => t is Answer).ToList()
            };
            return View(model);
        }
    }
}
