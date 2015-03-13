using System;
using ReviewMe.Common.Enums;

namespace ReviewMe.Common.Authorization
{
    public class SessionInformation
    {
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Int64 UserRoleId { get; set; }
        public UserRoleEnum UserRole { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string UserLogo { get; set; }
        //public bool CanCreateUser { get; set; }
        //public bool CanDeleteUser { get; set; }
        public Int64? TechnologyId { get; set; }
        public Int64 TeamLeaderId { get; set; }
        //public Int64 NotificationCount { get; set; }
    }
}