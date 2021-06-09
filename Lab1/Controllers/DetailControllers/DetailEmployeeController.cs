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
    public class DetailEmployeeController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();
        private List<DetailEmployee> allTables = new List<DetailEmployee>();

        // GET: ToSelectInfo
        public ActionResult EmployeeDetail()
        {
            List<Employee> list_emp = db.Employees.ToList();
            foreach (var item in list_emp)
            {
                List<Voucher> list_vouch = db.Vouchers.Where(e => e.EmployeeId == item.ID).ToList();
                double money = 0;
                foreach(var item_vouch in list_vouch)
                {
                    money += Convert.ToDouble(item_vouch.Price);
                }
                DetailEmployee result = new DetailEmployee();

                result.bio = item.Surname + " " + item.Name;
                result.count = list_vouch.Count;
                result.money = Convert.ToDecimal(money);
                allTables.Add(result);
            }
            return View(allTables);
        }
    }
}