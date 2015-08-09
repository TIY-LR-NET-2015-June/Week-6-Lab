using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Twitter.Models;
using Microsoft.Ajax.Utilities;
using System.Web.Security;

namespace Twitter.Controllers
{
    public class AlreadyExistsTwitterController : Controller
    {
        TwitterDBContext TwitterDB = new TwitterDBContext();

        public ActionResult StartPage()
        {
            List<Post> postFeed = new List<Post>();
            postFeed.AddRange(TwitterDB.Posts.Where(post => post.Publisher.Id == this.HttpContext.User.Identity.GetUserId()).ToList());
            //foreach (TwitterUser user in TwitterDB.Users.)
            //{
            //    postFeed.AddRange(TwitterDB.Posts.Where(post => post.Publisher == user.Id).ToList());
            //}
            return View(postFeed);
        }

        public ActionResult Follow(TwitterUser user)
        {
            //TwitterDB.Users.Find(Membership.GetUser().ProviderUserKey.ToString()).Following.Add(user);
            TwitterDB.Users.Find(Membership.GetUser().ProviderUserKey.ToString()).Following.Add(user);
            TwitterDB.SaveChanges();
            return View();
        }
    }
}