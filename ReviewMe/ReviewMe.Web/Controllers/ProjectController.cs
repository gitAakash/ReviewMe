using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewMe.Bal;
using ReviewMe.ViewModel;

namespace ReviewMe.Web.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        public ActionResult Index(Int64? id)
        {
            ProjectViewModelLong projectViewModelLong = new ProjectBal().GetAllProjects();
            projectViewModelLong.ProjectViewModel = new ProjectViewModel();
            if (id != null)
            {
                projectViewModelLong.ProjectViewModel = new ProjectBal().GetProjectById(Convert.ToInt64(id));
            }
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