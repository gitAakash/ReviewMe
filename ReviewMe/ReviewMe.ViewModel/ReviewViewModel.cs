using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class ReviewViewModel:EntityBaseView
    {
        public long UserId { get; set; }
        public DateTime ReviewDate { get; set; }
        public long ProjectId { get; set; }
        public string ModuleName { get; set; }
        public string FileReviewed { get; set; }
        public string MethodsReviewed { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public decimal CodingStandardRating { get; set; }
        public decimal ProjectArchitecture { get; set; }
        public decimal CodeOptimizationRating { get; set; }
        public decimal QueryOptimizationRating { get; set; }
    }
}
