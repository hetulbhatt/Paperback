using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookRental.Models;
using System.IO;

namespace BookRental.Controllers.Manager
{
    public class BooksController : Controller
    {
        private BookRentalContext db = new BookRentalContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Category);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase imgurl, HttpPostedFileBase pdfurl,[Bind(Include = "bookID,bookName,author,publisher,language,isbn,pages,description,yearofPublication,rent,imgurl,pdfurl,categoryID")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (db.Books.Where(x => x.bookID == book.bookID).Count() == 0)
                {
                    try
                    {

                        //Method 2 Get file details from HttpPostedFileBase class    

                        if (imgurl != null)
                        {
                            string path1 = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(imgurl.FileName));
                            string path2 = Path.Combine(Server.MapPath("~/Content/pdf"), Path.GetFileName(pdfurl.FileName));
                            book.imgurl = "~/Content/Images/"+imgurl.FileName;
                            book.pdfurl = "/Content/pdf/" + pdfurl.FileName;
                            imgurl.SaveAs(path1);
                            pdfurl.SaveAs(path2);
                        }
                        ViewBag.FileStatus = "File uploaded successfully.";
                    }
                    catch (Exception)
                    {
                        ViewBag.FileStatus = "Error while file uploading."; ;
                    }
                    
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "BookID already exist.");
                    return View("Create");
                }
                
            }

            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", book.categoryID);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", book.categoryID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase imgurl, HttpPostedFileBase pdfurl, [Bind(Include = "bookID,bookName,author,publisher,language,isbn,pages,description,yearofPublication,rent,imgurl,pdfurl,categoryID")] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (imgurl != null && pdfurl!=null)
                    {
                        string path1 = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(imgurl.FileName));
                        string path2 = Path.Combine(Server.MapPath("~/Content/pdf"), Path.GetFileName(pdfurl.FileName));
                        book.imgurl = "~/Content/Images/"+imgurl.FileName;
                        book.pdfurl = "/Content/pdf/" +pdfurl.FileName;
                        imgurl.SaveAs(path1);
                        pdfurl.SaveAs(path2);
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", book.categoryID);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
