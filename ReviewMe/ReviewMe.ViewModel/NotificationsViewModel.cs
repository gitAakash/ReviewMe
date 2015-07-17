using System;

namespace ReviewMe.ViewModel
{
    public class NotificationsViewModel : EntityBaseView
    {
        public string NotificationMessage { get; set; }
        public long UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ViewedOn { get; set; }
        public DateTime ReviewForDate { get; set; }
        public int NotificationType { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual UserViewModel CreatedByUser { get; set; }
    }
}