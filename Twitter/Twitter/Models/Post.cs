using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Twitter.Models
{
    public class Post
    {
        TwitterDBContext TwitterDB = new TwitterDBContext();
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishedOn { get; set; }
        public virtual TwitterUser Publisher { get; set; }

        public Post()
        {
            //PublishedOn = DateTime.Now;
            //Publisher = TwitterDB.Users.Find(Membership.GetUser().ProviderUserKey);
        }
    }
}