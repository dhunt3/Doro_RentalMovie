using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Doro_RentalMovie.Models;

namespace Doro_RentalMovie.Controllers
{
    public class ReturnMoviesController : Controller
    {
        private masterMovieEntities db = new masterMovieEntities();

        // GET: ReturnMovies
        [Authorize]
        public ActionResult Index()
        {
            var returnMovies = db.ReturnMovies.Include(r => r.Customer).Include(r => r.Movie);
            return View(returnMovies.ToList());
        }

        // GET: ReturnMovies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnMovie returnMovie = db.ReturnMovies.Find(id);
            if (returnMovie == null)
            {
                return HttpNotFound();
            }
            return View(returnMovie);
        }

        // GET: ReturnMovies/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Last_Name");
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title");
            return View();
        }

        // POST: ReturnMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReturnMovieID,CustomerID,MovieID,Title,Director,Genre")] ReturnMovie returnMovie)
        {
            if (ModelState.IsValid)
            {
                db.ReturnMovies.Add(returnMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Last_Name", returnMovie.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", returnMovie.MovieID);
            return View(returnMovie);
        }

        // GET: ReturnMovies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnMovie returnMovie = db.ReturnMovies.Find(id);
            if (returnMovie == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Last_Name", returnMovie.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", returnMovie.MovieID);
            return View(returnMovie);
        }

        // POST: ReturnMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReturnMovieID,CustomerID,MovieID,Title,Director,Genre")] ReturnMovie returnMovie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnMovie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Last_Name", returnMovie.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Title", returnMovie.MovieID);
            return View(returnMovie);
        }

        // GET: ReturnMovies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnMovie returnMovie = db.ReturnMovies.Find(id);
            if (returnMovie == null)
            {
                return HttpNotFound();
            }
            return View(returnMovie);
        }

        // POST: ReturnMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReturnMovie returnMovie = db.ReturnMovies.Find(id);
            db.ReturnMovies.Remove(returnMovie);
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
