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
    public class Voucher
    {
        // ==== поля собственные ====
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Наименование путевки должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Наименование путевки")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Дата оформления должно быть установлено")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата оформления")]
        [DisplayName("Дата оформления")]
        public DateTime Date { get; set; }        

        [Required(ErrorMessage = "Поле Стоимость должно быть установлено")]
        [DisplayName("Стоимость")]
        public decimal Price { get; set; }





        // ==== поля-ключи внешние ====
        [Required]
        [Display(Name = "Оформитель")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        [Display(Name = "тур")]
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        [Required]
        [Display(Name = "Клиент(Покупатель)")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}