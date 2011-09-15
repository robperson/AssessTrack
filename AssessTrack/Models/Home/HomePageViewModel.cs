using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.Home
{
    public class HomePageViewModel
    {
        public List<SiteSection> SiteSections = new List<SiteSection>();
        public List<DepartmentSiteListItem> DepartmentSites = new List<DepartmentSiteListItem>();

        AssessTrackDataRepository _repo;
        
        public HomePageViewModel(AssessTrackDataRepository repo)
        {
            _repo = repo;
            foreach (Site site in _repo.GetUserSites())
            {
                SiteSections.Add(new SiteSection(_repo, site));
            }

            foreach (var site in _repo.GetAllSites())
            {
                DepartmentSites.Add(new DepartmentSiteListItem(_repo,site));
            }
        }
    }
}
