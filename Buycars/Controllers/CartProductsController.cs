using Buycars.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buycars.Controllers
{
    public class CartProductsController : Controller
    {

        private ApplicationDbContext _context;

        public CartProductsController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Cart
        public ActionResult Index()
        {

            string CurrentUserId = User.Identity.GetUserId();


            var cart = _context.CartProducts.Where(c => c.IdentityUserId == CurrentUserId).ToList();


            return View(cart);
        }

        public ActionResult Save(int id)
        {

            var data1 = _context.Products.Find(id);
            var data2 = new CartProduct();

            string CurrentUserId = User.Identity.GetUserId();

            data2.IdentityUserId = CurrentUserId;
            data2.Image = data1.Image;
            data2.Year = data1.Year;
            data2.Price = data1.Price;
            data2.Name = data1.Name;
            data2.Description = data1.Description;
            data2.DateAddedToCart = DateTime.Now;
            data2.DateAdded = data1.DateAdded;

            _context.CartProducts.Add(data2);
         

            _context.SaveChanges();
            

            return RedirectToAction("Index", "Home");
        }


        public ActionResult DeleteProduct(int id)
        {

            var product = _context.CartProducts.SingleOrDefault(c => c.Id == id);
            _context.CartProducts.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}