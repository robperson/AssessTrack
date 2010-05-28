using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public AssessmentType GetAssessmentTypeByName(CourseTerm course, string name)
        {
            return course.AssessmentTypes.SingleOrDefault(at => at.Name == name);
        }

        public AssessmentType GetAssessmentTypeByID(CourseTerm course, Guid id)
        {
            return course.AssessmentTypes.SingleOrDefault(at => at.AssessmentTypeID == id);
        }

        public SelectList GetAssessmentTypesSelectList(CourseTerm course)
        {
            return GetAssessmentTypesSelectList(course,null);
        }

        public SelectList GetAssessmentTypesSelectList(CourseTerm course, object selectedValue)
        {
            return new SelectList(course.AssessmentTypes, "AssessmentTypeID", "Name", selectedValue);
        }
    }
}
