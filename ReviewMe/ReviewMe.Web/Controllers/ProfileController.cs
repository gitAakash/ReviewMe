using ReviewMe.Bal;
using ReviewMe.Common.Authorization;
using ReviewMe.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewMe.Common.Enums;

namespace ReviewMe.Web.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MyProfile()
        {
            long userId = SessionManager.GetCurrentlyLoggedInUserId();
            var sessionInfo = SessionManager.GetSessionInformation();
            UserViewModel userViewModel;
            if (userId != 0)
            {
                userViewModel = new UserBal().GetUserById(Convert.ToInt64(userId));
               
            }
            else
                userViewModel = new UserBal().GetAddUserViewModel();

            return View(userViewModel);
        }
        [HttpPost]
        public ActionResult AddEditUser(UserViewModel userViewModel, HttpPostedFileBase FilePath)
        {
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                if (userViewModel.Id == 0)
                {
                   ReviewMe.Model.User userModel = new UserBal().AddUser(userViewModel);
                }
                if (FilePath != null)
                {
                    if (userViewModel.FilePath.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(userViewModel.FilePath.FileName);
                        fileName += DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(userViewModel.FilePath.FileName);
                        string fileSavePath = Path.Combine(Server.MapPath("~/ProfileImages/"), fileName);
                        userViewModel.FilePath.SaveAs(fileSavePath);
                        userViewModel.UserImage = fileName;
                    }
                }

                ReviewMe.Model.User userModel1 = new UserBal().UpdateUser(userViewModel);
                var sessionInfo = SessionManager.GetSessionInformation();
                sessionInfo.UserName = userViewModel.FName;
                sessionInfo.UserLogo = userViewModel.UserImage;
            }
            else
            {
                return View();
            }

            if(SessionManager.GetUserRoleOfCurrentlyLoggedInUser() == UserRoleEnum.Developer)
                return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "User");
            
        }
	}
}