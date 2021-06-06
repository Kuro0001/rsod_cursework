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
    public class Kind
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Поле Наименование вида должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Наименование вида")]
        public string Name { get; set; }


        public ICollection<Tour> Tours { get; set; }
        public Kind()
        {
            Tours = new List<Tour>();
        }
    }
}