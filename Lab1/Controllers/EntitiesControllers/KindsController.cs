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
    public class KindsController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();

        // GET: Kinds
        public ActionResult Index()
        {
            return View(db.Kinds.ToList());
        }

        // GET: Kinds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // GET: Kinds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kinds/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Kind kind)
        {
            if (ModelState.IsValid)
            {
                db.Kinds.Add(kind);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kind);
        }

        // GET: Kinds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: Kinds/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Kind kind)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kind);
        }

        // GET: Kinds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = db.Kinds.Find(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: Kinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kind kind = db.Kinds.Find(id);
            db.Kinds.Remove(kind);
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
