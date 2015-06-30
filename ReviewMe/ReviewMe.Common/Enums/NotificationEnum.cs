using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.Common.Enums
{
    public static class NotificationEnum
    {
        public static string ReviewerNotification= "You have do code review of @RevieweeName's for dated ";
        public static string RevieweeNotification = "@ReviewerName will do your code review of dated";
        public static string ReviewAddedToReviwee = "{0} have added your's review of code  of dated {1}"; 


        //RevieweeNotification = "@ReviewerName will do your code review of dated "
        ////ReviewAdded          = "@ReviewerName have added review of dated "
        //ReviewAdded          = "{0} have added review of dated "
    }
}
