using System.Data.Entity;
using Lab1.Models.Entities;

namespace Lab1.Models
{
    public class VoucherFull
    {
        public Voucher Voucher { get; set; }
        public Tour Tour { get; set; }
        public Hotel Hotel { get; set; }
        public Direction Direction { get; set; }
        public TourOperator TourOperator { get; set; }
        public Client Client { get; set; }
        public Employee Employee { get; set; }
    }
}