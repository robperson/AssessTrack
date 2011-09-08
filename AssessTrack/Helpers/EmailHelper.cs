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

        public static void SendInvitationEmail(string to, string siteName, Guid id)
        {
            string subject = string.Format("You've been invited to join \"{0}\" at AssessTrack.com!", siteName);
            string messageBody = @"<h2>You've been invited to join the site ""{0}"" on AssessTrack.com!</h2>
<p><a href=""http://74.93.208.198/Account/AcceptInvite/{1}"">Click here to accept the invite.</a></p>";
            messageBody = string.Format(messageBody, siteName, id.ToString());
            MailMessage msg = new MailMessage("noreply@assesstrack.com",to,subject,messageBody);
            msg.IsBodyHtml = true;
            SendInvitationEmail(msg);
        }

        public static void SendInvitationEmail(string to, string siteName, string courseTermName, Guid id)
        {
            string subject = string.Format("You've been invited to join \"{0} - {1}\" at AssessTrack.com!", siteName, courseTermName);
            string messageBody = @"<h2>You've been invited to join the site ""{0}"" on AssessTrack.com!</h2>
<p>You'll be automatically enrolled in ""{2}"" when you accept the invite. <a href=""http://74.93.208.198/Account/AcceptInvite/{1}"">Click here to accept the invite.</a></p>";
            messageBody = string.Format(messageBody, siteName, id.ToString(), courseTermName);
            MailMessage msg = new MailMessage("noreply@assesstrack.com", to, subject, messageBody);
            msg.IsBodyHtml = true;
            SendInvitationEmail(msg);
        }
    }
}
