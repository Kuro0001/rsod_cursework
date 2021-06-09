using System.Collections.Generic;
using System.Data.Entity;
using Lab1.Models.Entities;

namespace Lab1.Models
{
    public class ToSelectTour
    {
        public int HotelId { get; set; }
        public List<Hotel> Hotel { get; set; }
        public List<Direction> Direction { get; set; }
        public List<TourOperator> TourOperator { get; set; }
    }
}