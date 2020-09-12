using Buycars.Models;
using Buycars.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buycars.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
            _context.Configuration.AutoDetectChangesEnabled = false;
            _context.Configuration.ValidateOnSaveEnabled = false;

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

     

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddCar()
        {
            Product prod = new Product();

            return View(prod);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product, HttpPostedFileBase image1)
        {

            if (!ModelState.IsValid)
            {
                
                return View("AddCar");
            }


            if (product.Id == 0)
            {
                string CurrentUserId = User.Identity.GetUserId();
                product.IdentityUserId = CurrentUserId;
                product.DateAdded = DateTime.Now;
                product.inStock = true;
                product.Views = 0;

                if(image1 != null)
                {
                    product.Image = new byte[image1.ContentLength];
                    image1.InputStream.Read(product.Image, 0, image1.ContentLength);
                }

               

                _context.Products.Add(product);

            }
            else
            {
                var prodDb = _context.Products.SingleOrDefault(p => p.Id == product.Id);


                prodDb.Name = product.Name;
                prodDb.Description = product.Description;
                prodDb.Price = product.Price;
                prodDb.Year = product.Year;
                prodDb.Number = product.Number;
                prodDb.Brand = product.Brand;

          


                prodDb.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(prodDb.Image, 0, image1.ContentLength);

                _context.Entry(prodDb).State = EntityState.Modified;

            }   


            /*  try
              {
                  _context.SaveChanges();
              }
              catch(DbEntityValidationException e)
              {
                  Console.WriteLine(e);
              }*/

            _context.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            product.Views = product.Views + 1;

            _context.Entry(product).State = EntityState.Modified;

            _context.SaveChanges();

            string CurrentUserId = User.Identity.GetUserId();

            ViewBag.CurrentUserId = CurrentUserId;

            Review review = new Review();

            var viewmodel = new ProductDetailsModel
            {
                Product = product,
                Review = review,
                Reviews = _context.Reviews.Where(r => r.ProductId == id).ToList()

            };

            return View(viewmodel);
        }

        public ActionResult Edit(int id)
        {

            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            if(product == null)
            {
                return HttpNotFound();
            }

            return View(product);

        }

        public ActionResult DeleteProduct(int id, int? page)
        {


            var delProd = _context.Products.SingleOrDefault(p => p.Id == id);

            _context.Products.Remove(delProd);

            _context.SaveChanges();


            string CurrentUserId = User.Identity.GetUserId();

            var prod = _context.Products.ToList().ToPagedList(page ?? 1, 9);

            var viewModel = new HomeViewModel
            {
                Products = prod,
                CartProducts = _context.CartProducts.Where(c => c.IdentityUserId == CurrentUserId).ToList(),
                AllProducts = _context.Products.ToList()
            };

            return RedirectToAction("Index", "Home", viewModel);
        }

    }
}