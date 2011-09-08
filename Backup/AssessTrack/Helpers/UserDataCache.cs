using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Models;

namespace AssessTrack.Helpers
{
    public static class UserDataCache
    {
        public static Guid? CurrentUserID
        {
            get
            {
                return (HttpContext.Current.Items.Contains("CurrentUserID")) ?
                    HttpContext.Current.Items["CurrentUserID"] as Guid?: null;
            }
            set
            {
                HttpContext.Current.Items["CurrentUserID"] = value;
            }
        }

        public static Profile CurrentProfile
        {
            get
            {
                return (HttpContext.Current.Items.Contains("CurrentProfile")) ?
                    HttpContext.Current.Items["CurrentProfile"] as Profile : null;
            }
            set
            {
                HttpContext.Current.Items["CurrentProfile"] = value;
            }
        }
    }
}
