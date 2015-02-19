using System;

namespace ReviewMe.Model
{
    public class EntityBase : ISoftDelete
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}