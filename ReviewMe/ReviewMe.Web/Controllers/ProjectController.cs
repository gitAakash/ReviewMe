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
            if (ModelState.IsValid)
            {
                if (projectViewModel.Id != 0)
                {
                    bool status = new ProjectBal().SaveOrUpdateProject(projectViewModel);
                }
                else
                {
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
        public string DeleteProject(long id)
        {
            var status = new ProjectBal().DeleteProject(Convert.ToInt64(id));
            if (status)
                return "Data deleted successfully.";
            else
                return "Some error has occurred";
        }
	}
}