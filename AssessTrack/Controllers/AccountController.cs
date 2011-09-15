using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using AssessTrack.Models;
using AssessTrack.Helpers;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Transactions;

namespace AssessTrack.Controllers
{

    [HandleError]
    public class AccountController : ATController
    {

        //private AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();

        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        public ActionResult LogOn()
        {

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {

            if (!ValidateLogOn(userName, password))
            {
                return View();
            }

            FormsAuth.SignIn(userName, rememberMe);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResetPassword(string email)
        {
            try
            {
                string username = Membership.GetUserNameByEmail(email);
                MembershipUser user = Membership.GetUser(username);
                string password;
                try
                {
                    password = user.GetPassword();
                }
                catch
                {
                    password = user.ResetPassword();
                }

                //email the user their password
                string smtpUsername = ConfigurationManager.AppSettings["PasswordResetUsername"];
                string smtpPassword = ConfigurationManager.AppSettings["PasswordResetPassword"];

                string subject = "Your AssessTrack.com Account Login Information.";
                string body = @"As you requested, Your username is: ""{0}"", Your password is: ""{1}"" (without the quotation marks).
After you log on you can change it to something you will be able to remember.";

                body = string.Format(body, username, password);

                MailMessage message = new MailMessage(smtpUsername, user.Email, subject, body);

                EmailHelper.SendEmail(smtpUsername, smtpPassword, message);

            }
            catch
            {
                ModelState.AddModelError("_FORM", "An error occurred while resetting your password!");
                return View();
            }
            return View("PasswordResetSuccess");
        }

        public ActionResult LogOff()
        {

            FormsAuth.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (ValidateRegistration(userName, email, password, confirmPassword))
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(userName, password, email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Guid membershipID = UserHelpers.GetIDFromUsername(userName);
                    Profile profile = new Profile();
                    UpdateModel(profile);
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            profile.MembershipID = membershipID;
                            dataRepository.CreateProfile(profile);
                            FormsAuth.SignIn(userName, false /* createPersistentCookie */);
                            return RedirectToAction("Index", "Home");
                        }
                        catch
                        {
                            ModelState.AddModelErrors(profile.GetRuleViolations());
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in password not being changed.")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                return View();
            }

            try
            {
                if (MembershipService.ChangePassword(User.Identity.Name, currentPassword, newPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                return View();
            }
        }

        public ActionResult ChangePasswordSuccess()
        {

            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            Profile p = dataRepository.GetLoggedInProfile();
            return View(p);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Profile(FormCollection input)
        {
            Profile p = dataRepository.GetLoggedInProfile();
            try
            {
                UpdateModel(p, new string[] { "FirstName", "LastName", "SchoolIDNumber", "Major", "EmailAddress" });
                dataRepository.Save();
                return View("ProfileUpdateSuccess");
            }
            catch
            {
                ModelState.AddModelError("_FORM", "An unexpected error occurred.");
                return View(p);
            }
        }

        public ActionResult AcceptInvite(Guid id)
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            Invitation invite = dataRepository.Single<Invitation>(inv => inv.InvitationID == id);

            if (invite == null)
            {
                FlashMessageHelper.AddMessage("Invite not found.");
                return RedirectToRoute(new { action = "Index", controller = "Home" });
            }
            else if (invite.Accepted)
            {
                FlashMessageHelper.AddMessage("Invite already accepted.");
                return RedirectToRoute(new { action = "Index", controller = "Home" });
            }

            return View(invite);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AcceptInvite(Guid id, string userName, string password, string confirmPassword)
        {
            Invitation invite = dataRepository.Single<Invitation>(inv => inv.InvitationID == id);

            if (invite == null)
            {
                FlashMessageHelper.AddMessage("Invite not found.");
                return RedirectToRoute(new { action = "Index", controller = "Home" });
            }
            using (TransactionScope scope = new TransactionScope())
            {
                string email = invite.Email;
                ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

                if (ValidateRegistration(userName, email, password, confirmPassword))
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus = MembershipService.CreateUser(userName, password, email);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        Guid membershipID = UserHelpers.GetIDFromUsername(userName);
                        Profile profile = new Profile();
                        UpdateModel(profile);
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                profile.MembershipID = membershipID;
                                dataRepository.CreateProfile(profile);

                                //Create site membership
                                SiteMember sitemember = new SiteMember()
                                {
                                    AccessLevel = invite.SiteAccessLevel,
                                    Site = invite.Site,
                                    Profile = profile
                                };

                                //Create courseterm member
                                if (invite.CourseTerm != null)
                                {
                                    CourseTermMember ctmember = new CourseTermMember()
                                    {
                                        AccessLevel = invite.CourseTermAccessLevel.Value,
                                        CourseTerm = invite.CourseTerm,
                                        Profile = profile
                                    };
                                }

                                invite.Accepted = true;
                                dataRepository.Save();
                                FormsAuth.SignIn(userName, false /* createPersistentCookie */);
                                scope.Complete();
                                return RedirectToAction("Index", "Home");
                            }
                            catch
                            {
                                ModelState.AddModelErrors(profile.GetRuleViolations());
                            }

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                    }
                }
                
            }
            // If we got this far, something failed, redisplay form
            return View(invite);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }

            base.OnActionExecuting(filterContext);
        }

        #region Validation Methods

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            try
            {

                MembershipUser user = Membership.GetUser(userName);
                if (user.IsLockedOut)
                {
                    ModelState.AddModelError("_FORM", "Your account has been locked. Contact and administrator to have it unlocked.");
                }
            }
            catch
            {
            }
            if (!MembershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }
            

            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (password == null || password.Length < MembershipService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         MembershipService.MinPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }
    }
}
