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
    public class ToSelectForTourController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();
        private ToSelectTour allTables = new ToSelectTour();

        // GET: SelectDirection
        public ActionResult SelectDirection()
        {
            allTables.Direction = db.Directions.ToList();
            return View(allTables);
        }
        // GET: SelectHotel
        public ActionResult SelectHotel(int idDirection)
        {
            allTables.Hotel = new List<Hotel>();
            List<Hotel> hotelList = db.Hotels.Where(h => h.DirectionId == idDirection).ToList();

            foreach (var item in hotelList)
            {
                item.Direction = db.Directions.Find(item.DirectionId);
                allTables.Hotel.Add(item);
            }
            return View(allTables);
        }
        // GET: SelectTourOperator
        public ActionResult SelectTourOperator(int idHotel)
        {
            allTables.TourOperator = db.TourOperators.ToList();
            allTables.HotelId = idHotel;
            ViewData["HotelId"] = idHotel;
            return View(allTables);
        }
    }
}