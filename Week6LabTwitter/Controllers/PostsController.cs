using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Week6LabTwitter.Models;

namespace Week6LabTwitter.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        public TwitterDbContext db = new TwitterDbContext();

        public ActionResult FollowFriend(string id , bool follow)
        {
            var loggedInUsername = User.Identity.Name;
            var loggedInUser = db.Users.First(x => x.UserName == loggedInUsername);
            var newfollower = db.Users.Find(id);

            if (follow)
            {
                loggedInUser.Friends.Add(newfollower);
            }
            else
            {
                loggedInUser.Friends.Remove(newfollower);
            }

            db.SaveChanges();

            return Content("Ok");
        }

        public ActionResult FindFriends()
        {
            var loggedInUsername = User.Identity.Name;
            var loggedInUser = db.Users.First(x => x.UserName == loggedInUsername);
            var model = db.Users.Where(x => x.UserName != loggedInUsername).ToList()
                .Select(u => new FindFriendViewModel() {  FriendId = u.Id, FriendName = u.UserName, AlreadyAFriend = u.FollowedBy.Contains(loggedInUser) });

            return View(model);
        }
        // GET: Posts
        public ActionResult Index()
        {
            var loggedInUsername = User.Identity.Name;
            var loggedInUser = db.Users.First(x => x.UserName == loggedInUsername);

            var model = new List<Post>();
            //grab our posts.
            model.AddRange(db.Posts.Where(x => x.Author.UserName == loggedInUsername).ToList());
            //grab our friends Posts.
            model.AddRange(loggedInUser.Friends.SelectMany(x => x.Posts).ToList());

            return View(model);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
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
        public ActionResult Create(string body)
        {
            if (ModelState.IsValid)
            {
                Post newpost = new Post();
                string loggedInUsername = User.Identity.Name;
                newpost.Author = db.Users.First(x => x.UserName == loggedInUsername);
                newpost.Body = body;
                newpost.CreatedOn = DateTime.Now;
                db.Posts.Add(newpost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View((object)body);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
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
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Body,Author")] Post post)
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
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
