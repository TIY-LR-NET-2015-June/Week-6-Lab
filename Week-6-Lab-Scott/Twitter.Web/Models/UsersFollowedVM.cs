using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitter.Web.Models
{
    public class UsersFollowedVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        public bool AlreadyFollowing { get; set; }


    }
}