using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Twitter.Models
{
    public class Post
    {
        TwitterDBContext TwitterDB = new TwitterDBContext();
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishedOn { get; set; }
        public TwitterUser Publisher { get; set; }

        public Post()
        {
            PublishedOn = DateTime.Now;
            Publisher = TwitterDB.Users.Find(Membership.GetUser().ProviderUserKey);
        }
    }
}