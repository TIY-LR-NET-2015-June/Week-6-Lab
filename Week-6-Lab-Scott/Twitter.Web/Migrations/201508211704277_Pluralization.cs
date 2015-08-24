namespace Twitter.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pluralization : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Posts", newName: "Posts");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Posts", newName: "Post");
        }
    }
}
