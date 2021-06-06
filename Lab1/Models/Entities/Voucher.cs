using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class Voucher
    {
        // ==== поля собственные ====
        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Наименование путевки")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата оформления")]
        [DisplayName("Дата оформления")]
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]

        [Required]
        [DisplayName("Стоимость")]
        public double Price { get; set; }





        // ==== поля-ключи внешние ====
        [Display(Name = "Оформитель")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "тур")]
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        [Display(Name = "Клиент/Покупатель")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}