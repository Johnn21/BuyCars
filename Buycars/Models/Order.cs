using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buycars.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int OrderPrice { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
        public string IdentityUserId { get; set; }
    }
}