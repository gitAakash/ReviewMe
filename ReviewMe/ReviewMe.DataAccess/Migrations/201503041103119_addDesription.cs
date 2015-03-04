namespace ReviewMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDesription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Description11", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "Description11");
        }
    }
}
