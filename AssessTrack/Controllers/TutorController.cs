using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Filters;

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

    }
}
