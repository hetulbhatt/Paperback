using BookRental.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookRental.Controllers
{
    public class RentController : Controller
    {
        // GET: Rent
        private BookRental.Models.BookRentalContext db = new BookRental.Models.BookRentalContext();
        [Authorize(Roles ="Users")]
        public ActionResult Index()
        {
            if(Session["book"] != null)
            {
                List<int> l = new List<int>();
                for (var i = 1;i<= 12;i++)
                {
                    l.Add(i);
                }
                ViewBag.sub = new SelectList(l,l[0]);
                ViewBag.book = Session["book"];
                ViewBag.rent = ViewBag.book.rent;
                ViewBag.booking = new BookRental.Models.Booking();
                ViewBag.transaction = new BookRental.Models.Transaction();
                return View();
            }
            else
            {
                return RedirectToAction("Index","Info");
            }
            
        }

        public ActionResult Payment(BookRental.Models.Transaction t,int subscriptionPeriod)
        {
            if(ModelState.IsValid)
            {
                BookRental.Models.Booking b = new BookRental.Models.Booking();
                t=db.Transactions.Add(t);
                b.username = User.Identity.GetUserName();
                b.bookID = Session["bookid"].ToString();
                b.subscriptionDate = DateTime.Now.Date;
                b.subscriptionPeriod = subscriptionPeriod;
                b.subscriptionexpiryDate = DateTime.Now.Date.AddMonths(b.subscriptionPeriod);
                b.transactionID = t.TransactionID;
                b.subcriptionfees = Convert.ToInt32(((BookRental.Models.Book)Session["book"]).rent)*subscriptionPeriod;
                db.Bookings.Add(b);
                if(db.SaveChanges()==2)
                {
                    Session.Remove("book");
                    TempData["amountpaid"] = b.subcriptionfees;
                    TempData["subscriptionPeriod"] = b.subscriptionPeriod;
                    return RedirectToAction("Confirmation");
                }
                else
                {
                    Session.Remove("book");
                    return View("Index");
                }
                
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult Confirmation()
        {
            if (Session["bookid"] != null)
            {
                string id = Session["bookid"].ToString();
                Book model=db.Books.FirstOrDefault(b => b.bookID == id);
                Session.Remove("bookid");
                return View(model);
            }
            else
            {
                return RedirectToAction("Mybooks","MyAccount");
            }
            
        }
    }
}