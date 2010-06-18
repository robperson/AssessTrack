﻿using System;
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
using AssessTrack.Models;

namespace AssessTrack.Helpers
{
    public class UserHelpers
    {
        public static string GetUsernameForID(Guid? id)
        {
            try
            {
                MembershipUser user = Membership.GetUser(id, false);
                return user.UserName;
            }
            catch
            {
                return "N/A";
            }
        }

        public static string GetFullNameForID(Guid? id)
        {
            try
            {
                AssessTrackDataRepository repo = new AssessTrackDataRepository();
                Profile user = repo.GetProfileByID(id.Value);
                return user.FirstName + " " + user.LastName;
            }
            catch
            {
                return "N/A";
            }
        }

        public static Guid GetCurrentUserID()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException("UserID cannot be retreived if user is not logged in.");
            }
            MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            return (Guid)user.ProviderUserKey;
            
        }

        public static Guid GetIDFromUsername(string username)
        {
            MembershipUser user = Membership.GetUser(username);
            return (Guid)user.ProviderUserKey;
        }
    }
}
