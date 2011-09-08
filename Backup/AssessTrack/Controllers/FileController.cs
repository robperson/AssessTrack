using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace AssessTrack.Controllers
{
    public class FileController : ATController
    {
        //
        // GET: /File/

        public ActionResult Download(Guid id)
        {
            AssessTrack.Models.File file = dataRepository.GetFileByID(id);

            if (file == null)
            {
                return View("FileNotFound");
            }

            return File(file.Data.ToArray(), file.Mimetype, file.Name);
        }

    }
}
