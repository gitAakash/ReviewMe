using System.Data.Entity.Migrations;

namespace ReviewMe.DataAccess.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                {
                    Id = c.Long(false, true),
                    UserId = c.Long(false),
                    ReviewId = c.Long(false),
                    Description = c.String(),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
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
                    Id = c.Long(false, true),
                    UserId = c.Long(false),
                    ReviewDate = c.DateTime(false),
                    ProjectId = c.Long(false),
                    ModuleName = c.String(),
                    FileReviewed = c.String(),
                    MethodsReviewed = c.String(),
                    Remarks = c.String(),
                    Status = c.Int(false),
                    CodingStandardRating = c.Decimal(false, 18, 2),
                    ProjectArchitecture = c.Decimal(false, 18, 2),
                    CodeOptimizationRating = c.Decimal(false, 18, 2),
                    QueryOptimizationRating = c.Decimal(false, 18, 2),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.User",
                c => new
                {
                    Id = c.Long(false, true),
                    FName = c.String(),
                    LName = c.String(),
                    MName = c.String(),
                    Dob = c.DateTimeOffset(false, 7),
                    Gender = c.Boolean(false),
                    EmailId = c.String(),
                    Password = c.String(),
                    MobileNo = c.String(),
                    AlternateContactNo = c.String(),
                    UserImage = c.String(),
                    Address = c.String(),
                    EmployeeCode = c.String(),
                    TeamLeaderId = c.Long(),
                    RoleId = c.Long(false),
                    TechnologyId = c.Long(false),
                    OnClient = c.Boolean(false),
                    OnProject = c.Boolean(false),
                    OnTask = c.Boolean(false),
                    Rating = c.Double(false),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
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
                    Id = c.Long(false, true),
                    ReviewerId = c.Long(false),
                    DevloperId = c.Long(false),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.DevloperId)
                .ForeignKey("dbo.User", t => t.ReviewerId)
                .Index(t => t.ReviewerId)
                .Index(t => t.DevloperId);

            CreateTable(
                "dbo.Notifications",
                c => new
                {
                    Id = c.Long(false, true),
                    NotificationMessage = c.String(),
                    UserId = c.Long(false),
                    IsRead = c.Boolean(false),
                    ViewedOn = c.DateTime(),
                    NotificationType = c.Int(false),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedBy)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedBy);

            CreateTable(
                "dbo.Project",
                c => new
                {
                    Id = c.Long(false, true),
                    UserId = c.Long(false),
                    ProjectTitle = c.String(),
                    Description = c.String(),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.ReviewSetting",
                c => new
                {
                    Id = c.Long(false, true),
                    UserToBeReviewedId = c.Long(false),
                    ReviewerId = c.Long(false),
                    ReviewUpTo = c.DateTime(false),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
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
                    Id = c.Long(false, true),
                    RoleName = c.String(false),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Technology",
                c => new
                {
                    Id = c.Long(false, true),
                    TechnologyName = c.String(),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ReviewDetails",
                c => new
                {
                    Id = c.Long(false, true),
                    ReviewerId = c.Long(false),
                    RevieweeId = c.Long(false),
                    Comment = c.String(),
                    Title = c.String(),
                    ReviewDate = c.DateTime(false),
                    CodingStandardRating = c.Decimal(false, 18, 2),
                    ProjectArchitecture = c.Decimal(false, 18, 2),
                    CodeOptimizationRating = c.Decimal(false, 18, 2),
                    QueryOptimizationRating = c.Decimal(false, 18, 2),
                    CreatedOn = c.DateTime(false),
                    CreatedBy = c.Long(false),
                    ModifiedOn = c.DateTime(),
                    ModifiedBy = c.Long(),
                    IsActive = c.Boolean(false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.RevieweeId)
                .ForeignKey("dbo.User", t => t.ReviewerId)
                .Index(t => t.ReviewerId)
                .Index(t => t.RevieweeId);

            CreateTable(
                "dbo.UsersProjects",
                c => new
                {
                    UserId = c.Long(false),
                    ProjectId = c.Long(false),
                })
                .PrimaryKey(t => new {t.UserId, t.ProjectId})
                .ForeignKey("dbo.User", t => t.UserId, true)
                .ForeignKey("dbo.Project", t => t.ProjectId, true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ReviewDetails", "ReviewerId", "dbo.User");
            DropForeignKey("dbo.ReviewDetails", "RevieweeId", "dbo.User");
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
            DropForeignKey("dbo.Notifications", "UserId", "dbo.User");
            DropForeignKey("dbo.Notifications", "CreatedBy", "dbo.User");
            DropForeignKey("dbo.ReviewMap", "ReviewerId", "dbo.User");
            DropForeignKey("dbo.ReviewMap", "DevloperId", "dbo.User");
            DropForeignKey("dbo.Comment", "ReviewId", "dbo.Review");
            DropIndex("dbo.UsersProjects", new[] {"ProjectId"});
            DropIndex("dbo.UsersProjects", new[] {"UserId"});
            DropIndex("dbo.ReviewDetails", new[] {"RevieweeId"});
            DropIndex("dbo.ReviewDetails", new[] {"ReviewerId"});
            DropIndex("dbo.ReviewSetting", new[] {"User_Id2"});
            DropIndex("dbo.ReviewSetting", new[] {"User_Id1"});
            DropIndex("dbo.ReviewSetting", new[] {"User_Id"});
            DropIndex("dbo.ReviewSetting", new[] {"ReviewerId"});
            DropIndex("dbo.ReviewSetting", new[] {"UserToBeReviewedId"});
            DropIndex("dbo.Project", new[] {"UserId"});
            DropIndex("dbo.Notifications", new[] {"CreatedBy"});
            DropIndex("dbo.Notifications", new[] {"UserId"});
            DropIndex("dbo.ReviewMap", new[] {"DevloperId"});
            DropIndex("dbo.ReviewMap", new[] {"ReviewerId"});
            DropIndex("dbo.User", new[] {"TechnologyId"});
            DropIndex("dbo.User", new[] {"RoleId"});
            DropIndex("dbo.User", new[] {"TeamLeaderId"});
            DropIndex("dbo.Review", new[] {"UserId"});
            DropIndex("dbo.Comment", new[] {"ReviewId"});
            DropIndex("dbo.Comment", new[] {"UserId"});
            DropTable("dbo.UsersProjects");
            DropTable("dbo.ReviewDetails");
            DropTable("dbo.Technology");
            DropTable("dbo.Role");
            DropTable("dbo.ReviewSetting");
            DropTable("dbo.Project");
            DropTable("dbo.Notifications");
            DropTable("dbo.ReviewMap");
            DropTable("dbo.User");
            DropTable("dbo.Review");
            DropTable("dbo.Comment");
        }
    }
}