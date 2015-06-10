using System.Data.Entity;
using ReviewMe.Model;

namespace ReviewMe.DataAccess
{
    public interface IEntityContext
    {
        IDbSet<Role> Roles { get; set; }
        IDbSet<Review> Remarks { get; set; }
        IDbSet<Project> Projects { get; set; }
        IDbSet<Comment> Comments { get; set; }
        IDbSet<Technology> Technologies { get; set; }
        //IDbSet<ReviewTechnology> ReviewTechnologies { get; set; }
       // IDbSet<ReviewSetting> ReviewSettings { get; set; }
        IDbSet<User> Users { get; set; }
        void Add<T>(T obj) where T : EntityBase;
        void Save();
    }
}