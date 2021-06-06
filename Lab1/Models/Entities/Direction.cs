using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace Lab1.Models.Entities
{
    public class Direction
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Наименование направления должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Наименование направления")]
        public string Name { get; set; }


        public ICollection<Hotel> Hotels { get; set; }
        public Direction()
        {
            Hotels = new List<Hotel>();
        }
    }
}