using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using ReviewMe.Common.Enums;

namespace ReviewMe.Common.Helpers
{
    public class UserRoleHelper
    {
        public static UserRoleEnum GetUserRole(string roleName)
        {
            switch (roleName)
            {
                case "Admin":
                    return UserRoleEnum.Admin;
                case "TeamLeader":
                    return UserRoleEnum.TeamLeader;
                case "Developer":
                    return UserRoleEnum.Developer;
                default:
                    return UserRoleEnum.Developer;
            }
        }
    }
}
