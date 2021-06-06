using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class TourOperator
    {
        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Наименование туроператора")]
        public string Name { get; set; }

        [Required]
        [StringLength(11)]
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [Required]
        [StringLength(13)]
        [DisplayName("Уникальный  номер юр. лица")]
        public string UniqCompanyNumber { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("E-mail")]
        public string Email { get; set; }





        public ICollection<Tour> Tours { get; set; }
        public TourOperator()
        {
            Tours = new List<Tour>();
        }
    }
}