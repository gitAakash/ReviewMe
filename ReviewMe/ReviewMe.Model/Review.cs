using System;
using System.Collections.Generic;

namespace ReviewMe.Model
{
    public class Review : EntityBase
    {
        public Review() 
        {
            Comments = new List<Comment>();
        }

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

        public virtual User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
