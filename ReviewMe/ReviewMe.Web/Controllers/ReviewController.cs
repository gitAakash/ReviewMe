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
    //[ReviewMeAuthorize]
    public class ReviewController : Controller
    {
        //
        // GET: /Review/
        public ActionResult Index(Int64? id)
        {
            ReviewViewModelLong reviewViewModelLong = new ReviewBal().GetAllReviews();
            reviewViewModelLong.ReviewViewModel = new ReviewViewModel();
            if (id != null && id != 0)
            {
                reviewViewModelLong.ReviewViewModel = new ReviewBal().GetReviewById(Convert.ToInt64(id));
            }
            return View(reviewViewModelLong);
        }

        [HttpPost]
        public ActionResult AddEditReview(ReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                if (reviewViewModel.Id != 0)
                {
                    bool status = new ReviewBal().SaveOrUpdateReview(reviewViewModel);
                }
                else
                {
                    bool status = new ReviewBal().AddReview(reviewViewModel);
                }
            }
            return RedirectToAction("Index", "Review");
        }

        [HttpPost]
        public string DeleteReview(long id)
        {
            var status = new ReviewBal().DeleteReview(Convert.ToInt64(id));
            if (status)
                return "Data deleted successfully.";
            else
                return "Some error has occurred";
        }

        public ActionResult FullCalendar()
        {

            return View();
        }
    }
}