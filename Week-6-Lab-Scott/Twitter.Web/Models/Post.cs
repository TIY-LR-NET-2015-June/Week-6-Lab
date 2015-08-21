using System;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Web.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime PostTime { get; set; }

        //Foreign key for user
        public virtual TwitterUser User { get; set; }
    }
}