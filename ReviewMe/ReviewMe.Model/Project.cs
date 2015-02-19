using System.Collections.Generic;

namespace ReviewMe.Model
{
    public class Project : EntityBase
    {
        public Project()
        {
            Users = new List<User>();
        }

        public long UserId { get; set; }
        public string ProjectTitle { get; set; }

        public virtual User User { get; set; }

        public List<User> Users { get; set; }
    }
}