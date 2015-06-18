using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using ReviewMe.Model;

namespace ReviewMe.DataAccess
{
    public class EntityContext : DbContext, IEntityContext
    {
        static EntityContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EntityContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<EntityContext>());
        }

       public EntityContext()
            : base("Name=NeoDailyReviewsContext")
        {
           // Database.SetInitializer(new EntityContextInitializer()<EntityContext>());
            Database.SetInitializer<EntityContext>(new MigrateDatabaseToLatestVersion<EntityContext, DataAccess.Migrations.Configuration>());
        }
       
        #region IQueryable

        public IQueryable<Role> Rolees
        {
            get { return Roles; }
            set { Roles = (IDbSet<Role>) value; }
        }

        public IQueryable<Review> Remaarks
        {
            get { return Remarks; }
            set { Remarks = (IDbSet<Review>) value; }
        }

        public IQueryable<Project> Projectss
        {
            get { return Projects; }
            set { Projects = (IDbSet<Project>) value; }
        }

        public IQueryable<Comment> Commentss
        {
            get { return Comments; }
            set { Comments = (IDbSet<Comment>) value; }
        }

        public IQueryable<Technology> Technologiess
        {
            get { return Technologies; }
            set { Technologies = (IDbSet<Technology>) value; }
        }

        public IQueryable<ReviewSetting> ReviewSettingss
        {
            get { return ReviewSettings; }
            set { ReviewSettings = (IDbSet<ReviewSetting>) value; }
        }

        public IQueryable<User> Userss
        {
            get { return Users; }
            set { Users = (IDbSet<User>)value; }
        }

        public IQueryable<ReviewMap> ReviewMapss
        {
            get { return ReviewMaps; }
            set { ReviewMaps = (IDbSet<ReviewMap>)value; }
        }

        #endregion 

        #region IDbSet
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Review> Remarks { get; set; }
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Technology> Technologies { get; set; }
        public IDbSet<ReviewSetting> ReviewSettings { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<ReviewMap> ReviewMaps { get; set; }
        #endregion

        #region Besic Methods
        public void Add<T>(T obj) where T : EntityBase
        {
            Set<T>().Add(obj);
        }

        public void Save()
        {
            base.SaveChanges();
        }

        /// <summary>
        /// Relationships and constraints while creating database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Role>().HasKey(x => x.Id).Property(x => x.Id)
                 .HasColumnName("Id");

            modelBuilder.Entity<Project>().HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Review>().HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Comment>().HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Technology>().HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<ReviewSetting>().HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<User>().HasKey(x => x.Id).Property(x => x.Id)
            .HasColumnName("Id");

            modelBuilder.Entity<ReviewMap>().HasKey(x => x.Id).Property(x => x.Id)
                .HasColumnName("Id");

            //// User and ReviewMap
            modelBuilder.Entity<ReviewMap>()
                .HasRequired(s => s.ReviewerUser)
                .WithMany(s => s.ReviewerMapUsers)
                .HasForeignKey(s => s.ReviewerId);

            modelBuilder.Entity<ReviewMap>()
                .HasRequired(s => s.DeveloperUser)
                .WithMany(s => s.DeveloperMapUsers)
                .HasForeignKey(s => s.DevloperId);
                

            //// User and ReviewMap
            //modelBuilder.Entity<ReviewMap>()
            //    .HasMany<ReviewMap>(s => s.ReviewerUsers)
            //    .WithRequired(s => s.Users)
            //    .HasForeignKey(s => s.Id);

           // User and Technology
            modelBuilder.Entity<User>()
                .HasRequired<Technology>(s => s.Technology)
                .WithMany(s => s.Users)
                .HasForeignKey(s => s.TechnologyId);

            // User and Role
            modelBuilder.Entity<User>().
                HasRequired(x => x.Role).
                WithMany(x => x.Users).
                HasForeignKey(x => x.RoleId);

            // User and Comment
            modelBuilder.Entity<Comment>(). 
                HasRequired(x => x.User).
                WithMany(x => x.Comments).
                HasForeignKey(x => x.UserId);

            // Review and comment
            //modelBuilder.Entity<Comment>().
            //    HasRequired(x => x.Review).
            //    WithMany(x => x.Comments).
            //    HasForeignKey(x => x.ReviewId);

            //// User and Review
            //modelBuilder.Entity<User>().
            //    HasMany(x => x.Reviews).
            //    WithRequired(x => x.User).
            //    HasForeignKey(x => x.UserId);

            // User and ReviewSettings
            //modelBuilder.Entity<ReviewSetting>()
            //    .HasRequired(m => m.UserToBeReviewed)
            //    .WithMany(t => t.UsersToBeReviewed)
            //    .HasForeignKey(m => m.UserToBeReviewedId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ReviewSetting>()
            //    .HasRequired(m => m.Reviewer)
            //    .WithMany(t => t.ReviewerUsers)
            //    .HasForeignKey(m => m.ReviewerId)
            //    .WillCascadeOnDelete(false);

            // Many to Many mapping Users and Projects of table UsersProjects
            modelBuilder.Entity<User>().
                HasMany(x => x.Projects).
                WithMany(x => x.Users).
                Map(m =>
                {
                    m.ToTable("UsersProjects");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("ProjectId");
                });

            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }
        #endregion
    }
}