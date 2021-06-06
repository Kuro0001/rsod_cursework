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
    public class VouchersController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();

        // GET: Vouchers
        public ActionResult Index()
        {
            var vouchers = db.Vouchers.Include(v => v.Client).Include(v => v.Employee).Include(v => v.Tour);
            return View(vouchers.ToList());
        }

        // GET: Vouchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // GET: Vouchers/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ID", "Pasport");
            ViewBag.EmployeeId = new SelectList(db.Employees, "ID", "Name");
            ViewBag.TourId = new SelectList(db.Tours, "ID", "Name");
            return View();
        }

        // POST: Vouchers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Date,Price,EmployeeId,TourId,ClientId")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ID", "Pasport", voucher.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "ID", "Name", voucher.EmployeeId);
            ViewBag.TourId = new SelectList(db.Tours, "ID", "Name", voucher.TourId);
            return View(voucher);
        }

        // GET: Vouchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ID", "Pasport", voucher.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "ID", "Name", voucher.EmployeeId);
            ViewBag.TourId = new SelectList(db.Tours, "ID", "Name", voucher.TourId);
            return View(voucher);
        }

        // POST: Vouchers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Date,Price,EmployeeId,TourId,ClientId")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ID", "Pasport", voucher.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "ID", "Name", voucher.EmployeeId);
            ViewBag.TourId = new SelectList(db.Tours, "ID", "Name", voucher.TourId);
            return View(voucher);
        }

        // GET: Vouchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voucher voucher = db.Vouchers.Find(id);
            db.Vouchers.Remove(voucher);
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
