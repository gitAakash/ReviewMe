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
        }                

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
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

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            SessionManager.RemoveSessionInformation();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }          
    }
}