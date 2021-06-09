using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab1.Models.Entities;

namespace Lab1.Models
{
    public class ToSelectVoucher
    {
        public int TourId { get; set; }
        public List<Tour> Tour { get; set; }
        public int ClientId { get; set; }
        public List<Client> Client { get; set; }
        public int EmployeeId { get; set; }
        public List<Employee> Employee { get; set; }
    }
}