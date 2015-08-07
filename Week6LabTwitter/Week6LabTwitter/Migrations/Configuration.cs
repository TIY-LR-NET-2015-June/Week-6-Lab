namespace Week6LabTwitter.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Week6LabTwitter.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Week6LabTwitter.Models.TwitterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Week6LabTwitter.Models.TwitterDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var userStore = new UserStore<TwitterUser>(db);
            var userManager = new UserManager<TwitterUser>(userStore);


            if (!db.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                // Add missing roles
                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }

                // Create test users
                var user = userManager.FindByName("jwilliams");
                if (user == null)
                {
                    var newUser = new TwitterUser()
                    {
                        UserName = "jwilliams",
                        Email = "jasonwilliamsmd@gmail.com",
                        PhoneNumber = "5551234567",
                    };
                    userManager.Create(newUser, "Password1");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "Admin");
                }
            }

            var user2 = userManager.FindByName("jwilliams");

            //insert some posts
            db.Posts.AddOrUpdate(x => x.Body,
                new Post { Author = user2, Body = "Hello World!", CreatedOn = DateTime.Parse("8/7/2015 2:00pm") },
                new Post { Author = user2, Body = "I did it finally.", CreatedOn = DateTime.Parse("8/7/2015 2:05pm") },
                new Post { Author = user2, Body = "Not liking life today...", CreatedOn = DateTime.Parse("8/7/2015 2:10pm") },
                new Post { Author = user2, Body = "Might kill somebody, maybe just myself, but we'll see. #pensiveAboutLife", CreatedOn = DateTime.Parse("8/7/2015 2:15pm") }
                );

        }
    }
}
