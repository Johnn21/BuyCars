using Buycars.Models;
using Buycars.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Buycars.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult SortProducts(int? page)
        {

            var selectedValue = Request.Form["SortType"].ToString();

            var prod = _context.Products.ToList().ToPagedList(page ?? 1, 9);

            string CurrentUserId = User.Identity.GetUserId();

            if (selectedValue == "1")
            {
                prod = _context.Products.OrderByDescending(p => p.Views).ToList().ToPagedList(page ?? 1, 9);
            }else if(selectedValue == "2")
            {
                prod = _context.Products.OrderBy(p => p.Price).ToList().ToPagedList(page ?? 1, 9);
            }else if(selectedValue == "3")
            {
                prod = _context.Products.OrderByDescending(p => p.Price).ToList().ToPagedList(page ?? 1, 9);
            }else if(selectedValue == "4")
            {
                prod = _context.Products.OrderByDescending(p => p.DateAdded).ToList().ToPagedList(page ?? 1, 9);
            }

            var viewModel = new HomeViewModel
            {
                Products = prod,
                CartProducts = _context.CartProducts.Where(c => c.IdentityUserId == CurrentUserId).ToList(),
                AllProducts = _context.Products.ToList()
            };

            

            return View("Index", viewModel);
        }

        public ActionResult SearchProduct(string searchName, int? page)
        {

            var prod = _context.Products.ToList().ToPagedList(page ?? 1, 9);

            if (string.IsNullOrEmpty(searchName))
            {
                prod = _context.Products.ToList().ToPagedList(page ?? 1, 9);
            }
            else
            {
                prod = _context.Products.Where(p => p.Name.StartsWith(searchName)).ToList().ToPagedList(page ?? 1, 9);
            }


           

            string CurrentUserId = User.Identity.GetUserId();

            var viewModel = new HomeViewModel
            {
                Products = prod,
                CartProducts = _context.CartProducts.Where(c => c.IdentityUserId == CurrentUserId).ToList(),
                AllProducts = _context.Products.ToList()
            };

            return View("Index", viewModel);
        }


        public ActionResult Index(int? page)
        {
            // var products = _context.Products.ToList();

            string CurrentUserId = User.Identity.GetUserId();

   

          


        var viewModel = new HomeViewModel
            {
                Products = _context.Products.ToList().ToPagedList(page ?? 1, 9),
                CartProducts = _context.CartProducts.Where(c => c.IdentityUserId == CurrentUserId).ToList(),
                AllProducts = _context.Products.ToList(),

            };


            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult FilterProducts(List<Product> allproducts, int? page, bool Under2000 = false, bool Under4000 = false, bool After4000 = false,
                    bool inStock = false, bool AllStock = false)
        {

            var prods = _context.Products.ToList().ToPagedList(page ?? 1, 9);

            List<string> brandList = new List<string>();

            foreach (var item in allproducts)
               {

                   if (item.isSelected)
                   {
                    brandList.Add(item.Brand);
                   }
               }

            if (!brandList.Any())
            {
                for(int i = 0; i < prods.Count; i++)
                {
                    brandList.Add(prods[i].Brand);
                }
            }
            else
            {
                prods = _context.Products.Where(p => brandList.Contains(p.Brand)).ToList().ToPagedList(page ?? 1, 9);
            }

            


            if (Under2000)
            {
                if(!Under4000 && !After4000)
                {
                    prods = _context.Products.Where(p => p.Price <= 2000).Where(p => brandList.Contains(p.Brand)).ToList().ToPagedList(page ?? 1, 9);

                    if (inStock)
                    {
                        prods = _context.Products.Where(p => p.Price <= 2000).Where(p => brandList.Contains(p.Brand)).Where(p => p.inStock == true).ToList().ToPagedList(page ?? 1, 9);
                    }

                }         
                else if(Under4000 && !After4000)
                {
                    prods = _context.Products.Where(p => p.Price <= 4000).Where(p => brandList.Contains(p.Brand)).ToList().ToPagedList(page ?? 1, 9);

                    if (inStock)
                    {
                        prods = _context.Products.Where(p => p.Price <= 4000).Where(p => brandList.Contains(p.Brand)).Where(p => p.inStock == true).ToList().ToPagedList(page ?? 1, 9);
                    }

                }
                else if(Under4000 && After4000)
                {
                    prods = _context.Products.Where(p => brandList.Contains(p.Brand)).ToList().ToPagedList(page ?? 1, 9);

                    if (inStock)
                    {
                        prods = _context.Products.Where(p => brandList.Contains(p.Brand)).Where(p => p.inStock == true).ToList().ToPagedList(page ?? 1, 9);
                    }

                }
                    
            }
            else if (Under4000)
            {
                if(!Under2000 && !After4000)
                {
                    prods = _context.Products.Where(p => p.Price >= 2000 && p.Price <= 4000).Where(p => brandList.Contains(p.Brand)).ToList().ToPagedList(page ?? 1, 9);

                    if (inStock == true)
                    {
                        prods = _context.Products.Where(p => p.Price >= 2000 && p.Price <= 4000).Where(p => brandList.Contains(p.Brand)).Where(p => p.inStock == true).ToList().ToPagedList(page ?? 1, 9);
                    }

                }else if(!Under2000 && After4000)
                {
                    prods = _context.Products.Where(p => p.Price >= 2000).Where(p => brandList.Contains(p.Brand)).ToList().ToPagedList(page ?? 1, 9);

                    if (inStock)
                    {
                        prods = _context.Products.Where(p => p.Price >= 2000).Where(p => brandList.Contains(p.Brand)).Where(p => p.inStock == true).ToList().ToPagedList(page ?? 1, 9);
                    }

                }
            }else if (After4000)
            {
                if(!Under2000 && !Under4000)
                {
                    prods = _context.Products.Where(p => p.Price >= 4000).Where(p => brandList.Contains(p.Brand)).ToList().ToPagedList(page ?? 1, 9);

                    if (inStock)
                    {
                        prods = _context.Products.Where(p => p.Price >= 4000).Where(p => brandList.Contains(p.Brand)).Where(p => p.inStock == true).ToList().ToPagedList(page ?? 1, 9);
                    }

                }

            }
            else
            {
                if (inStock)
                {
                    prods = _context.Products.Where(p => brandList.Contains(p.Brand)).Where(p => p.inStock == true).ToList().ToPagedList(page ?? 1, 9);
                }
            }


            string CurrentUserId = User.Identity.GetUserId();

            var viewModel = new HomeViewModel
            {
                Products = prods,
                CartProducts = _context.CartProducts.Where(c => c.IdentityUserId == CurrentUserId).ToList(),
                AllProducts = _context.Products.ToList()
            };


            return View("Index", viewModel);
        }

    }
}