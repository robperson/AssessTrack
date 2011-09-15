using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Helpers;

namespace AssessTrack.Models.Home
{
    public class DepartmentSiteListItem
    {
        public Site Site { get; set; }
        public bool UserIsMember { get; set; }

        AssessTrackDataRepository _repo;

        public DepartmentSiteListItem(AssessTrackDataRepository repo, Site site)
        {
            Site = site;
            _repo = repo;
            UserIsMember = _repo.IsSiteMember(Site);
        }
    }
}