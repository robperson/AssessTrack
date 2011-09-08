using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.ViewModels
{
    /// <summary>
    /// Represents whether or not there is a relationship between
    /// a Tag and a ProgramOutcome
    /// </summary>
    public class TagProgramOutcomeRelationship
    {
        public Tag Tag;
        public ProgramOutcome Outcome;
        public bool AreRelated;

        public TagProgramOutcomeRelationship(Tag tag, ProgramOutcome outcome)
        {
            Tag = tag;
            Outcome = outcome;
            var rel = from tpo in tag.TagProgramOutcomes
                      where tpo.ProgramOutcome == outcome
                      select tpo;
            AreRelated = rel.SingleOrDefault() != null;
        }
    }
}
