using Buycars.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buycars.ViewModels
{
    public class HomeViewModel
    {
        public IPagedList<Product> Products { get; set; }
        public IEnumerable<CartProduct> CartProducts { get; set; }

        public List<Product> AllProducts { get; set; }


       


    }

}