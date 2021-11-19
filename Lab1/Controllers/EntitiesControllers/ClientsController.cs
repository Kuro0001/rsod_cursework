using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Lab1.Models;
using Lab1.Models.Entities;

namespace Lab1.Controllers.EntitiesControllers
{
    public class ClientsController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();


        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create(string email)
        {
            ViewBag.email = email;
            return View();
        }

        // POST: Clients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Pasport,Name,Surname,Patronymic,BirthDate,Sex,Phone,Email,Login,Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Pasport,Name,Surname,Patronymic,BirthDate,Sex,Phone,Email,Login,Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:44356/client");
            request.Accept = "application/xml";
            WebResponse response = request.GetResponse();
            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());
            XmlElement root = doc.DocumentElement;
            foreach (XmlElement clientNode in root)
            {
                int id = Convert.ToInt32(clientNode["Id"].InnerText);
                Client client = db.Clients.Find(id);
                if (client == null)
                {
                    //Изменить ввод полей и проверить на уникальность айдишника
                    Client newClient = new Client()
                    {
                        FIO = clientNode["FIO"].InnerText,
                        Group = clientNode["Group"].InnerText,
                        PhoneNo = clientNode["PhoneNo"].InnerText,
                        Scholarship = Convert.ToDecimal(clientNode["Scholarship"].InnerText.Replace(".", ","))
                    };
                    db.Clients.Add(newClient);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
