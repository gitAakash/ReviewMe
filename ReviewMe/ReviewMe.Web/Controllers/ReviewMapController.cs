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
using ReviewMe.Common.Authorization;
using ReviewMe.Common.Enums;

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

        #region User Reviews

        [HttpGet]
        public ActionResult ReviewDetails(long revieweeId)
        {
            ViewBag.RevieweeId = revieweeId;
            ViewBag.RevieweeName = new UserBal().GetUserById(revieweeId).FName;
            //return View(reviewDetailsViewModel);
            return View();
        }

        /// <summary>
        /// Added by : Ramchandra Rane,24th June 2015
        /// </summary>
        /// <param name="revieweeId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMonthWiseReviewDetails(string revieweeDate, long revieweeId)
        {
            long reviewerId = SessionManager.GetCurrentlyLoggedInUserId();
            DateTime RevieweeDate = Convert.ToDateTime(revieweeDate);
            RevieweeDate = RevieweeDate.AddDays(1);
            DateTime startDate = new DateTime(RevieweeDate.Year, RevieweeDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            ReviewDetailsViewModel reviewDetailsViewModel = new ReviewMapBal().GetReviewDetailsByRevieweeId(revieweeId, reviewerId, startDate, endDate);
            return Json(new { status = "S", Result = reviewDetailsViewModel.ReviewDetailsViewModelList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddEditReviewDetails(long revieweeId, string revieweeDate, long id)
        {
            long reviewerId = SessionManager.GetCurrentlyLoggedInUserId();

            ReviewDetailsViewModel reviewDetailsViewModel = new ReviewMapBal().GetReviewDetailsById(id);
            if (reviewDetailsViewModel.ReviewDetailsViewModelList.Count > 0)
            {
                reviewDetailsViewModel.Title = reviewDetailsViewModel.ReviewDetailsViewModelList.FirstOrDefault(R => R.ReviewerId == reviewerId).Title;
                reviewDetailsViewModel.Comment = reviewDetailsViewModel.ReviewDetailsViewModelList.FirstOrDefault(R => R.ReviewerId == reviewerId).Comment;
                reviewDetailsViewModel.Id = reviewDetailsViewModel.ReviewDetailsViewModelList.FirstOrDefault(R => R.ReviewerId == reviewerId).Id;
            }

            ViewBag.RevieweeId = Convert.ToInt64(revieweeId);
            string tempRevieweeDate = revieweeDate.Split('-')[1] + "/" + revieweeDate.Split('-')[2] + "/" + revieweeDate.Split('-')[0];
            ViewBag.RevieweeDate = tempRevieweeDate;
            return PartialView("AddEditReviewDetails", reviewDetailsViewModel);

        }

        [HttpGet]
        public ActionResult DeleteReviewDetails(long Id)
        {
            try
            {
                bool response = new ReviewMapBal().DeleteReviewById(Id);
                if (response)
                {
                    return Json(new { Status = "S", Message = "Record has been deleted successfully." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = "F", Message = "Thre are some problem with server! Try Again later. . " }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return Json(new { Status = "F", Message = "Thre are some problem with server! Try Again later. . " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddEditReviewDetails(ReviewDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ReviewerId = SessionManager.GetCurrentlyLoggedInUserId();
                if (model.RevieweeId > 0)
                {
                    if (model.Id != 0)
                    {
                        bool status = new ReviewMapBal().EditReviewDetails(model);
                        if (status)
                        {

                            return Json(new { Status = "S", Message = "Review has been updated successfully." }, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            return Json(new { Status = "F", Message = "Thre are some problem with server! Try Again later" }, JsonRequestBehavior.AllowGet);
                        }

                        //return RedirectToAction("ReviewDetails", "ReviewMap", new { revieweeId = model.RevieweeId });
                    }
                    else
                    {
                        bool status = new ReviewMapBal().AddReviewDetails(model);
                        //return RedirectToAction("ReviewDetails", "ReviewMap", new { revieweeId = model.RevieweeId });
                        if (status)
                        {
                            Notifications notifications = new Notifications();
                            notifications.CreatedBy = SessionManager.GetCurrentlyLoggedInUserId();
                            notifications.CreatedOn = System.DateTime.Now;
                            notifications.IsActive = true;
                            notifications.IsRead = false;
                            //notifications.NotificationType = 1;
                            notifications.UserId = model.RevieweeId;
                            notifications.NotificationMessage = string.Format(NotificationEnum.ReviewAddedToReviwee, SessionManager.GetSessionInformation().FullName, model.ReviewDate.ToShortDateString());

                            status = new NotificationBal().AddNewNotification(notifications);
                            return Json(new { Status = "S", Message = "Review has been added successfully." }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { Status = "F", Message = "Thre are some problem with server! Try Again later" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    //Error : RevieweeId cann't pass as zero
                    return Json(new { Status = "F", Message = "RevieweeId can't be zero." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Status = "F", Message = "Model is invalid" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Added By    : Ramchandra Rane,26th June 2015
        /// Description : For displaying only login Users review.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMyReviewList()
        {
            long reviewerId = SessionManager.GetCurrentlyLoggedInUserId();

            DateTime RevieweeDate = System.DateTime.Now;
            //RevieweeDate = RevieweeDate.AddDays(1);
            DateTime startDate = new DateTime(RevieweeDate.Year, RevieweeDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            ReviewDetailsViewModel reviewDetailsViewModel = new ReviewMapBal().GetAllReviewDetailsByRevieweeId(reviewerId, startDate, endDate);
            //return Json(new { status = "S", Result = reviewDetailsViewModel.ReviewDetailsViewModelList }, JsonRequestBehavior.AllowGet);
            return View(reviewDetailsViewModel);
        }

        /// <summary>
        /// Added By    : Ramchandra Rane
        /// Description : View for displaying details of the review
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewReviewDetails(long Id)
        {
            ReviewDetailsViewModel reviewDetailsViewModel = null;
            try
            {
                reviewDetailsViewModel = new ReviewMapBal().GetReviewDetailsById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(reviewDetailsViewModel);
        }

        #endregion
    }
}