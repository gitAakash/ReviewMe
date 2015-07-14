using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using ReviewMe.Bal;
using ReviewMe.Common.Authorization;
using ReviewMe.ViewModel;
using ReviewMe.Web.Attributes;

namespace ReviewMe.Web.Controllers
{
    [ReviewMeAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           /* DateTimeFormatInfo months = new DateTimeFormatInfo();
            for (int i = 0; i < 13; i++)
            {
                ViewBag.Months.Add(new SelectList(months.GetMonthName(i),i.ToString()));
            }

            for (int i = 0; i < UPPER; i++)
            {
                
            }
*/
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

        public JsonResult GetRatingsByMonth(int month, int year)
        {
            long reviewerId = SessionManager.GetCurrentlyLoggedInUserId();
            //DateTime RevieweeDate = Convert.ToDateTime(revieweeDate);
          //  RevieweeDate = RevieweeDate.AddDays(1);
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            var reviewDetails = new ReviewMapBal().GetReviewDetailsByUserId(reviewerId, startDate, endDate);
            //return Json(new { status = "S", Result = reviewDetailsViewModel.ReviewDetailsViewModelList }, JsonRequestBehavior.AllowGet);
            return Json(new { reviewDetails }, JsonRequestBehavior.AllowGet);
        }
    }
}