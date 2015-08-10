namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForgotModelBuilder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "TwitterUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "TwitterUser_Id" });
            CreateTable(
                "dbo.User_Follow",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FollowerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowerID })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerID)
                .Index(t => t.UserId)
                .Index(t => t.FollowerID);
            
            DropColumn("dbo.AspNetUsers", "TwitterUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "TwitterUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.User_Follow", "FollowerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Follow", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.User_Follow", new[] { "FollowerID" });
            DropIndex("dbo.User_Follow", new[] { "UserId" });
            DropTable("dbo.User_Follow");
            CreateIndex("dbo.AspNetUsers", "TwitterUser_Id");
            AddForeignKey("dbo.AspNetUsers", "TwitterUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
