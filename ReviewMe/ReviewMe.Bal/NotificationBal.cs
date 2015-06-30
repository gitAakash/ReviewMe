using ReviewMe.Common.Authorization;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.Bal
{
    public class NotificationBal
    {
        private readonly Repository<Notifications> _notificationsRepository = new Repository<Notifications>(new EntityContext());

        /// <summary>
        /// Addded By   : Ramchandra rane
        /// Description : Save Notification
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns></returns>
        public bool AddNewNotification(Notifications notifications)
        {
            try
            {
                var model = _notificationsRepository.Add(notifications);
                if (model != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Added By : Ramchandra Rane
        // Get All Reviews
        public IEnumerable<Notifications> GetAllNotificationByUserId()
        {
            try
            {
                //List<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r=>r.IsActive==true && r.UserId== SessionManager.GetCurrentlyLoggedInUserId()).OrderBy(r=>r.CreatedOn).ToList();

                IEnumerable<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r=>r.UserId== SessionManager.GetCurrentlyLoggedInUserId() && r.IsActive==true).OrderByDescending(r=>r.CreatedOn);
                return notificationlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Read User Notification

        public bool ReadUserNotifications(int Id)
        {
            try
            {
                IEnumerable<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r => r.UserId == SessionManager.GetCurrentlyLoggedInUserId() && r.IsActive == true).OrderByDescending(r => r.CreatedOn).Take(3);

                if(Id==0)
                {
                    foreach (Notifications notification in notificationlist)
                    {
                        notification.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                        notification.ModifiedOn = DateTime.Now;
                        notification.ViewedOn = DateTime.Now;
                        notification.IsRead = true;
                        _notificationsRepository.SaveOrUpdate(notification);
                    }
                }
                else
                {
                    Notifications notification = _notificationsRepository.GetAll().SingleOrDefault(r => r.Id == Id && r.IsRead == false);
                    if(notification != null)
                    {
                        notification.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                        notification.ModifiedOn = DateTime.Now;
                        notification.ViewedOn = DateTime.Now;
                        notification.IsRead = true;
                        _notificationsRepository.SaveOrUpdate(notification);
                    }

                }
               
                return true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ReadAllUserNotifications()
        {
            try
            {
                IEnumerable<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r => r.UserId == SessionManager.GetCurrentlyLoggedInUserId() && r.IsActive == true && r.IsRead==false).OrderByDescending(r => r.CreatedOn);

                foreach (Notifications notification in notificationlist)
                {
                    notification.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    notification.ModifiedOn = DateTime.Now;
                    notification.ViewedOn = DateTime.Now;
                    notification.IsRead = true;
                    _notificationsRepository.SaveOrUpdate(notification);

                }
                return true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
