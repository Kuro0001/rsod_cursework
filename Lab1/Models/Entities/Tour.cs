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
    public class Tour
    {
        // ==== поля собственные ====
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Наименование тура должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Наименование тура")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Кол-во предложений должно быть установлено")]
        [DisplayName("Кол-во предложений")]
        public int OffersAll { get; set; }

        [Required(ErrorMessage = "Поле Цена за день на человека должно быть установлено")]
        [DisplayName("Цена за день на человека")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле Дата начала тура должно быть установлено")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала тура")]
        [DisplayName("Дата начала тура")]
        public DateTime StartDate { get; set; } 

        [Required(ErrorMessage = "Поле Кол-во дней в туре должно быть установлено")]
        [DisplayName("Кол-во дней в туре")]
        public int DayCount { get; set; }




        // ==== поля-ключи внешние ====
        [Required]
        [Display(Name = "Туроператор")]
        public int TourOperatorId { get; set; }
        public TourOperator TourOperator { get; set; }

        [Required]
        [Display(Name = "Вид")]
        public int KindId { get; set; }
        public Kind Kind { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Отель")]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }



        public ICollection<Voucher> Vouchers { get; set; }
        public Tour()
        {
            Vouchers = new List<Voucher>();
        }
    }
}