namespace ReviewMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewDetailsClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReviewDetails", "CodingStandardRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ReviewDetails", "ProjectArchitecture", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ReviewDetails", "CodeOptimizationRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ReviewDetails", "QueryOptimizationRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReviewDetails", "QueryOptimizationRating");
            DropColumn("dbo.ReviewDetails", "CodeOptimizationRating");
            DropColumn("dbo.ReviewDetails", "ProjectArchitecture");
            DropColumn("dbo.ReviewDetails", "CodingStandardRating");
        }
    }
}
