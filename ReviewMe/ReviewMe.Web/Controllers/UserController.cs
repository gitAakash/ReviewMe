using System;
using System.IO;
using System.Web.Mvc;
using ReviewMe.Bal;
using ReviewMe.ViewModel;
using ReviewMe.Web.Attributes;
using System.Web;

using ReviewMe.Model;
using System.Linq;
using System.Collections.Generic;

namespace ReviewMe.Web.Controllers
{
    [ReviewMeAuthorize]
    public class UserController : Controller
    {
        // GET: /User/
        public ActionResult Index()
        {
            ViewBag.Status = TempData["Status"];
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

        [HttpGet]
        public ActionResult Search(string strSearch)
        {
            UserViewModelLong userViewModelLong = null;
            if(!string.IsNullOrEmpty(strSearch))
            {
                UserViewModel user = new UserViewModel();
                 userViewModelLong = new UserBal().GetAllUsers();
                int aa = userViewModelLong.UserViewModelList.Count();
                List<UserViewModel> userViewModel = new List<UserViewModel>();
                if (!string.IsNullOrEmpty(strSearch))
                    userViewModel = (List<UserViewModel>)userViewModelLong.UserViewModelList.Where(p => ((p.FName + ' ' + p.LName)).Contains(strSearch) || (p.Address != null && p.Address.Contains(strSearch) || (p.EmailId != null && p.EmailId.Contains(strSearch) || (p.MobileNo != null && p.MobileNo.Contains(strSearch))))).ToList();

                userViewModelLong.UserViewModelList = userViewModel;    
            }
            else
            {
                 userViewModelLong = new UserBal().GetAllUsers();
                return View(userViewModelLong);
            }
        
            return PartialView("Search", userViewModelLong);

        }

        [HttpPost]
        public ActionResult AddEditUser(UserViewModel userViewModel, HttpPostedFileBase FilePath)
        {
            // Modified By : Ramchandra Rane, 19th Jun 2015,Description : Added if condition
            if(userViewModel.Id !=0)
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");               
            }
            TempData["Status"] = "Opps! Some error has occurred";
            if (ModelState.IsValid)
            {               
                if (userViewModel.Id != 0)
                {                   
                    //string ProfileImagePath = new UserBal().GetUserById(userViewModel.Id).UserImage;
                    //if (!string.IsNullOrEmpty(ProfileImagePath) && FilePath != null)
                    //{
                    //   string fileSavePath = Path.Combine(Server.MapPath("~/ProfileImages/"), ProfileImagePath);
                    //    //Check whether file is exist or not on location
                    //   if(System.IO.File.Exists(fileSavePath))
                    //   {
                    //       System.IO.File.Delete(fileSavePath);
                    //   }
                    //}
                    bool status=false;
                    if (FilePath != null)
                    {
                        if (userViewModel.FilePath.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(userViewModel.FilePath.FileName);
                            fileName = userViewModel.Id + Path.GetExtension(fileName);
                            string fileSavePath = Path.Combine(Server.MapPath("~/ProfileImages/"), fileName);
                            userViewModel.FilePath.SaveAs(fileSavePath);
                            userViewModel.UserImage = fileName;                     

                            //Updating UserProfile image name;
                             status = new UserBal().SaveOrUpdateUser(userViewModel);

                             TempData["Status"] = "Records has been updated successfully";
                        }
                    }
                    else
                    {
                        status = new UserBal().SaveOrUpdateUser(userViewModel);
                        TempData["Status"] = "Records has been updated successfully.";
                    }                  
                }
                else
                {
                
                    User returnModel = new UserBal().AddUser(userViewModel);

                    if (FilePath != null)
                    {
                        if (userViewModel.FilePath.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(userViewModel.FilePath.FileName);
                            fileName = returnModel.Id + Path.GetExtension(fileName);
                            string fileSavePath = Path.Combine(Server.MapPath("~/ProfileImages/"), fileName);
                            userViewModel.FilePath.SaveAs(fileSavePath);
                            userViewModel.UserImage = fileName;
                            userViewModel.Id = returnModel.Id;
                            
                            //Updating UserProfile image name;
                            bool status = new UserBal().SaveOrUpdateUser(userViewModel);                          
                        }
                    }
                    TempData["Status"] = "User has been added successfully.";
                }
            }
         
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult DeleteUser(long id)
        {
            bool status = new UserBal().DeleteUser(Convert.ToInt64(id));
            if (status)
            {
                string ProfileImagePath = new UserBal().GetUserById(id).UserImage;
                if (!string.IsNullOrEmpty(ProfileImagePath))
                {
                    string fileSavePath = Path.Combine(Server.MapPath("~/ProfileImages/"), ProfileImagePath);
                    //Check whether file is exist or not on location
                    if (System.IO.File.Exists(fileSavePath))
                    {
                        System.IO.File.Delete(fileSavePath);
                    }
                }       
                return Json(new { Status = "S", Message = "Records has been deleted successfully." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = "F", Message = "Some error has occurred" }, JsonRequestBehavior.AllowGet);        
        }
        public ActionResult IsValidEmailAddress(string emailAddress)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(emailAddress);
                return Json(new { Status = "Success", Message = "Successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = "Error", Message = "EmailId Not a valid" }, JsonRequestBehavior.AllowGet);
            }
          
        }
    }
}