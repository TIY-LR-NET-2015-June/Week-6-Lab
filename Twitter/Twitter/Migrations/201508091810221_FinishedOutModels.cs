namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinishedOutModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Publisher_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "TwitterUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "Publisher_Id");
            CreateIndex("dbo.AspNetUsers", "TwitterUser_Id");
            AddForeignKey("dbo.AspNetUsers", "TwitterUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "Publisher_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Posts", "Publisher");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Publisher", c => c.String());
            DropForeignKey("dbo.Posts", "Publisher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "TwitterUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "TwitterUser_Id" });
            DropIndex("dbo.Posts", new[] { "Publisher_Id" });
            DropColumn("dbo.AspNetUsers", "TwitterUser_Id");
            DropColumn("dbo.Posts", "Publisher_Id");
        }
    }
}
