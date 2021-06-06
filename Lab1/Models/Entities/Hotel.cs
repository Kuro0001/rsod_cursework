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
    public class Hotel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Наименование отеля должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Наименование отеля")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Адрес должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Адрес")]
        public string Address { get; set; }


        [DisplayName("Фото")]
        public byte[] Photo { get; set; }             




        [Required]
        [DisplayName("Направление")]
        public int DirectionId { get; set; }
        public Direction Direction { get; set; }



        public ICollection<Tour> Tours { get; set; }
        public Hotel()
        {
            Tours = new List<Tour>();
        }
    }
}