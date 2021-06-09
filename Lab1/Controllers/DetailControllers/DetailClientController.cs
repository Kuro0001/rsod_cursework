using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab1.Models;
using Lab1.Models.DetailModels;
using Lab1.Models.Entities;

namespace Lab1.Controllers.DetailControllers
{
    public class DetailClientController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();
        private DetailClient allTables = new DetailClient();

        // GET: ToSelectVoucher
        public ActionResult ClientDetail(int id)
        {
            ViewData["id"] = id;
            allTables.Client = db.Clients.Find(id);
            List<Voucher> list = db.Vouchers.Where(v => v.ClientId == id).ToList();
            allTables.Vouchers = new List<Voucher>();
            foreach (var item in list)
            {
                item.Client = allTables.Client;
                item.Employee = db.Employees.Find(item.EmployeeId);
                item.Tour = db.Tours.Find(item.TourId);
                allTables.Vouchers.Add(item);
            }
            return View(allTables);
        }
    }
}