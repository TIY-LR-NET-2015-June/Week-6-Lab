using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Twitter.Web.Models;

namespace Twitter.Web.Controllers
{
    public class PostsController : Controller
    {
        private TwitterDbContext db = new TwitterDbContext();

        // GET: Posts
        [Authorize]
        public ActionResult Index()
        {
            TwitterUser user = db.Users.Find(User.Identity.GetUserId());
            List<Post> posts = new List<Post>();
            posts.AddRange(user.Posts);
            user.UsersFollowed.ForEach(x => posts.AddRange(x.Posts));
            posts.OrderByDescending(d => d.PostTime);
            return View(posts);
        }

        //Get: Users followed
        [Authorize]
        public ActionResult Followed()
        {
            TwitterUser user = db.Users.Find(User.Identity.GetUserId());
            return View(user.UsersFollowed.Select(u => new UsersFollowedVM() { FullName = u.FirstName + " " + u.LastName, UserName = u.UserName, Id = u.Id }));
        }

        //Get: Choose who to follow
        [Authorize]
        public ActionResult FindUsers()
        {
            return View(db.Users.Select(u => new UsersFollowedVM() { FullName = u.FirstName + " " + u.LastName, UserName = u.UserName, Id = u.Id }));
        }

        //Post: Choose who to follow
        [HttpPost]
        public ActionResult FollowUser(string Id)
        {
            TwitterUser user = db.Users.Find(User.Identity.GetUserId());

            var followedUser = user.UsersFollowed.Where(i => i.Id == Id).FirstOrDefault();
            if (followedUser == null)
            {
                TwitterUser whoTheyWantToFollow = db.Users.Find(Id);
                user.UsersFollowed.Add(whoTheyWantToFollow);
                db.SaveChanges();
            }
            return Content("OK");
        }

        [HttpPost]
        public ActionResult UnFollowUser(string Id)
        {
            TwitterUser user = db.Users.Find(User.Identity.GetUserId());
            var unfollowedUser = user.UsersFollowed.Where(i => i.Id == Id).FirstOrDefault();
            user.UsersFollowed.Remove(unfollowedUser);
            return Content("OK");
        }


        // GET: Posts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            TwitterUser user = db.Users.Find(User.Identity.GetUserId());
            //Check to make sure the user follows the user that posted before returning post details (commented out for later implementation)
            if (post == null /*|| user.UsersFollowed.Any(y => y.Id != post.User.Id)*/)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Text,PostTime,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostTime = DateTime.Now;
                TwitterUser user = db.Users.Find(User.Identity.GetUserId());
                post.User = user;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            TwitterUser user = db.Users.Find(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null || post.User != user)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Text,PostTime,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TwitterUser user = db.Users.Find(User.Identity.GetUserId());
            Post post = db.Posts.Find(id);
            if (post == null || post.User != user)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
