﻿using System;
using System.IO;
using System.Web.Mvc;
using ReviewMe.Bal;
using ReviewMe.ViewModel;
using ReviewMe.Web.Attributes;
using System.Web;

namespace ReviewMe.Web.Controllers
{
    [ReviewMeAuthorize]
    public class UserController : Controller
    {
        // GET: /User/
        public ActionResult Index()
        {
            UserViewModelLong userViewModelLong = new UserBal().GetAllUsers();
            return View(userViewModelLong);
        }

        [HttpGet]
        public ActionResult AddEditUser(Int64? userId)
        {
            UserViewModel userViewModel;
            if (userId != null && userId != 0)
            {
                userViewModel = new UserBal().GetUserById(Convert.ToInt64(userId));                
            }
            else
                userViewModel = new UserBal().GetAddUserViewModel();

            return PartialView("AddEditUser", userViewModel);
        }

        [HttpPost]
        public ActionResult AddEditUser(UserViewModel userViewModel, HttpPostedFileBase FilePath)
        {
            if (ModelState.IsValid)
            {
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
                if (userViewModel.Id != 0)
                {
                    bool status = new UserBal().SaveOrUpdateUser(userViewModel);
                }
                else
                {
                    bool status = new UserBal().AddUser(userViewModel);
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public string DeleteUser(long id)
        {
            bool status = new UserBal().DeleteUser(Convert.ToInt64(id));
            if (status)
                return "Data deleted successfully.";

            return "Some error has occurred";
        }
    }
}