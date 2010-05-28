using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;

namespace AssessTrack.Controllers
{
    public class ATController : Controller
    {
        protected AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();
    }
}
