namespace ReviewMe.DataAccess.Migrations
{
    using ReviewMe.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ReviewMe.DataAccess.EntityContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ReviewMe.DataAccess.EntityContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            var technology = new List<Technology>
            {
                new Technology
                {
                    TechnologyName=".Net",
                    CreatedOn=DateTime.Now,
                    IsActive=true,
                },
                 new Technology
                {
                    TechnologyName="Java",
                    CreatedOn=DateTime.Now,
                    IsActive=true,
                },
                 new Technology
                {
                    TechnologyName="PHP",
                    CreatedOn=DateTime.Now,
                    IsActive=true,
                }
            };
            technology.ForEach(i => context.Technologies.AddOrUpdate(t => t.TechnologyName, i));


            var role = new List<Role>
            {
                new Role
                {
                    RoleName="Admin",
                    CreatedOn=DateTime.Now,
                    IsActive=true,
                },
                 new Role
                {
                    RoleName="TeamLeader",
                    CreatedOn=DateTime.Now,
                    IsActive=true,
                },
                 new Role
                {
                    RoleName="Developer",
                    CreatedOn=DateTime.Now,
                    IsActive=true,
                }
            };
            role.ForEach(i => context.Roles.AddOrUpdate(r => r.RoleName, i));


            var user = new List<User>
            {
                new User 
                {
                    FName="F Name",
                    LName="L Name",
                    Dob=DateTime.Now,
                    Gender=true,
                    EmailId="admin@admin.com",
                    Password="123456",
                    ConfirmPassword="123456",
                    //TeamLeader="",
                    Role=role.First(),
                    //TechnologyId=technology.SingleOrDefault().Id,
                    Technology=technology.First(),
                    OnClient=true,
                    OnProject=true,
                    OnTask=true,
                    Rating=2,
                    CreatedOn=DateTime.Now,
                    IsActive=true,
                },
            };
            user.ForEach(i => context.Users.AddOrUpdate(u => u.FName, i));
        }
    }
}
