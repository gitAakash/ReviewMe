namespace ReviewMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ReviewId = c.Long(nullable: false),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Review", t => t.ReviewId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ReviewId);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ReviewDate = c.DateTime(nullable: false),
                        ProjectId = c.Long(nullable: false),
                        ModuleName = c.String(),
                        FileReviewed = c.String(),
                        MethodsReviewed = c.String(),
                        Remarks = c.String(),
                        Status = c.Int(nullable: false),
                        CodingStandardRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProjectArchitecture = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CodeOptimizationRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QueryOptimizationRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        MName = c.String(),
                        Dob = c.DateTimeOffset(nullable: false, precision: 7),
                        Gender = c.Boolean(nullable: false),
                        EmailId = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        MobileNo = c.String(),
                        AlternateContactNo = c.String(),
                        UserImage = c.String(),
                        Address = c.String(),
                        EmployeeCode = c.String(),
                        TeamLeaderId = c.Long(),
                        RoleId = c.Long(nullable: false),
                        TechnologyId = c.Long(nullable: false),
                        OnClient = c.Boolean(nullable: false),
                        OnProject = c.Boolean(nullable: false),
                        OnTask = c.Boolean(nullable: false),
                        Rating = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .ForeignKey("dbo.User", t => t.TeamLeaderId)
                .ForeignKey("dbo.Technology", t => t.TechnologyId)
                .Index(t => t.TeamLeaderId)
                .Index(t => t.RoleId)
                .Index(t => t.TechnologyId);
            
            CreateTable(
                "dbo.ReviewMap",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ReviewerId = c.Long(nullable: false),
                        DevloperId = c.Long(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.DevloperId)
                .ForeignKey("dbo.User", t => t.ReviewerId)
                .Index(t => t.ReviewerId)
                .Index(t => t.DevloperId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ProjectTitle = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ReviewSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserToBeReviewedId = c.Long(nullable: false),
                        ReviewerId = c.Long(nullable: false),
                        ReviewUpTo = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        User_Id = c.Long(),
                        User_Id1 = c.Long(),
                        User_Id2 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ReviewerId)
                .ForeignKey("dbo.User", t => t.UserToBeReviewedId)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.User", t => t.User_Id1)
                .ForeignKey("dbo.User", t => t.User_Id2)
                .Index(t => t.UserToBeReviewedId)
                .Index(t => t.ReviewerId)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1)
                .Index(t => t.User_Id2);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Technology",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TechnologyName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsersProjects",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        ProjectId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjectId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropForeignKey("dbo.ReviewSetting", "User_Id2", "dbo.User");
            DropForeignKey("dbo.User", "TechnologyId", "dbo.Technology");
            DropForeignKey("dbo.User", "TeamLeaderId", "dbo.User");
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.ReviewSetting", "User_Id1", "dbo.User");
            DropForeignKey("dbo.Review", "UserId", "dbo.User");
            DropForeignKey("dbo.ReviewSetting", "User_Id", "dbo.User");
            DropForeignKey("dbo.ReviewSetting", "UserToBeReviewedId", "dbo.User");
            DropForeignKey("dbo.ReviewSetting", "ReviewerId", "dbo.User");
            DropForeignKey("dbo.UsersProjects", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.UsersProjects", "UserId", "dbo.User");
            DropForeignKey("dbo.Project", "UserId", "dbo.User");
            DropForeignKey("dbo.ReviewMap", "ReviewerId", "dbo.User");
            DropForeignKey("dbo.ReviewMap", "DevloperId", "dbo.User");
            DropForeignKey("dbo.Comment", "ReviewId", "dbo.Review");
            DropIndex("dbo.UsersProjects", new[] { "ProjectId" });
            DropIndex("dbo.UsersProjects", new[] { "UserId" });
            DropIndex("dbo.ReviewSetting", new[] { "User_Id2" });
            DropIndex("dbo.ReviewSetting", new[] { "User_Id1" });
            DropIndex("dbo.ReviewSetting", new[] { "User_Id" });
            DropIndex("dbo.ReviewSetting", new[] { "ReviewerId" });
            DropIndex("dbo.ReviewSetting", new[] { "UserToBeReviewedId" });
            DropIndex("dbo.Project", new[] { "UserId" });
            DropIndex("dbo.ReviewMap", new[] { "DevloperId" });
            DropIndex("dbo.ReviewMap", new[] { "ReviewerId" });
            DropIndex("dbo.User", new[] { "TechnologyId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.User", new[] { "TeamLeaderId" });
            DropIndex("dbo.Review", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "ReviewId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropTable("dbo.UsersProjects");
            DropTable("dbo.Technology");
            DropTable("dbo.Role");
            DropTable("dbo.ReviewSetting");
            DropTable("dbo.Project");
            DropTable("dbo.ReviewMap");
            DropTable("dbo.User");
            DropTable("dbo.Review");
            DropTable("dbo.Comment");
        }
    }
}
