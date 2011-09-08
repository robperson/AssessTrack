using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessTrack.Helpers;
using AssessTrack.Models.Home;
using System.Web.Security;

namespace AssessTrack.Controllers
{
    [HandleError]
    public class HomeController : ATController
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View(new HomePageViewModel());
            }
            else
            {
                return View("Home");
            }
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
