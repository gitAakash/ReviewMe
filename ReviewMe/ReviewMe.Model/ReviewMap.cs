using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.Model
{
    public class ReviewMap : EntityBase
    {
        public ReviewMap()
        {
            
        }

        public Int64 ReviewerId { get; set; }
        public Int64 DevloperId { get; set; }

        public virtual User ReviewerUser { get; set; }
        public virtual User DeveloperUser { get; set; }
        

    }
}
