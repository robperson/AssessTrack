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

        public static DateTime GetUserRegistrationDate(Guid id)
        {
            MembershipUser user = Membership.GetUser(id);
            if (user != null)
            {
                return user.CreationDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static bool IsEmailRegistered(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            return (Membership.GetUserNameByEmail(email) != null);
        }

        public static string GetPasswordForID(Guid? id)
        {
            try
            {
                MembershipUser user = Membership.GetUser(id, false);
                return user.GetPassword();
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
            
        }

        public static bool UnlockAccount(Guid? id)
        {
            try
            {
                MembershipUser user = Membership.GetUser(id, false);
                user.UnlockUser();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsAccountLocked(Guid? id)
        {
            try
            {
                MembershipUser user = Membership.GetUser(id, false);
                return user.IsLockedOut;
            }
            catch
            {
                return true;
            }
        }

        public static string GetFullNameForCurrentUser()
        {
            try
            {
                return GetFullNameForID(GetCurrentUserID());
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
            if (UserDataCache.CurrentUserID != null)
                return UserDataCache.CurrentUserID.Value;
            MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            if (user == null)
                return Guid.Empty;
            UserDataCache.CurrentUserID = (Guid)user.ProviderUserKey;
            return (Guid)user.ProviderUserKey;
            
        }

        public static Guid GetIDFromUsername(string username)
        {
            MembershipUser user = Membership.GetUser(username);
            return (Guid)user.ProviderUserKey;
        }

        public static Guid GetIDFromEmail(string email)
        {
            string username = Membership.GetUserNameByEmail(email);
            return GetIDFromUsername(username);
        }
    }
}
