using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Добавочная стоимость")]
        public double AddedValue { get; set; }

        [Required]
        [DisplayName("Скидка")]
        public double Discount { get; set; }



        public ICollection<Tour> Tours { get; set; }
        public Category()
        {
            Tours = new List<Tour>();
        }
    }
}