using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.Model;

namespace ReviewMe.DataAccess
{
    public class EntityContextInitializer : CreateDatabaseIfNotExists<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            base.Seed(context);

            var roles = new List<Role>
            {
                new Role{RoleName="Admin",CreatedBy=1,CreatedOn = DateTime.Now,IsActive=true},
                new Role{RoleName="Team Leader",CreatedBy=1,CreatedOn = DateTime.Now,IsActive=true},
                new Role{RoleName = "Developer",CreatedBy=1,CreatedOn = DateTime.Now,IsActive=true}
            };
            roles.ForEach(role => context.Roles.Add(role));

            var technologies = new List<Technology>
            {
                new Technology{TechnologyName = "ASP.NET",CreatedBy=1,CreatedOn = DateTime.Now,IsActive=true},
                new Technology{TechnologyName="JAVA",CreatedBy=1,CreatedOn = DateTime.Now,IsActive=true}
            };
            technologies.ForEach(technology => context.Technologies.Add(technology));

            var users = new List<User>
            {
                new User{FName = "admin",LName = "admin", Address = "Pune", Gender = true, Dob = DateTime.Now, EmailId = "admin@admin.com",Password = "password",RoleId = 1, TechnologyId = 1,CreatedBy = 1, CreatedOn = DateTime.Now, IsActive = true, OnClient = false, OnProject = false, OnTask = false}
                
            };
            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();
        }
    }
}
