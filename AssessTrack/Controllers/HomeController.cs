using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessTrack.Controllers
{
    [HandleError]
    public class HomeController : ATController
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to the new Course Management System!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Install()
        {
            if (dataRepository.IsInstalled())
            {
                return View("AlreadyInstalled");
            }
            else
            {
                return View();
            }
        }
    }
}
