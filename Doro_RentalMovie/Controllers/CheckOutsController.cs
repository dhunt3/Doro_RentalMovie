using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Doro_RentalMovie.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Doro_RentalMovie.Controllers
{
    public class CheckOutsController : Controller
    {
        private masterMovieEntities db = new masterMovieEntities();

        // GET: CheckOuts
        [Authorize]
        public ActionResult Index()
        {
            var checkOuts = db.CheckOuts.Include(c => c.Customer).Include(c => c.Movie);
            return View(checkOuts.ToList());
        }

        public ActionResult Overdue()   //will select overdue accounts. I created this
        {
            var checkOuts = db.CheckOuts.Include(c => c.Customer).Include(c => c.Movie)
                .Where(c => c.Return_Date < DateTime.Now);
            // Lambda expression is a fancy function. c => c.Customer, where the c is 
            // the input and c.Customer is the output. For each c in db.Checkouts
            // (checkOuts db set), we want to include its Customer (.Customer) property.
            return View(checkOuts.ToList());    // ToList() causes the query to execute
                                                // in this case.
        }

        // GET: CheckOuts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOuts.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // GET: CheckOuts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Last_Name");
            /*var moviesId = db.Movies.Include(m => m.MovieID);
            var checkOutMovieId = db.CheckOuts.Include(c => c.MovieID);
            if (moviesId != checkOutMovieId)
            {*/
                //ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title");
                var availableMovies = from movie in db.Movies
                                      join checkout in db.CheckOuts
                                      on movie.MovieID equals checkout.MovieID
                                      where checkout.MovieID == null
                                      select movie;
                ViewBag.MovieID = new SelectList(availableMovies, "MovieID", "Title");
            //}
            return View();
        }

        // POST: CheckOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckOutID,Price,CustomerID,MovieID,Checkout_Date,Return_Date")] CheckOut checkOut)
        {
            if (ModelState.IsValid)
            {
                //var returnMovie = db.CheckOuts.Include(c => c.MovieID);
                //Movie movie = db.Movies.Find(returnMovie);
                //ReturnMovie addReturnMovie = new ReturnMovie(); //new ReturnMovie(checkOut.CustomerID, checkOut.Movie.MovieID, checkOut.Movie.Title, checkOut.Movie.Director, checkOut.Movie.Genre);
                

                db.CheckOuts.Add(checkOut);
                //db.ReturnMovies.Add(addReturnMovie);
                //db.Movies.Remove(movie);
                //db.Database.ExecuteSqlCommand("UPDATE [dbo].[CheckOut] SET [Return Date] = CONVERT (datetime, DATEADD(day,3,[Checkout Date]), 107)");
                //db.CheckOuts.SqlQuery("UPDATE [dbo].[CheckOut] SET [Return Date] = CONVERT (datetime, DATEADD(day,3,[Checkout Date]), 107)").ToList();  // Find a way to update the return date column. Or run the query created for it.
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Last_Name", checkOut.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", checkOut.MovieID);
            return View(checkOut);
        }

        // GET: CheckOuts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOuts.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Last_Name", checkOut.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", checkOut.MovieID);
            return View(checkOut);
        }

        // POST: CheckOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckOutID,Price,CustomerID,MovieID,Checkout_Date,Return_Date")] CheckOut checkOut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Last_Name", checkOut.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", checkOut.MovieID);
            return View(checkOut);
        }

        // add a return method that will add the deleted movie belonging to CustomerID to 
        // Movies db (Movie table) from the ReturnMovie table. Remove returned movie from
        // ReturnTable

        /*public ActionResult Return()
        {
         * 

        }*/


        // GET: CheckOuts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = db.CheckOuts.Find(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // POST: CheckOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckOut checkOut = db.CheckOuts.Find(id);
            //var returnMovie = db.ReturnMovies.Include(m => m.MovieID);
            //ReturnMovie removeMovie = db.ReturnMovies.Find(returnMovie);
            //Movie addMovie = new Movie();
            //  add option to add movie to ReturnMovie sql table
            db.CheckOuts.Remove(checkOut);
            //db.ReturnMovies.Remove(removeMovie);
            //db.Movies.Add(addMovie);
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
