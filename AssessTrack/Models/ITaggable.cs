using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssessTrack.Models
{
    public interface ITaggable
    {
        double Weight { get; }
        string Name { get; }
        double Score(Profile profile);
    }
}
