using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BookRental.Controllers
{
    public class HomeController : Controller
    {
        private BookRental.Models.BookRentalContext db = new Models.BookRentalContext();
        public ActionResult Index()
        {
            
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName");
            string id = db.Categories.ToList()[0].categoryID;
            ViewBag.books = db.Books.Where(b=>b.categoryID== id);
            return View();
        }
        [HttpPost]
        public ActionResult Index(string categoryID)
        {
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName",categoryID);
            ViewBag.books = db.Books.Where(b=>b.categoryID==categoryID);
            return View("Index");
        }

        public ActionResult BookSelected(string bookid)
        {
            //Session.Add("bookid", bookid);
            //return View();
            return RedirectToAction("Index","Info",new { id=bookid });
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}