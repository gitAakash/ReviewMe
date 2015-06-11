﻿using ReviewMe.Bal;
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
            return View();
        }

        [HttpGet]
        public ActionResult AddEditGroup()
        {
            ReviewMapViewModel reviewMapViewModel;

            reviewMapViewModel = new ReviewMapBal().GetAddReviewMapDetails();

            return PartialView("AddEditGroup", reviewMapViewModel);
        }

        [HttpPost]
        public ActionResult AddEditGroup(ReviewMapViewModel reviewMapViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = new ReviewMapBal().AddReviewMap(reviewMapViewModel);
            }
            return RedirectToAction("Index", "ReviewMap");
        }
	}
}