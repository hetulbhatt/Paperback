using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookRental.Controllers
{
    public class InfoController : Controller
    {
        private BookRental.Models.BookRentalContext db = new Models.BookRentalContext();
        // GET: Info
        public ActionResult Index(string id)
        {
            if (!User.IsInRole("Manager") && id != null)
            {
                Session.Add("bookid",id);
                ViewBag.rented = false;
                Session.Add("book",db.Books.FirstOrDefault(m => m.bookID == id));
                string uname = User.Identity.GetUserName();
                if (db.Bookings.FirstOrDefault(u=>u.username==uname && u.bookID==id)!=null)
                {
                    ViewBag.rented = true;
                }
                ViewBag.book = Session["book"];
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            

        }
    }
}