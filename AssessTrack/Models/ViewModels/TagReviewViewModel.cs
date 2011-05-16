using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.ViewModels
{
    public class TagReviewViewModel
    {
        public Tag Tag;
        public List<ITaggable> Assessments;
        public List<ITaggable> Questions;
        public List<ITaggable> Answers;
    }
}
