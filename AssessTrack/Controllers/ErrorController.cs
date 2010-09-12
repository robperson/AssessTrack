using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace AssessTrack.Controllers
{
    public class ErrorController : ATController
    {
        //
        // GET: /Error/

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult UnexpectedError()
        {
            return View();
        }

    }
}
