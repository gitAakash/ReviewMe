namespace ReviewMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserResertPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ResetPassword", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ResetPassword");
        }
    }
}
