namespace Week6LabTwitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwitterUserFriends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FollowedById = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowedById })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowedById)
                .Index(t => t.UserId)
                .Index(t => t.FollowedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followers", "FollowedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Followers", new[] { "FollowedById" });
            DropIndex("dbo.Followers", new[] { "UserId" });
            DropTable("dbo.Followers");
        }
    }
}
