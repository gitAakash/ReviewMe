using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewMe.Bal;

namespace ReviewMe.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            var userBal = new UserBal();
            //List<User> allUsers = userBal.GetAllUsers();
            //var user = new User();
            //user.FName = "abc";
            //user.LName = "abc";
            //user.Dob = DateTime.Now;
            //user.Gender = true;
            //user.TechnologyId = 1;
            //user.TeamLeaderId = 1;
            //user.RoleId = 1;
            //user.OnClient = false;
            //user.OnProject = true;
            //user.OnTask = true;
            //user.CreatedBy = 1;
            //user.CreatedOn = DateTime.Now;
            //user.ModifiedBy = 1;
            //user.ModifiedOn = DateTime.Now;

            //var response = userBal.AddUser(user);
            return View();
        }
	}
}