using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewMe.Bal;
using ReviewMe.ViewModel;
using ReviewMe.Web.Attributes;

namespace ReviewMe.Web.Controllers
{
    [ReviewMeAuthorize]
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        public ActionResult Index(Int64? id)
        {
            ViewBag.Status = TempData["Status"];
            ProjectViewModelLong projectViewModelLong = new ProjectBal().GetAllProjects();
            projectViewModelLong.ProjectViewModel = new ProjectViewModel();
            if (id != null && id != 0)
            {
                projectViewModelLong.ProjectViewModel = new ProjectBal().GetProjectById(Convert.ToInt64(id));
                ViewBag.EditMode = true;
            }
            else { ViewBag.EditMode = false; }
            return View(projectViewModelLong);
        }

        [HttpPost]
        public ActionResult AddEditProject(ProjectViewModel projectViewModel)
        {
            TempData["Status"] = "Opps! Some error has occurred";
            if (ModelState.IsValid)
            {
                if (projectViewModel.Id != 0)
                {
                    TempData["Status"] = "Project has been updated successfully.";
                    bool status = new ProjectBal().SaveOrUpdateProject(projectViewModel);
                }
                else
                {
                    TempData["Status"] = "Project has been added successfully.";
                    bool status = new ProjectBal().AddProject(projectViewModel);
                }
            }
            return RedirectToAction("Index", "Project");
        }
        [HttpGet]
        public ActionResult SearchProject(string strSearch)
        {
          
            ProjectViewModelLong projectViewModelLong = new ProjectBal().GetAllProjects();
            int aa = projectViewModelLong.ProjectViewModelList.Count();
            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            if (!string.IsNullOrEmpty(strSearch))

                projectViewModel = (List<ProjectViewModel>)projectViewModelLong.ProjectViewModelList.Where(p => (p.ProjectTitle != null && p.ProjectTitle.Contains(strSearch)) || (p.Description != null && p.Description.Contains(strSearch))).ToList();

            projectViewModelLong.ProjectViewModelList = projectViewModel;


            return PartialView("SearchProject", projectViewModelLong);

        }
        [HttpPost]
        public ActionResult DeleteProject(long id)
        {
            var status = new ProjectBal().DeleteProject(Convert.ToInt64(id));
            if (status)
                return Json(new { Status = "S", Message = "Project has been deleted successfully." }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Status = "F", Message = "Some error has occurred" }, JsonRequestBehavior.AllowGet);        
        }
	}
}