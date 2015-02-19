using System;
using System.Collections.Generic;

namespace ReviewMe.Model
{
    public class ReviewSetting : EntityBase
    {
        public long UserToBeReviewedId { get; set; }
        public long ReviewerId { get; set; }
        public DateTime ReviewUpTo { get; set; }

        public virtual User UserToBeReviewed { get; set; }
        public virtual User Reviewer { get; set; }
        
    }
}