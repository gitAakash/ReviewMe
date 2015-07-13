using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using ReviewMe.Bal;
using ReviewMe.Common.Authorization;
using ReviewMe.Common.Enums;
using ReviewMe.Common.Helpers;
using ReviewMe.ViewModel;
using ReviewMe.Web.Attributes;
using ReviewMe.Web.Models;

namespace ReviewMe.Web.Controllers
{
    
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public string UrlAfterLogin = null;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        //
        // GET: /Account/Login

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
       {
            try
            {
                //var userBal = new UserBal();
                //List<User> allUsers = userBal.GetAllUsers();
                //ViewBag.ReturnUrl = returnUrl;
                var filterContext = new AuthorizationContext(ControllerContext);
                var reviewMeAuthentication = new ReviewMeAuthentication(filterContext);
                reviewMeAuthentication.Authorize();
                SessionInformation sessionInformation = SessionManager.GetSessionInformation();
                if (sessionInformation != null)
                {
                    if (returnUrl != null)
                        return Redirect(returnUrl);
                    if (SessionManager.GetUserRoleIdOfCurrentlyLoggedInUser().Equals(UserRoleEnum.Admin))
                        return RedirectToAction("Index", "Home");
                    if (SessionManager.GetUserRoleIdOfCurrentlyLoggedInUser().Equals(UserRoleEnum.TeamLeader))
                        return RedirectToAction("Index", "Home");
                    if (SessionManager.GetUserRoleIdOfCurrentlyLoggedInUser().Equals(UserRoleEnum.Developer))
                        return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    UrlAfterLogin = Request.QueryString["ReturnUrl"];
                    var loginViewModel = new LoginViewModel();
                    return View(loginViewModel);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Uri url = HttpContext.Request.UrlReferrer;
               // var encryptionHelper = new EncryptionHelper();
               // model.Password = encryptionHelper.Encrypt(model.Password);

                var userModel = new UserBal().GetAuthenticateUserViewModel(model.Email, model.Password);
                var roleModel=new RoleViewModel();
                if(userModel != null)
                {
                    roleModel = new RoleBal().GetRoleById(userModel.SelectedRoleId);
                }
             
                if (SessionManager.GetSessionInformation() == null)
                {
                    if (userModel != null)
                    {
                        if (userModel.IsActive)
                        {
                            var sessionInfo = new SessionInformation
                            {
                                UserId = userModel.Id,
                                UserRole = UserRoleHelper.GetUserRole(roleModel.RoleName),
                                UserName = userModel.FName,
                                FullName = userModel.FName + " " + userModel.LName,
                                EmailId = userModel.EmailId,
                                UserLogo = userModel.UserImage,
                                UserRoleId = userModel.SelectedRoleId,
                                //CanCreateUser = userModel.CanCreateUser,
                                //CanDeleteUser = userModel.CanDeleteUser,
                                TechnologyId = userModel.SelectedTechnologyId
                            };
                            SessionManager.SetSessionInformation(sessionInfo);
                            if (model.RememberMe)
                            {
                                var cookieInformation = new CookieInformation
                                {
                                    Email = model.Email,
                                    Password = model.Password
                                };
                                string cookie = JsonConvert.SerializeObject(cookieInformation);
                                FormsAuthentication.SetAuthCookie(cookie, model.RememberMe);
                            }
                            if (!string.IsNullOrEmpty(url.Query))
                            {
                                return Redirect(returnUrl);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        Session["LOGIN_FAILED"] = "Oops!!! Your account is not active. Please Contact Admin.";
                        return View(model);
                    }
                    /*Session["LOGIN_FAILED"] = "Oops!!! Invalid username or password, Please Try Again.";
                    return View(model);*/
                }
                else
                {
                    SessionManager.RemoveSessionInformation();
                }
                Session["LOGIN_FAILED"] = "Oops!!! Invalid username or password, Please Try Again.";
                              ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Login");
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //SignInStatus result =
            //    await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, model.RememberMe});
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            ApplicationUser user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                string code = await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            SignInStatus result =
                await
                    SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe,
                        model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isEmailSend = false;
                if (!string.IsNullOrEmpty(model.Email))
                {

                    UserViewModel userViewModel = new UserBal().GetUserByEmailId(model.Email);
                    if (userViewModel != null && userViewModel.Id != 0)
                    {
                        #region Send Reset Password Email to User

                        const string mailSubject = "NeoDailyReviews :: Reset Password";
                        string mainUrl = ConfigurationManager.AppSettings["MainURL"];
                        var sbMailBody = new StringBuilder();
                        var resetPassword = 1;
                        var response = new UserBal().UpdateResetPassword(userViewModel.Id, resetPassword);
                        StreamReader reader = new StreamReader(Server.MapPath("~/assets/EmailTemplate/SendEmailTemplate.html"));
                        string readFile = reader.ReadToEnd();
                        readFile = readFile.Replace("##SendTo##", userViewModel.FName);
                        readFile = readFile.Replace("##Link##", mainUrl + "Account/ResetPassword?userId=" + Convert.ToString(userViewModel.Id));
                        sbMailBody.Append(readFile);

                        //Send Reset Password Email to User
                        isEmailSend = EmailHelper.SendMail(model.Email, mailSubject, sbMailBody.ToString());
                        if (isEmailSend == true)
                        {
                            return RedirectToAction("ForgotPasswordConfirmation");
                        }
                        else
                        {
                            //return RedirectToAction("ErrorOnPage", "Check your Internet connection, Failed to send email");
                            ModelState.AddModelError("", "Failed to send emails");
                            return View();
                        }
                        #endregion
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Does Not Exists. Please Enter Valid Email Address");
                        return View();
                    }
                }
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string userId)
        {
            UserViewModel uvModel = new UserBal().GetUserById(Convert.ToInt64(userId));
            if (uvModel != null)
            {
                if (uvModel.ResetPassword)
                {
                    var model = new ResetPasswordViewModel
                    {
                        Id = uvModel.Id,
                        Email = uvModel.EmailId
                    };
                    return View(model);
                }
                return View("LinkExpire");
            }
            return View("Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserViewModel uvModel = new UserBal().GetUserById(Convert.ToInt64(model.Id));
            if (uvModel != null)
            {
                var response = new UserBal().UpdatePassword(model.Id, model.Password);
                if (response)
                    return RedirectToAction("ResetPasswordConfirmation");
            }
            return View("Error");
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            string userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            IList<string> userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            List<SelectListItem> factorOptions =
                userFactors.Select(purpose => new SelectListItem {Text = purpose, Value = purpose}).ToList();
            return
                View(new SendCodeViewModel {Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode",
                new {Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe});
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            SignInStatus result = await SignInManager.ExternalSignInAsync(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, RememberMe = false});
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel {Email = loginInfo.Email});
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                ExternalLoginInfo info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

      
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            SessionManager.RemoveSessionInformation();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}