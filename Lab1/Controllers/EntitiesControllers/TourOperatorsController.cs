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
    public class TourOperatorsController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();

        // GET: TourOperators
        public ActionResult Index()
        {
            return View(db.TourOperators.ToList());
        }

        // GET: TourOperators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourOperator tourOperator = db.TourOperators.Find(id);
            if (tourOperator == null)
            {
                return HttpNotFound();
            }
            return View(tourOperator);
        }

        // GET: TourOperators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TourOperators/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Phone,UniqCompanyNumber,Email")] TourOperator tourOperator)
        {
            if (ModelState.IsValid)
            {
                db.TourOperators.Add(tourOperator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tourOperator);
        }

        // GET: TourOperators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourOperator tourOperator = db.TourOperators.Find(id);
            if (tourOperator == null)
            {
                return HttpNotFound();
            }
            return View(tourOperator);
        }

        // POST: TourOperators/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Phone,UniqCompanyNumber,Email")] TourOperator tourOperator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourOperator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tourOperator);
        }

        // GET: TourOperators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourOperator tourOperator = db.TourOperators.Find(id);
            if (tourOperator == null)
            {
                return HttpNotFound();
            }
            return View(tourOperator);
        }

        // POST: TourOperators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TourOperator tourOperator = db.TourOperators.Find(id);
            db.TourOperators.Remove(tourOperator);
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
