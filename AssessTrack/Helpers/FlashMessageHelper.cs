using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace AssessTrack.Helpers
{
    public static class FlashMessageHelper
    {
        private static string _key = "ATFlashMessage";
        
        public static void AddMessage(string message)
        {
            List<string> messages;
            if (HttpContext.Current.Session[_key] == null)
            {
                HttpContext.Current.Session[_key] = new List<string>();
            }
            messages = HttpContext.Current.Session[_key] as List<string>;

            messages.Add(message);
        }

        public static string PrintFlash()
        {
            List<string> messages;
            if (HttpContext.Current.Session[_key] == null)
            {
                HttpContext.Current.Session[_key] = new List<string>();
            }
            messages = HttpContext.Current.Session[_key] as List<string>;

            if (messages.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder flashOutput = new StringBuilder();
            flashOutput.Append(@"<div class=""flash""><ul>");
            foreach (var message in messages)
            {
                flashOutput.AppendFormat("<li>{0}</li>", message);
            }
            flashOutput.Append("</ul></div>");

            messages.Clear();

            return flashOutput.ToString();
        }
    }
}
