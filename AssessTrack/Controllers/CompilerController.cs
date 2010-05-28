using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Helpers;

namespace AssessTrack.Controllers
{
    public class CompilerController : ATController
    {
        //
        // GET: /Compile/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Compile(Guid submissionRecordID)
        {
            SubmissionRecord record = dataRepository.GetSubmissionRecordByID(submissionRecordID);
            if (record == null)
            {
                var result = new
                {
                    message = "Could Not Find Submission Record",
                    comments = "",
                    code = -1
                };
                return Json(result);
            }
            try
            {
                record.CompileCodeQuestions();
                var result = new
                {
                    message = "Finished. See Comments",
                    comments = record.Comments,
                    code = 1
                };
                return Json(result);
            }
            catch
            {
                var result = new
                {
                    message = "OMG!!! SOMETHING AWFUL HAPPENED!!! :'(",
                    comments = "",
                    code = -1
                };
                return Json(result);
            }
        }

    }
}
