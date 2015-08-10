namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelBuilder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "FollowID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FollowID", c => c.Int(nullable: false));
        }
    }
}
