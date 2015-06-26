namespace ReviewMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewDateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReviewDetails", "ReviewDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReviewDetails", "ReviewDate");
        }
    }
}
