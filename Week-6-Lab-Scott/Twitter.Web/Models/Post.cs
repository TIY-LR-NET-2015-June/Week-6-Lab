using System;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Web.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int MyProperty { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime PostTime { get; set; }

        //Foreign key for user
        public virtual int UserId { get; set; }
        public virtual TwitterUser User { get; set; }
    }
}