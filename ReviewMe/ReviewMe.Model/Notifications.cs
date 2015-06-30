using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.Model
{
    public class Notifications :EntityBase
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
