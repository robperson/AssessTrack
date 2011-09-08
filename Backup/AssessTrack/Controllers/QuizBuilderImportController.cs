using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AssessTrack.Models;
using AssessTrack.Helpers;
using System.Xml.Linq;

namespace AssessTrack.Controllers
{
    [Authorize]
    public class QuizBuilderImportController : Controller
    {
        //
        // GET: /QuizBuilderImport/

        public ActionResult GetCourseOfferings()
        {
            AssessTrackDataRepository repo = new AssessTrackDataRepository();
            List<CourseTerm> courseTerms = repo.GetUserCourseTerms(5);
            var jsonCourseTerms = from ct in courseTerms select new { name = ct.Name, id = ct.CourseTermID };
            return Json(jsonCourseTerms);
        }

        public ActionResult GetAssessments(Guid id)
        {
            AssessTrackDataRepository repo = new AssessTrackDataRepository();
            CourseTerm term = repo.GetCourseTermByID(id);
            var assessments = from assessment in term.Assessments
                              orderby assessment.Name
                              select new { name = assessment.Name, id = assessment.AssessmentID };

            return Json(assessments);
                              
        }

        public ActionResult GetAssessmentImportForm(Guid id)
        {
            AssessTrackDataRepository repo = new AssessTrackDataRepository();
            Assessment assessment = repo.GetAssessmentByID(id);
            string importform = SubmissionFormHelpers.RenderImportForm(assessment);
            return Content(importform);
        }

        public ActionResult GetQuestion(Guid id)
        {
            AssessTrackDataRepository repo = new AssessTrackDataRepository();
            Question question = repo.GetQuestionByID(id);
            //Strip id attributes from the question and its answers
            XElement questionXml = XElement.Parse(question.Data);
            questionXml.SetAttributeValue("id", null);
            foreach (XElement answer in questionXml.Elements("answer"))
            {
                answer.SetAttributeValue("id", null);
            }
            string questiondata = DesignerHelper.GetQuestionMarkupFromXml(questionXml);
            return Content(questiondata);
        }

    }
}
