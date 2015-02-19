using System.Collections.Generic;
using System.Configuration;

namespace ReviewMe.Model
{
    public class Role : EntityBase
    {
        public Role()
        {
            Users = new List<User>();
        }

        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}