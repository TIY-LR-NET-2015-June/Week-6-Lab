using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Twitter.Models;

namespace Twitter.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        TwitterDBContext TwitterDB = new TwitterDBContext();

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Post post)
        {
            TwitterDB.Posts.Attach(post);
            TwitterDB.Posts.Add(post);
            TwitterDB.Users.Find(Membership.GetUser().ProviderUserKey.ToString()).Posts.Add(TwitterDB.Posts.Find(post.Id));
            TwitterDB.SaveChanges();
            return RedirectToAction("StartPage", "Twitter");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            return View(TwitterDB.Posts.Find(ID));
        }
        [HttpPost]
        public ActionResult Delete(Post post)
        {
            TwitterDB.Posts.Attach(post);
            TwitterDB.Posts.Remove(post);
            TwitterDB.Users.Find(Membership.GetUser().ProviderUserKey.ToString()).Posts.Remove(TwitterDB.Posts.Find(post.Id));
            TwitterDB.SaveChanges();

            return RedirectToAction("StartPage", "Twitter");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            return View(TwitterDB.Posts.Find(ID));
        }
        [HttpPost]
        public ActionResult Edit(Post post)
        {
            TwitterDB.Posts.Remove(TwitterDB.Posts.Find(post.Id));
            TwitterDB.Posts.Add(post);
            TwitterDB.SaveChanges();
            return RedirectToAction("StartPage", "Twitter");
        }

        public ActionResult Details(int ID)
        {
            return View(TwitterDB.Posts.Find(ID));
        }
    }
}