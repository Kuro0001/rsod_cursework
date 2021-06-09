using System.Collections.Generic;
using System.Data.Entity;
using Lab1.Models.Entities;

namespace Lab1.Models.DetailModels
{
    public class DetailEmployee
    {
        public string bio { get; set; }

        public int count { get; set; }

        public decimal money { get; set; }
    }
}