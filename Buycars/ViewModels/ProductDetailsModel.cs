using Buycars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buycars.ViewModels
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }

        public Review Review { get; set; }

        public IEnumerable<Review> Reviews { get; set; }

    }
}