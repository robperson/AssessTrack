using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.Home
{
    public class SiteSection
    {
        public Site Site;
        public List<CourseTermSection> CourseTermSections = new List<CourseTermSection>();

        public SiteSection(Site site)
        {
            AssessTrackDataRepository repo = new AssessTrackDataRepository();
            Site = site;
            foreach (CourseTerm ct in repo.GetUserCourseTerms(site))
            {
                CourseTermSections.Add(new CourseTermSection(ct));
            }
        }
    }
}
