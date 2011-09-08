using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.ViewModels
{
    public class TagEditViewModel
    {
        public Tag Tag;
        public List<TagProgramOutcomeRelationship> ProgramOutcomes;

        public TagEditViewModel(Tag tag, List<ProgramOutcome> outcomes)
        {
            Tag = tag;
            ProgramOutcomes = new List<TagProgramOutcomeRelationship>();
            foreach (var outcome in outcomes)
            {
                ProgramOutcomes.Add(new TagProgramOutcomeRelationship(tag, outcome));
            }
        }
    }
}
