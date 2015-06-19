using System.Diagnostics;
using System.IO;
using ReviewMe.Bal;
using ReviewMe.Common.Extensions;
using ReviewMe.Model;
using ReviewMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewMe.Web.Controllers
{
    public class ReviewMapController : Controller
    {
        //
        // GET: /ReviewMap/
        public ActionResult Index()
        {
            List<ReviewGroupViewModel> lstReviewGroupViewModel;
            lstReviewGroupViewModel = new ReviewMapBal().GetReviewGroupDetails();
            return View(lstReviewGroupViewModel);
        }

        [HttpGet]
        public ActionResult AddEditGroup(Int64? revId)
        {
            ReviewMapViewModel reviewMapViewModel;

            if (revId != null && revId != 0)
            {
                reviewMapViewModel = new ReviewMapBal().GetEditReviewMapById(Convert.ToInt64(revId));
            }
            else
            {
                reviewMapViewModel = new ReviewMapBal().GetAddReviewMapDetails();
            }

            return PartialView("AddEditGroup", reviewMapViewModel);
        }


        [HttpPost]
        public ActionResult AddEditGroup(ReviewMapViewModel reviewMapViewModel)
        {
            if (ModelState.IsValid)
            {
                if (reviewMapViewModel.IsEdit == "1")
                {
                    bool status = new ReviewMapBal().EditReviewMap(reviewMapViewModel);
                }
                else
                {
                    bool status = new ReviewMapBal().AddReviewMap(reviewMapViewModel);
                }
            }
            return RedirectToAction("Index", "ReviewMap");
        }

        [HttpGet]
        public ActionResult GetRevieweeList(string id, string IsEdit)
        {
            Int64 ReviewerId = Convert.ToInt64(id);
            ReviewMapViewModel reviewMapViewModel = new ReviewMapViewModel();

            reviewMapViewModel = new ReviewMapBal().GetRevieweeBalList(ReviewerId, IsEdit);
            return PartialView("GetRevieweeList", reviewMapViewModel);
        }

        [HttpPost]
        public bool DeleteGroup(string id)
        {
            try
            {
                bool response = new ReviewMapBal().DeleteUserMapByReviewerId(Convert.ToInt64(id));
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult GetRevieweeListByReviewerId(string id)
        {
            Int64 ReviewerId = Convert.ToInt64(id);
            /*ReviewMapViewModel reviewMapViewModel = new ReviewMapViewModel();*/

           var reviewMapViewModel = new ReviewMapBal().GetRevieweeByReviewerId(ReviewerId);
           return View("GetRevieweeListByReviewerId", reviewMapViewModel);
        }

        [HttpGet]
        public ActionResult ReviewDetails(long revieweeId)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddDayReviewDetails(Int64? userId)
        {
            return PartialView("AddDayReviewDetails");
        }

        [HttpPost]
        public ActionResult AddDayReviewDetails(ReviewDetailsViewModel reviewDetailsViewModel)
        {
           // bool status = new ReviewMapBal().SaveOrUpdateUser(reviewDetailsViewModel);
            return RedirectToAction("GetRevieweeListByReviewerId");
        }
    }
}