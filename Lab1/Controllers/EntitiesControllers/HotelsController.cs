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
using System.IO;
using System.Xml;

namespace Lab1.Controllers.EntitiesControllers
{
    public class HotelsController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();

        // GET: Hotels
        public ActionResult Index()
        {
            var hotels = db.Hotels.Include(h => h.Direction);
            return View(hotels.ToList());
        }

        // GET: Hotels
        public ActionResult SelectHotelToCreatTour(int IdDirection)
        {
            var hotels = db.Hotels.Include(h => h.Direction).Where(h => h.DirectionId == IdDirection);
            return View(hotels.ToList());
        }


        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            hotel.Direction = db.Directions.Find(hotel.DirectionId);
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create(int id)
        {
            setDirection(id);
            return View();
        }

        public void setDirection(int id)
        {
            ViewData["Name"] = db.Directions.Find(id).Name;
            ViewData["Id"] = db.Directions.Find(id).ID;
            ViewBag.Direction = db.Directions.Find(id);
        }
        // POST: Hotels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,DirectionId")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DirectionId = new SelectList(db.Directions, "ID", "Name", hotel.DirectionId);
            return View(hotel);
        }


        



        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            hotel.Direction = db.Directions.Find(hotel.DirectionId);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,DirectionId")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DirectionId = new SelectList(db.Directions, "ID", "Name", hotel.DirectionId);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            hotel.Direction = db.Directions.Find(hotel.DirectionId);
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
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

        public ActionResult LoadFromAPI()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:5001/hotel");
            request.Accept = "application/xml";
            WebResponse response = request.GetResponse();
            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());
            XmlElement root = doc.DocumentElement;
            foreach (XmlElement node in root)
            {
                int id = Convert.ToInt32(node["ID"].InnerText);
                Hotel hotel = db.Hotels.Find(id);
                if (hotel == null)
                {
                    //Изменить ввод полей и проверить на уникальность айдишника
                    Hotel note = new Hotel()
                    {
                        Name = node["Name"].InnerText,
                        Address = node["Address"].InnerText,
                        DirectionId = Convert.ToInt32(node["DirectionId"].InnerText)
                    };
                    db.Hotels.Add(note);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
