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

namespace Lab1.Controllers.ToSelect
{
    public class ToSelectVoucherController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();
        private ToSelectVoucher allTables = new ToSelectVoucher();

        // GET: ToSelectVoucher
        public ActionResult SelectTour()
        {
            allTables.Tour = new List<Tour>();
            List<Tour> list = db.Tours.ToList();
            foreach(var item in list)
            {
                item.Category = db.Categorys.Find(item.CategoryId);
                item.Kind = db.Kinds.Find(item.KindId);
                item.Hotel = db.Hotels.Find(item.HotelId);
                item.TourOperator = db.TourOperators.Find(item.TourOperatorId);
                allTables.Tour.Add(item);
            }
            return View(allTables);
        }

        // GET: ToSelectVoucher
        public ActionResult SelectEmployee(int idTour)
        {
            allTables.TourId = idTour;
            ViewData["TourId"] = idTour;
            allTables.Employee = db.Employees.ToList();
            return View(allTables);
        }

        // GET: ToSelectVoucher
        public ActionResult SelectClient(int idTour, int idEmployee)
        {
            allTables.TourId = idTour;
            ViewData["TourId"] = idTour;
            allTables.EmployeeId = idEmployee;
            ViewData["EmployeeId"] = idEmployee;
            allTables.Client = db.Clients.ToList();
            return View(allTables);
        }
    }
}