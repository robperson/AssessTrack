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
using System.Collections.Generic;
using System.Transactions;
using AssessTrack.Helpers;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        private AssessTrackModelClassesDataContext dc = new AssessTrackModelClassesDataContext();
        public Course GetCourseByID(Guid id)
        {
            var course = (from c in dc.Courses
                          where c.CourseID == id
                          select c).SingleOrDefault();
            return course;
        }

        //public Course GetCourseByShortName(string courseShortName)
        //{
        //    return null;
        //}

        public IEnumerable<Course> GetSiteCourses(Site site)
        {
            return site.Courses.ToList();
        }

        public void CreateCourse(Course newCourse)
        {
            try
            {
                using (TransactionScope t = new TransactionScope())
                {
                    dc.Courses.InsertOnSubmit(newCourse);
                    dc.SubmitChanges();
                    
                    t.Complete();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteCourse(Course course)
        {
            foreach (var courseTerm in course.CourseTerms)
            {
                DeleteCourseTerm(courseTerm);
            }

            dc.Courses.DeleteOnSubmit(course);
        }

        public void Save()
        {
            dc.SubmitChanges();
        }
    }


}
