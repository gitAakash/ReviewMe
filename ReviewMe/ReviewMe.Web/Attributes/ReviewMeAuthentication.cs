using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReviewMe.Bal;
using ReviewMe.Common.Authorization;
using ReviewMe.Common.Helpers;

namespace ReviewMe.Web.Attributes
{
    public class ReviewMeAuthentication
    {
        private readonly AuthorizationContext _filterContext;

        public ReviewMeAuthentication(AuthorizationContext filterContext)
        {
            _filterContext = filterContext;
        }

        public void Authorize()
        {
            if (_filterContext.HttpContext.Session == null)
                return;
            var sessionInfo = _filterContext.HttpContext.Session["USER_SESSION_INFORMATION"] as SessionInformation;
            Uri url = _filterContext.HttpContext.Request.UrlReferrer;
            if (sessionInfo == null)
            {
                string cookieName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = _filterContext.HttpContext.Request.Cookies[cookieName];
                if (authCookie == null)
                {
                    _filterContext.Result = new HttpUnauthorizedResult();
                   // HttpContext.Current.Response.Redirect("~/Account/Login", true);
                }
                else
                {
                    FormsAuthenticationTicket cookie = FormsAuthentication.Decrypt(authCookie.Value);
                    JObject cookieInfo = JObject.Parse(cookie.Name);
                    var encryptionHelper = new EncryptionHelper();
                    var email = (string)cookieInfo["Email"];
                    string password = (string) cookieInfo["Password"];//encryptionHelper.Decrypt((string)cookieInfo["Password"]);

                    var userModel = new UserBal().GetAuthenticateUserViewModel(email, password);
                    var roleModel = new RoleBal().GetRoleById(userModel.SelectedRoleId);
                    if (!userModel.IsActive)
                    {
                        FormsAuthentication.SignOut();
                        _filterContext.Result = new HttpUnauthorizedResult();
                       // HttpContext.Current.Response.Redirect("~/Account/Login", true);
                    }
                    else
                    {
                        sessionInfo = new SessionInformation
                        {
                            UserId = userModel.Id,
                            UserRole = UserRoleHelper.GetUserRole(roleModel.RoleName),
                            UserName = userModel.FName,
                            FullName= userModel.FName+" "+userModel.LName,
                            EmailId = userModel.EmailId,
                            UserLogo = userModel.UserImage,
                            UserRoleId = userModel.SelectedRoleId,
                            //CanCreateUser = userModel.CanCreateUser,
                            //CanDeleteUser = userModel.CanDeleteUser,
                            TechnologyId = userModel.SelectedTechnologyId
                        };
                        _filterContext.HttpContext.Session.Add("USER_SESSION_INFORMATION", sessionInfo);
                    }
                }
            }
        }
    }
}