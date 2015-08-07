using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week6LabTwitter.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Body { get; set; }
        public virtual TwitterUser Author { get; set; }
    }
}