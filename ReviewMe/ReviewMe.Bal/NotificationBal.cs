using ReviewMe.Common.Authorization;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.ViewModel;

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
        public long AddNewNotification(NotificationsViewModel notifications)
        {
            var notificationModel = new Notifications();
            try
            {
                if (notifications != null)
                {
                    notificationModel.CreatedBy = notifications.CreatedBy;
                    notificationModel.IsActive = notifications.IsActive;
                    notificationModel.UserId = notifications.UserId;
                    notificationModel.NotificationMessage = notifications.NotificationMessage;
                    notificationModel.ReviewForDate = notifications.ReviewForDate;
                    //notificationModel.NotificationType = notifications.NotificationType;
                    notificationModel.IsRead = notifications.IsRead;
                    notificationModel.NotificationType = (int)notifications.NotificationType;
                    notificationModel.CreatedOn = DateTime.Now;
                }
                var model = _notificationsRepository.Add(notificationModel);
                if (model != null)
                    return model.Id;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Added By : Ramchandra Rane
        // Get All Reviews
        public IEnumerable<NotificationsViewModel> GetAllNotificationByUserId()
        {
            try
            {
                //List<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r=>r.IsActive==true && r.UserId== SessionManager.GetCurrentlyLoggedInUserId()).OrderBy(r=>r.CreatedOn).ToList();

                IEnumerable<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r => r.UserId == SessionManager.GetCurrentlyLoggedInUserId() && r.IsActive == true).OrderByDescending(r => r.CreatedOn);
                if (notificationlist.Any())
                {
                    var notificationViewModel = new List<NotificationsViewModel>();
        
                    foreach (var notificationse in notificationlist)
                    {
                        var notification = new NotificationsViewModel();
                        notification.UserId = notificationse.UserId;
                        notification.ViewedOn = notificationse.ViewedOn;
                        notification.CreatedOn = notificationse.CreatedOn;
                        notification.NotificationType = notificationse.NotificationType;
                        notification.NotificationMessage = notificationse.NotificationMessage;
                        notification.IsRead = notificationse.IsRead;
                        notification.ReviewForDate = notificationse.ReviewForDate;

                        notification.CreatedByUser = new UserViewModel();
                        notification.CreatedByUser.UserImage = notificationse.CreatedByUser.UserImage;
                        notification.CreatedByUser.SelectedTechnologyId = notificationse.CreatedByUser.TechnologyId;
                        notification.CreatedByUser.SelectedTeamLeadId = notificationse.CreatedByUser.TeamLeaderId;
                        notification.CreatedByUser.SelectedRoleId = notificationse.CreatedByUser.RoleId;
                        notification.CreatedByUser.RoleName = notificationse.CreatedByUser.Role.RoleName;
                        notification.CreatedByUser.ResetPassword = notificationse.CreatedByUser.ResetPassword;
                        notification.CreatedByUser.Rating = notificationse.CreatedByUser.Rating;
                        notification.CreatedByUser.Password = notificationse.CreatedByUser.Password;
                        notification.CreatedByUser.OnTask = notificationse.CreatedByUser.OnTask;
                        notification.CreatedByUser.OnProject = notificationse.CreatedByUser.OnProject;
                        notification.CreatedByUser.OnClient = notificationse.CreatedByUser.OnClient;
                        notification.CreatedByUser.MobileNo = notificationse.CreatedByUser.MobileNo;
                        notification.CreatedByUser.MName = notificationse.CreatedByUser.MName;
                        notification.CreatedByUser.LName = notificationse.CreatedByUser.LName;
                        notification.CreatedByUser.FName = notificationse.CreatedByUser.FName;
                        notification.CreatedByUser.Gender = notificationse.CreatedByUser.Gender;
                        notificationViewModel.Add(notification);
                    }

                    return notificationViewModel;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new List<NotificationsViewModel>();
        }


        public IEnumerable<NotificationsViewModel> GetAllNotification(bool status)
        {
            try
            {
                //List<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r=>r.IsActive==true && r.UserId== SessionManager.GetCurrentlyLoggedInUserId()).OrderBy(r=>r.CreatedOn).ToList();

                IEnumerable<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r => r.IsActive == status).OrderByDescending(r => r.CreatedOn);
                if (notificationlist.Any())
                {
                    var notificationViewModel = new List<NotificationsViewModel>();
                    var notification = new NotificationsViewModel();
                    foreach (var notificationse in notificationlist)
                    {
                        notification.UserId = notificationse.UserId;
                        notification.ViewedOn = notificationse.ViewedOn;
                        notification.CreatedOn = notificationse.CreatedOn;
                        notification.NotificationType = notificationse.NotificationType;
                        notification.NotificationMessage = notificationse.NotificationMessage;
                        notification.IsRead = notificationse.IsRead;
                        notification.ReviewForDate = notificationse.ReviewForDate;

                        notification.CreatedByUser = new UserViewModel();
                        notification.CreatedByUser.UserImage = notificationse.CreatedByUser.UserImage;
                        notification.CreatedByUser.SelectedTechnologyId = notificationse.CreatedByUser.TechnologyId;
                        notification.CreatedByUser.SelectedTeamLeadId = notificationse.CreatedByUser.TeamLeaderId;
                        notification.CreatedByUser.SelectedRoleId = notificationse.CreatedByUser.RoleId;
                        notification.CreatedByUser.RoleName = notificationse.CreatedByUser.Role.RoleName;
                        notification.CreatedByUser.ResetPassword = notificationse.CreatedByUser.ResetPassword;
                        notification.CreatedByUser.Rating = notificationse.CreatedByUser.Rating;
                        notification.CreatedByUser.Password = notificationse.CreatedByUser.Password;
                        notification.CreatedByUser.OnTask = notificationse.CreatedByUser.OnTask;
                        notification.CreatedByUser.OnProject = notificationse.CreatedByUser.OnProject;
                        notification.CreatedByUser.OnClient = notificationse.CreatedByUser.OnClient;
                        notification.CreatedByUser.MobileNo = notificationse.CreatedByUser.MobileNo;
                        notification.CreatedByUser.MName = notificationse.CreatedByUser.MName;
                        notification.CreatedByUser.LName = notificationse.CreatedByUser.LName;
                        notification.CreatedByUser.FName = notificationse.CreatedByUser.FName;
                        notification.CreatedByUser.Gender = notificationse.CreatedByUser.Gender;
                        notificationViewModel.Add(notification);
                    }

                    return notificationViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new List<NotificationsViewModel>();
        }
        // Read User Notification

        public bool ReadUserNotifications(int Id)
        {
            try
            {
                IEnumerable<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r => r.UserId == SessionManager.GetCurrentlyLoggedInUserId() && r.IsActive == true).OrderByDescending(r => r.CreatedOn).Take(3);

                if (Id == 0)
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
                    if (notification != null)
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
                IEnumerable<Notifications> notificationlist = _notificationsRepository.GetAll().Where(r => r.UserId == SessionManager.GetCurrentlyLoggedInUserId() && r.IsActive == true && r.IsRead == false).OrderByDescending(r => r.CreatedOn);

                foreach (Notifications notification in notificationlist)
                {
                    notification.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    notification.ModifiedOn = DateTime.Now;
                    notification.ViewedOn = DateTime.Now;
                    notification.IsRead = true;
                    _notificationsRepository.SaveOrUpdate(notification);

                }
                return true; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
