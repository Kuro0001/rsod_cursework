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
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Наименование категории должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Наименование категории")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Добавочная стоимость должно быть установлено")]
        [DisplayName("Добавочная стоимость")]
        public decimal AddedValue { get; set; }

        [Required(ErrorMessage = "Поле Коэффециент стоимости должно быть установлено")]
        [DisplayName("Коэффециент стоимости")]
        [Range(typeof(decimal), "0.00", "2.00")]
        public decimal Discount { get; set; }



        public ICollection<Tour> Tours { get; set; }
        public Category()
        {
            Tours = new List<Tour>();
        }
    }
}