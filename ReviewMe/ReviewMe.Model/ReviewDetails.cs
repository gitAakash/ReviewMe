using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.Model
{
   public class ReviewDetails : EntityBase
    {
       public long ReviewerId { get; set; }
       public long RevieweeId { get; set; }
   
       public string Comment { get; set; }   
       public string Title { get; set; }

       public DateTime ReviewDate { get; set; }

       public decimal CodingStandardRating { get; set; }
       public decimal ProjectArchitecture { get; set; }
       public decimal CodeOptimizationRating { get; set; }
       public decimal QueryOptimizationRating { get; set; }

       public virtual User Reviewer { get; set; }
       public virtual User Reviewee { get; set; }
    }
}
