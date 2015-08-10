namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<TwitterDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TwitterDBContext context)
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
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password@123");
            context.Users.AddOrUpdate(u => u.Id,
                new TwitterUser()
                {
                    Id = "1",
                    UserName = "Brandon",
                    Email = "brandon@goza.net",
                    PasswordHash=password,
                    Posts = context.Posts.Where(p => p.Id == 1 || p.Id == 3).ToList(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                },
                new TwitterUser()
                {
                    Id = "2",
                    UserName = "Daniel",
                    Email = "daniel@pollock.net",
                    PasswordHash = password,
                    SecurityStamp = Guid.NewGuid().ToString(),
                },
                new TwitterUser()
                {
                    Id = "3",
                    UserName = "aaron@hudson.net",
                    Email = "aaron@hudson.net",
                    PasswordHash = password,
                    Posts = context.Posts.Where(p => p.Id == 2).ToList(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                },
                new TwitterUser()
                {
                    Id = "4",
                    UserName = "Mike",
                    Email = "mike@null.net",
                    PasswordHash = password,
                    Posts = context.Posts.Where(p => p.Id == 4).ToList(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                },
                new TwitterUser()
                {
                    Id = "5",
                    UserName = "Jason",
                    Email = "jason@williams.net",
                    PasswordHash = password,
                    Posts = context.Posts.Where(p => p.Id == 5).ToList(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                },
                new TwitterUser()
                {
                    Id = "6",
                    UserName = "David",
                    Email = "david@plate.net",
                    PasswordHash = password,
                    Posts = context.Posts.Where(p => p.Id == 6).ToList(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                },
                new TwitterUser()
                {
                    Id = "7",
                    UserName = "Scott",
                    Email = "scott@furgeson.net",
                    PasswordHash = password,
                    Posts = context.Posts.Where(p => p.Id == 7).ToList(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                });
            context.Users.Find("3").Following.Add(context.Users.Find("1"));
            context.Users.Find("3").Following.Add(context.Users.Find("2"));
            context.Users.Find("2").Following.Add(context.Users.Find("1"));
            context.Users.Find("6").Following.Add(context.Users.Find("7"));
            context.Users.Find("6").Following.Add(context.Users.Find("4"));
            context.Users.Find("5").Following.Add(context.Users.Find("3"));
            context.Users.Find("7").Following.Add(context.Users.Find("1"));
            context.Users.Find("7").Following.Add(context.Users.Find("6"));
            context.Users.Find("7").Following.Add(context.Users.Find("4"));
            context.Posts.AddOrUpdate(
                p => p.Title,
                new Post()
                {
                    Id = 1,
                    Publisher = context.Users.Find("1"),
                    PublishedOn = DateTime.Now,
                    Title = "BrandonCurabitur et arcu",
                    Body = "BrandonCurabitur et arcu eros. Phasellus viverra elementum nisl. Aenean a orci aliquet, tincidunt lectus at, mollis sapien. Quisque ac sagittis risus, quis scelerisque velit. Aenean consequat leo ac diam lobortis finibus. Nunc tempus neque efficitur, laoreet justo id, porttitor magna. Quisque tincidunt justo sit amet tortor suscipit varius. Nam ornare, nisl non posuere facilisis, justo libero molestie sem, nec elementum lectus lorem sed eros."
                },
                new Post()
                {
                    Id = 2,
                    Publisher = context.Users.Find("3"),
                    PublishedOn = DateTime.Now,
                    Title = "AaronFusce scelerisque metus",
                    Body = "AaronFusce scelerisque metus non augue auctor elementum. Sed sed pulvinar urna. Vivamus laoreet erat ipsum. Pellentesque eleifend convallis pharetra. Sed fringilla velit id tempor sodales. Ut aliquam vel velit in venenatis. Quisque sit amet maximus elit, in blandit tellus."
                },
                new Post()
                {
                    Id = 3,
                    Publisher = context.Users.Find("1"),
                    PublishedOn = DateTime.Now,
                    Title = "BrandonNulla congue aliquet",
                    Body = "BrandonNulla congue aliquet urna, in ultrices nulla vulputate vel. Maecenas tempus ex ut placerat condimentum. In quis efficitur ex. Suspendisse potenti. Nunc velit lorem, fermentum id consequat eget, cursus sit amet velit. Etiam sed molestie ligula. Vivamus eleifend dui facilisis, finibus nisi ut, tempus tortor. In eget luctus lectus. Vestibulum lobortis vitae est auctor laoreet. Etiam feugiat elit velit, nec dictum tortor tristique quis."
                },
                new Post()
                {
                    Id = 4,
                    Publisher = context.Users.Find("4"),
                    PublishedOn = DateTime.Now,
                    Title = "MikeNulla congue aliquet",
                    Body = "MikeNulla congue aliquet urna, in ultrices nulla vulputate vel. Maecenas tempus ex ut placerat condimentum. In quis efficitur ex. Suspendisse potenti. Nunc velit lorem, fermentum id consequat eget, cursus sit amet velit. Etiam sed molestie ligula. Vivamus eleifend dui facilisis, finibus nisi ut, tempus tortor. In eget luctus lectus. Vestibulum lobortis vitae est auctor laoreet. Etiam feugiat elit velit, nec dictum tortor tristique quis."
                },
                new Post()
                {
                    Id = 5,
                    Publisher = context.Users.Find("5"),
                    PublishedOn = DateTime.Now,
                    Title = "JasonNulla congue aliquet",
                    Body = "JasonNulla congue aliquet urna, in ultrices nulla vulputate vel. Maecenas tempus ex ut placerat condimentum. In quis efficitur ex. Suspendisse potenti. Nunc velit lorem, fermentum id consequat eget, cursus sit amet velit. Etiam sed molestie ligula. Vivamus eleifend dui facilisis, finibus nisi ut, tempus tortor. In eget luctus lectus. Vestibulum lobortis vitae est auctor laoreet. Etiam feugiat elit velit, nec dictum tortor tristique quis."
                },
                new Post()
                {
                    Id = 6,
                    Publisher = context.Users.Find("6"),
                    PublishedOn = DateTime.Now,
                    Title = "DavidNulla congue aliquet",
                    Body = "DavidNulla congue aliquet urna, in ultrices nulla vulputate vel. Maecenas tempus ex ut placerat condimentum. In quis efficitur ex. Suspendisse potenti. Nunc velit lorem, fermentum id consequat eget, cursus sit amet velit. Etiam sed molestie ligula. Vivamus eleifend dui facilisis, finibus nisi ut, tempus tortor. In eget luctus lectus. Vestibulum lobortis vitae est auctor laoreet. Etiam feugiat elit velit, nec dictum tortor tristique quis."
                },
                new Post()
                {
                    Id = 7,
                    Publisher = context.Users.Find("7"),
                    PublishedOn = DateTime.Now,
                    Title = "ScottLorem ipsum dolor",
                    Body = "ScottLorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse eget tincidunt augue. Morbi semper purus id sollicitudin consequat. Donec porta, lacus ac sodales facilisis, nunc sem ultrices lectus, id gravida augue massa sed orci. Phasellus vestibulum elementum consectetur. Aliquam ullamcorper laoreet bibendum. Integer suscipit sagittis sagittis. Nulla risus ante, suscipit at mi nec, ullamcorper gravida velit. Vivamus iaculis."
                });
            context.SaveChanges();
        }
    }
}
