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
    public class TechnologyController : Controller
    {
        //
        // GET: /Technology/
        public ActionResult Index(Int64? id)
        {
            ViewBag.Status = TempData["Status"];
            TechnologyViewModelLong technologyViewModelLong = new TechnologyBal().GetAllTechnologies();
            technologyViewModelLong.TechnologyViewModel = new TechnologyViewModel();
            if (id != null && id != 0)
            {
                technologyViewModelLong.TechnologyViewModel = new TechnologyBal().GetTechnologyById(Convert.ToInt64(id));
            }
            return View(technologyViewModelLong);
        }
        [HttpGet]
        public ActionResult SearchTechnology(string strSearch)
        {

            TechnologyViewModelLong technologyViewModelLong = new TechnologyBal().GetAllTechnologies();
            int aa = technologyViewModelLong.TechnologyViewModelList.Count();
            List<TechnologyViewModel> technologyViewModel = new List<TechnologyViewModel>();
            if (!string.IsNullOrEmpty(strSearch))

                technologyViewModel = (List<TechnologyViewModel>)technologyViewModelLong.TechnologyViewModelList.Where(p => (p.TechnologyName!=null && p.TechnologyName.Contains(strSearch) )).ToList();

            technologyViewModelLong.TechnologyViewModelList = technologyViewModel;


            return PartialView("SearchTechnology", technologyViewModelLong);

        }
        [HttpPost]
        public ActionResult AddEditTechnology(TechnologyViewModel technologyViewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["Status"] = "Opps! Some error has occurred";
                if (technologyViewModel.Id != 0)
                {
                    TempData["Status"] = "Technology has been updated successfully.";
                    bool status = new TechnologyBal().SaveOrUpdateTechnology(technologyViewModel);
                }
                else
                {
                    TempData["Status"] = "Technology has been added successfully.";
                    bool status = new TechnologyBal().AddTechnology(technologyViewModel);
                }
            }
            return RedirectToAction("Index", "Technology");
        }

        [HttpPost]
        public string DeleteTechnology(long id)
        {
            var status = new TechnologyBal().DeleteTechnology(Convert.ToInt64(id));
            if (status)
                return "Technology deleted successfully.";
            else
                return "Some error has occurred while deleting";
        }
	}
}