using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Transactions;
using AssessTrack.Helpers;
using System.Data.Linq;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public SiteMember GetSiteMemberByID(Guid id)
        {
            return (from site in dc.SiteMembers
                    where site.SiteMemberID == id
                    select site).SingleOrDefault();
        }

        public SiteMember GetSiteMemberByMembershipID(Site site, Guid id)
        {
            return (from sitemem in site.SiteMembers
                    where sitemem.MembershipID == id
                    select sitemem).SingleOrDefault();
        }

        public bool IsUserSiteMember(Site site, Guid id)
        {
            SiteMember m = GetSiteMemberByMembershipID(site, id);
            return (m != null);            
        }

        public bool IsCurrentUserSiteMember(Site site)
        {
            try
            {
                return IsUserSiteMember(site, UserHelpers.GetCurrentUserID());
            }
            catch
            {
                return false;
            }
        }
    }


}
