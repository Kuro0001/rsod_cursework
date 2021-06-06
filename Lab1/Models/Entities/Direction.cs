using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class Direction
    {
        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Наименование направления")]
        public string Name { get; set; }


        public ICollection<Hotel> Hotels { get; set; }
        public Direction()
        {
            Hotels = new List<Hotel>();
        }
    }
}