using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.Common.Enums;
using ReviewMe.Model;

namespace ReviewMe.ViewModel
{
    public class NotificationsViewModel : EntityBaseView
    {
        public string NotificationMessage { get; set; }
        public long UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ViewedOn { get; set; }
        public NotificationType NotificationType { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual UserViewModel CreatedByUser { get; set; }
    }
}
