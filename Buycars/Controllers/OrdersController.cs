using Buycars.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buycars.Controllers
{
    public class OrdersController : Controller
    {

        private ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Orders
        public ActionResult Index()
        {

            string CurrentUserID = User.Identity.GetUserId();

            var orders = _context.Orders.Where(o => o.IdentityUserId == CurrentUserID).ToList();


            return View(orders);
        }

        [HttpPost]
        public ActionResult Save(int numberProd, int productId)
        {
            string CurrentUserID = User.Identity.GetUserId();

            var data1 = _context.CartProducts.Find(productId);
            var data2 = new Order();

            var prod = _context.Products.Single(p => p.Name == data1.Name);

            var rez = prod.Number - numberProd;

            if (rez == 0)
            {
                prod.inStock = false;
                prod.Number = rez;
            }else if(rez > 0)
            {
                prod.Number = rez;
            }
            else
            {
                return RedirectToAction("Index", "CartProducts");
            }

            data2.IdentityUserId = CurrentUserID;
            data2.Name = data1.Name;
            data2.Image = data1.Image;
            data2.Description = data1.Description;
            data2.DateAdded = DateTime.Now;
            data2.OrderPrice = numberProd * data1.Price;

           


            _context.Orders.Add(data2);
            _context.CartProducts.Remove(data1);

              try
             {
                 _context.SaveChanges();
             }
             catch(DbEntityValidationException e)
             {
                 Console.WriteLine(e);
             }

        //    _context.SaveChanges();



            return RedirectToAction("Index", "CartProducts");
        }

    }
}