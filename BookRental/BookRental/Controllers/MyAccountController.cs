using BookRental.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BookRental.Controllers
{
    [Authorize(Roles ="Users")]
    public class MyAccountController : Controller
    {
        private BookRentalContext db = new BookRentalContext();
        // GET: MyAccount
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountDetails()
        {
            return View();
        }
        public ActionResult Mybooks()
        {
            if(Request.IsAuthenticated)
            {
                string uname = User.Identity.GetUserName();
                var books = db.Bookings.Include("book").Where(u=>u.username==uname);
                
                return View(books.ToList());
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult Read()
        {
                return RedirectToAction("Mybooks", "MyAccount");
        }

        [HttpPost]
        public ActionResult Read(FormCollection f)
        {
            if(Request.IsAuthenticated && f["item.bookID"]!= null)
            {
                string id=f["item.bookID"];
                ViewBag.path = db.Books.FirstOrDefault(b=>b.bookID==id).pdfurl;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}