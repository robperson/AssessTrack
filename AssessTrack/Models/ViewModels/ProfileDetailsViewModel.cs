using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessTrack.Models.Home;

namespace AssessTrack.Models.ViewModels
{
    public class ProfileDetailsViewModel
    {
        public Profile member;
        public List<CourseTerm> CourseTerms;

        public ProfileDetailsViewModel(Profile mem, List<CourseTerm> cTerms)
        {
            member = mem;
            CourseTerms = cTerms;
        }
    }
}
