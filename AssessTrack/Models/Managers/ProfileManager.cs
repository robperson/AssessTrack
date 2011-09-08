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
using System.Transactions;
using System.Collections.Generic;
using AssessTrack.Helpers;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public void CreateProfile(Profile newProfile)
        {
            newProfile.AccessLevel = 1;
            dc.Profiles.InsertOnSubmit(newProfile);
            dc.SubmitChanges();
        }

        public Profile GetLoggedInProfile()
        {
            Guid id = UserHelpers.GetCurrentUserID();
            //if (UserDataCache.CurrentProfile != null)
            //    return UserDataCache.CurrentProfile;
            Profile profile = dc.Profiles.SingleOrDefault(sp => sp.MembershipID == id);
            //UserDataCache.CurrentProfile = profile;
            return profile;
        }

        public Profile GetProfileByID(Guid id)
        {
            return dc.Profiles.SingleOrDefault(p => p.MembershipID == id);
        }
    }
}
