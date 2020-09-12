using Buycars.Migrations;
using Buycars.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buycars.Controllers
{
    public class ReviewsController : Controller
    {

        private ApplicationDbContext _context;

        public ReviewsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Save(Review review, int ProductId)
        {
            string CurrentUserId = User.Identity.GetUserId();
            review.IdentityUserId = CurrentUserId;
            review.ProductId = ProductId;

            _context.Reviews.Add(review);

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
    }
}