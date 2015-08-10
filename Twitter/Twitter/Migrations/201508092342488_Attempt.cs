namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attempt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "TwitterUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "TwitterUser_Id" });
            CreateTable(
                "dbo.TwitterUserTwitterUsers",
                c => new
                    {
                        TwitterUser_Id = c.String(nullable: false, maxLength: 128),
                        TwitterUser_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TwitterUser_Id, t.TwitterUser_Id1 })
                .ForeignKey("dbo.AspNetUsers", t => t.TwitterUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TwitterUser_Id1)
                .Index(t => t.TwitterUser_Id)
                .Index(t => t.TwitterUser_Id1);
            
            DropColumn("dbo.AspNetUsers", "TwitterUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "TwitterUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.TwitterUserTwitterUsers", "TwitterUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.TwitterUserTwitterUsers", "TwitterUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TwitterUserTwitterUsers", new[] { "TwitterUser_Id1" });
            DropIndex("dbo.TwitterUserTwitterUsers", new[] { "TwitterUser_Id" });
            DropTable("dbo.TwitterUserTwitterUsers");
            CreateIndex("dbo.AspNetUsers", "TwitterUser_Id");
            AddForeignKey("dbo.AspNetUsers", "TwitterUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
