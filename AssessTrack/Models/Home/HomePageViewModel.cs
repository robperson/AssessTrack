using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models.Home
{
    public class HomePageViewModel
    {
        public List<SiteSection> SiteSections = new List<SiteSection>();

        public HomePageViewModel()
        {
            AssessTrackDataRepository repo = new AssessTrackDataRepository();
            foreach (Site site in repo.GetUserSites())
            {
                SiteSections.Add(new SiteSection(site));
            }
        }
    }
}
