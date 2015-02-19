using System;
using System.Data.Entity.Migrations;

namespace ReviewMe.DataAccess.Migrations
{
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                {
                    Id = c.Int(false, true),
                    ProjectTitle = c.String(false),
                    IsActive = c.Boolean(false, true),
                    CreatedOn = c.DateTime(false, null, DateTime.Now),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(true, null, DateTime.Now),
                    ModifiedBy = c.Long(true)
                })
                .PrimaryKey(t => t.Id);

            /* AlterColumn("dbo.Employee", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Employee", "ModifiedBy", c => c.Long());
            AlterColumn("dbo.Remark", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Remark", "ModifiedBy", c => c.Long());
            AlterColumn("dbo.Role", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Role", "ModifiedBy", c => c.Long());*/
        }

        public override void Down()
        {
            /*AlterColumn("dbo.Role", "ModifiedBy", c => c.Long(false));
            AlterColumn("dbo.Role", "ModifiedOn", c => c.DateTime(false));
            AlterColumn("dbo.Remark", "ModifiedBy", c => c.Long(false));
            AlterColumn("dbo.Remark", "ModifiedOn", c => c.DateTime(false));
            AlterColumn("dbo.Employee", "ModifiedBy", c => c.Long(false));
            AlterColumn("dbo.Employee", "ModifiedOn", c => c.DateTime(false));*/
        }
    }
}