using System.ComponentModel;

namespace ReviewMe.Common.Enums
{
    public static class NotificationEnum
    {
        public static string ReviewerNotification = "You have to do code review of @RevieweeName's for dated ";
        public static string RevieweeNotification = "@ReviewerName will do your code review of dated";
        public static string ReviewAddedToReviwee = "{0} has added your's review of code  of dated {1}";
        public static string ReviewEditedToReviwee = "{0} has updated your's review of code  of dated {1}";
    }

    public enum NotificationType
    {
        [Description("have to do code review")] NotifyReviwer = 1,
        [Description("code will be reviewed ")] NotifyReviewee = 2,
        [Description("added your code review of")] AddedReview = 3,
        [Description("edited your code review of")] EditedReview = 4
    }
}