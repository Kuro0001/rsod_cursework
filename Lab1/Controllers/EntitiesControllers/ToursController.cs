using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab1.Models;
using Lab1.Models.Entities;

namespace Lab1.Controllers.EntitiesControllers
{
    public class ToursController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();

        // GET: Tours
        public ActionResult Index()
        {
            var tours = db.Tours.Include(t => t.Category).Include(t => t.Hotel).Include(t => t.Kind).Include(t => t.TourOperator);
            return View(tours.ToList());
        }

        // GET: Tours
        public ActionResult SelectTourToCreateVoucher()
        {
            var tours = db.Tours.Include(t => t.Category).Include(t => t.Hotel).Include(t => t.Kind).Include(t => t.TourOperator);
            return View(tours.ToList());
        }

        // GET: Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            tour.Hotel = db.Hotels.Find(tour.HotelId);
            tour.Category = db.Categorys.Find(tour.CategoryId);
            tour.Kind = db.Kinds.Find(tour.KindId);
            tour.TourOperator = db.TourOperators.Find(tour.TourOperatorId);
            return View(tour);
        }

        // GET: Tours/Create
        public ActionResult Create(int idHotel, int idTourOperator)
        {
            Hotel Hotel = db.Hotels.Find(idHotel);
            Hotel.Direction = db.Directions.Find(Hotel.DirectionId);
            ViewData["HotelName"] = Hotel.Name + ", " + Hotel.Direction.Name;
            ViewData["HotelId"] = Hotel.ID;
            TourOperator oper = db.TourOperators.Find(idTourOperator);
            ViewData["TourOperatorName"] = oper.Name;
            ViewData["TourOperatorId"] = oper.ID;


            ViewBag.CategoryId = new SelectList(db.Categorys, "ID", "Name");
            ViewBag.KindId = new SelectList(db.Kinds, "ID", "Name");
            return View();
        }

        // POST: Tours/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,OffersAll,Price,StartDate,DayCount,TourOperatorId,KindId,CategoryId,HotelId")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categorys, "ID", "Name", tour.CategoryId);
            ViewBag.HotelId = new SelectList(db.Hotels, "ID", "Name", tour.HotelId);
            ViewBag.KindId = new SelectList(db.Kinds, "ID", "Name", tour.KindId);
            ViewBag.TourOperatorId = new SelectList(db.TourOperators, "ID", "Name", tour.TourOperatorId);
            return View(tour);
        }

        // GET: Tours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            tour.Hotel = db.Hotels.Find(tour.HotelId);
            tour.TourOperator = db.TourOperators.Find(tour.TourOperatorId);
            ViewBag.CategoryId = new SelectList(db.Categorys, "ID", "Name", tour.CategoryId);
            ViewBag.KindId = new SelectList(db.Kinds, "ID", "Name", tour.KindId);
            return View(tour);
        }

        // POST: Tours/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,OffersAll,Price,StartDate,DayCount,TourOperatorId,KindId,CategoryId,HotelId")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categorys, "ID", "Name", tour.CategoryId);
            ViewBag.HotelId = new SelectList(db.Hotels, "ID", "Name", tour.HotelId);
            ViewBag.KindId = new SelectList(db.Kinds, "ID", "Name", tour.KindId);
            ViewBag.TourOperatorId = new SelectList(db.TourOperators, "ID", "Name", tour.TourOperatorId);
            return View(tour);
        }

        // GET: Tours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            tour.Hotel = db.Hotels.Find(tour.HotelId);
            tour.Category = db.Categorys.Find(tour.CategoryId);
            tour.Kind = db.Kinds.Find(tour.KindId);
            tour.TourOperator = db.TourOperators.Find(tour.TourOperatorId);
            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
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
