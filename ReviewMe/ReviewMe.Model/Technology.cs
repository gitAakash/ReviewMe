using System.Collections.Generic;

namespace ReviewMe.Model
{
    public class Technology : EntityBase
    {
        public Technology()
        {
            Users = new List<User>();
        }

        public string TechnologyName { get; set; }
     
        public List<User> Users { get; set; }
    }
}