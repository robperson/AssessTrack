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
        AssessTrackDataRepository _repo;
        public SiteSection(AssessTrackDataRepository repo, Site site)
        {
            _repo = repo;
            Site = site;
            foreach (CourseTerm ct in _repo.GetUserCourseTerms(site))
            {
                CourseTermSections.Add(new CourseTermSection(_repo, ct));
            }
        }
    }
}
