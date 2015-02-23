using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace ReviewMe.Model
{
    public class Role : EntityBase
    {
        public Role()
        {
            Users = new List<User>();
        }
        [Required]
        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}