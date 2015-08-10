using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Twitter.Models;
using Microsoft.Ajax.Utilities;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace Twitter.Controllers
{
    [Authorize]
    public class TwitterController : Controller
    {
        TwitterDBContext TwitterDB = new TwitterDBContext();

        public ActionResult StartPage()
        {
            TwitterUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            List<Post> postFeed = new List<Post>();
            postFeed.AddRange(TwitterDB.Posts.Where(post => post.Publisher.Id == user.Id));
            foreach (TwitterUser followee in user.Following.ToList())
            {
                postFeed.AddRange(TwitterDB.Posts.Where(post => post.Publisher.Id == followee.Id).ToList());
            }
            return View(postFeed);
        }

        public ActionResult Follow(TwitterUser user)
        {
            TwitterDB.Users.Find(Membership.GetUser().ProviderUserKey.ToString()).Following.Add(user);
            TwitterDB.SaveChanges();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Follow(string ID)
        {
            TwitterUser Following = TwitterDB.Users.Find(ID);
            TwitterUser Follower = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            TwitterDB.Users.Find(Following.Id).Following.Add(TwitterDB.Users.Find(Follower.Id));
            //Following.Following.Add(Follower);
            //Follower.Followers.Add(Following);
            TwitterDB.SaveChanges();
            return PartialView("Unfollow", ID);
        }

        [HttpPost]
        public ActionResult Unfollow(string ID)
        {
            TwitterUser Following = TwitterDB.Users.Find(ID);
            TwitterUser Follower = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            TwitterDB.Users.Find(Following.Id).Following.Remove(Follower);
            // Following.Following.Remove(Follower);
            TwitterDB.Users.Find(Follower.Id).Followers.Remove(Following);
            //Follower.Followers.Remove(Following);
            //TwitterDB.Users.Attach(Following);
            //TwitterDB.Users.Attach(Follower);
            TwitterDB.SaveChanges();
            return PartialView("Follow", ID);
        }
    }
}