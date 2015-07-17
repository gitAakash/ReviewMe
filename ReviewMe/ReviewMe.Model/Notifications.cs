using System;

namespace ReviewMe.Model
{
    public class Notifications : EntityBase
    {
        public string NotificationMessage { get; set; }
        public long UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ViewedOn { get; set; }
        public int NotificationType { get; set; }
        public virtual User User { get; set; }
        public virtual User CreatedByUser { get; set; }
    }
}