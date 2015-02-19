namespace ReviewMe.Model
{
    public class ReviewTechnology : EntityBase
    {
        public long RemarkId { get; set; }
        public long TechnologyId { get; set; }

        public virtual Review Remark { get; set; }
        public virtual Technology Technology { get; set; }
    }
}