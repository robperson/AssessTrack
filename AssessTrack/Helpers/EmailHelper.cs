using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace AssessTrack.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail(string smtpUsername, string smtpPassword, MailMessage message)
        {
            string server = ConfigurationManager.AppSettings["SmtpServer"];
            string port = ConfigurationManager.AppSettings["SmtpPort"];
                
            SmtpClient client = new SmtpClient(server, Convert.ToInt32(port));
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword, "");

            client.Send(message);
        }

        public static void SendPasswordResetEmail(MailMessage message)
        {
            string smtpUsername = ConfigurationManager.AppSettings["PasswordResetUsername"];
            string smtpPassword = ConfigurationManager.AppSettings["PasswordResetPassword"];

            SendEmail(smtpUsername, smtpPassword, message);
        }

        public static void SendInvitationEmail(MailMessage message)
        {
            string smtpUsername = ConfigurationManager.AppSettings["InvitationsUsername"];
            string smtpPassword = ConfigurationManager.AppSettings["InvitationsPassword"];

            SendEmail(smtpUsername, smtpPassword, message);
        }
    }
}
