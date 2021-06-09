using System.Collections.Generic;
using System.Data.Entity;
using Lab1.Models.Entities;

namespace Lab1.Models.DetailModels
{
    public class DetailClient
    {
        public Client Client { get; set; }
        public List<Voucher> Vouchers { get; set; }
    }
}