using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buycars.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Make a Review:")]
        public string Body { get; set; }


        public virtual Product Product { get; set; }
        [Required]
        public int ProductId { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
        public string IdentityUserId { get; set; }

    }
}