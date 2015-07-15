using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using ReviewMe.Bal;
using ReviewMe.Common.Authorization;
using ReviewMe.Common.Extensions;
using ReviewMe.ViewModel;
using ReviewMe.Web.Attributes;

namespace ReviewMe.Web.Controllers
{
    [ReviewMeAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var a = new TechnologyBal().GetAllTechnologies();
           
            ViewBag.Technologies = a.TechnologyViewModelList.ToList().Select(c => new SelectListItem         
                            {
                               Text = c.TechnologyName,
                               Value = c.Id.ToString(),
                            }).ToList();

         return View();   
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public JsonResult GetMyRatingStats()
        {
            try
            {
                var reviewMapBal = new ReviewMapBal();
                var id = SessionManager.GetCurrentlyLoggedInUserId();
                var reviewDetails = reviewMapBal.GetReviewDetailsByUserId(id, DateTime.Now, DateTime.Now); // modified

                return Json(new { reviewDetails }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public JsonResult GetRatingsByMonth(int month, int year, long? id)
        {
            long reviewerId = SessionManager.GetCurrentlyLoggedInUserId();
            if ( id != null)
                reviewerId = Convert.ToInt64(id);
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            var reviewDetails = new ReviewMapBal().GetReviewDetailsByUserId(reviewerId, startDate, endDate);
            return Json(new { reviewDetails }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsersByTechnology(string id)
        {
            var userBal = new UserBal();
            var usersList = userBal.GetUsersByTechnology(Convert.ToInt64(id));
            return Json(new{usersList},JsonRequestBehavior.AllowGet);
        }
    }
}