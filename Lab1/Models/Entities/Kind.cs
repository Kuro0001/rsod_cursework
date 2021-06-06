using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class Kind
    {
        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Наименование вида")]
        public string Name { get; set; }


        public ICollection<Tour> Tours { get; set; }
        public Kind()
        {
            Tours = new List<Tour>();
        }
    }
}