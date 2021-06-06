using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class Hotel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Адрес")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Фото")]
        public byte[] Photo { get; set; }



        public int DirectionId { get; set; }
        public Direction Direction { get; set; }



        public ICollection<Tour> Tours { get; set; }
        public Hotel()
        {
            Tours = new List<Tour>();
        }
    }
}