namespace Twitter.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuidEdit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "MyProperty");
            DropColumn("dbo.Posts", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
