namespace ReviewMe.Model
{
    public class Comment : EntityBase
    {
        public long UserId { get; set; }
        public long ReviewId { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public virtual Review Review { get; set; }
    }
}   