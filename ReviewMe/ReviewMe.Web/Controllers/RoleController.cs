using System;
using System.Web.Mvc;
using ReviewMe.Bal;
using ReviewMe.ViewModel;

namespace ReviewMe.Web.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/
        public ActionResult Index(Int64? id)
        {
            RoleViewModelLong roleViewModelLong = new RoleBal().GetAllRoles();
            roleViewModelLong.RoleViewModel = new RoleViewModel();
            if (id != null)
            {
                roleViewModelLong.RoleViewModel = new RoleBal().GetRoleById(Convert.ToInt64(id));
            }
            return View(roleViewModelLong);
        }

        [HttpPost]
        public ActionResult AddEditRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                if (roleViewModel.Id != 0)
                    new RoleBal().SaveOrUpdateRole(roleViewModel);
                else
                    new RoleBal().AddRole(roleViewModel);
                
            }
            //var roleViewModelLong = new RoleViewModelLong()
            //{
            //    RoleViewModel = roleViewModel
            //};
            //return View(roleViewModel);
            return RedirectToAction("Index", "Role");
        }

        [HttpPost]
        public string DeleteRole(long id)
        {
            var status = new RoleBal().DeleteRole(Convert.ToInt64(id));
            if (status)
                return "Data deleted successfully.";
            else
                return "Some error has occurred";
        }
    }
}