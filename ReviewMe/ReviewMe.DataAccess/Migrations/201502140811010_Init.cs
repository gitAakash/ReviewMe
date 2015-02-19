using System;
using System.Data.Entity.Migrations;

namespace ReviewMe.DataAccess.Migrations
{
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                {
                    Id = c.Int(false, true),
                    EmployeeCode = c.Int(false),
                    Name = c.String(),
                    Description = c.String(),
                    IsEmployeeRetired = c.Boolean(false),
                    Country = c.String(),
                    Company = c.String(),
                    IsActive = c.Boolean(false, true),
                    CreatedOn = c.DateTime(false, null, DateTime.Now),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(false, null, DateTime.Now),
                    ModifiedBy = c.Long(false)
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Int(false, true),
                    RoleName = c.ToString(),
                    IsActive = c.Boolean(false, true),
                    CreatedOn = c.DateTime(false, null, DateTime.Now),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(false, null, DateTime.Now),
                    ModifiedBy = c.Long(false)
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Remarks",
                c => new
                {
                    Id = c.Int(false, true),
                    DevloperId = c.Int(false),
                    ReviewedDate = c.DateTime(false, null, DateTime.Now),
                    ProjectId = c.Int(),
                    ModuleName = c.String(),
                    FilesReviewed = c.String(false),
                    MethodsReviewed = c.String(false),
                    Remarks = c.String(false),
                    Status = c.Int(false),
                    CodingStandardRatings = c.Decimal(false, 1, 1, 1),
                    CodeOptimizationRating = c.Decimal(false, 1, 1, 1),
                    QueryOptimizationRating = c.Decimal(false, 1, 1, 1),
                    IsActive = c.Boolean(false, true),
                    CreatedOn = c.DateTime(false, null, DateTime.Now),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(false, null, DateTime.Now),
                    ModifiedBy = c.Long(false)
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}