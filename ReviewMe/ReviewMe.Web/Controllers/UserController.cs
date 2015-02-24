using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewMe.Bal;
using ReviewMe.ViewModel;

namespace ReviewMe.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: /User/
        public ActionResult Index(Int64? id)
        {
            UserViewModelLong userViewModelLong = new UserBal().GetAllUsers();
            userViewModelLong.UserViewModel = new UserViewModel();
            if (id != null)
            {
                userViewModelLong.UserViewModel = new UserBal().GetUserById(Convert.ToInt64(id));
            }
            return View(userViewModelLong);
        }

        [HttpPost]
        public ActionResult AddEditUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
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
            var status = new UserBal().DeleteUser(Convert.ToInt64(id));
            if (status)
                return "Data deleted successfully.";
            else
                return "Some error has occurred";
        }
	}
}