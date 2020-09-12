using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buycars.Models
{
    public class CartProduct
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Car Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int Price { get; set; }

        [Display(Name = "Fabrication Year")]
        public int Year { get; set; }


        [Display(Name = "Car Image")]
        public byte[] Image { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateAddedToCart { get; set; }


        public virtual IdentityUser IdentityUser { get; set; }

        public string IdentityUserId { get; set; }
    }
}