using ReviewMe.Bal;
using ReviewMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReviewMe.ViewModel;

namespace ReviewMe.Web.Controllers
{
    public class NotificationController : Controller
    {
        //
        // GET: /Notification/
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<NotificationsViewModel> notificationList = new NotificationBal().GetAllNotificationByUserId();
            new NotificationBal().ReadAllUserNotifications();

            return View(notificationList);
        }

        [HttpGet]
        public ActionResult UsersNotifications()
        {
            IEnumerable<NotificationsViewModel> notificationList = new NotificationBal().GetAllNotificationByUserId();
            return PartialView(notificationList);
        }

      [HttpGet]
        public ActionResult ReadUserNotification(string Id)
        {
            new NotificationBal().ReadUserNotifications(Convert.ToInt32(Id));
            return null;
        }
    }
}