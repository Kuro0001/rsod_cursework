using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class Tour
    {
        // ==== поля собственные ====
        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Наименование тура")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Кол-во предложений")]
        public int OffersAll { get; set; }

        [Required]
        [DisplayName("Цена за день на человека")]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала тура")]
        [DisplayName("Дата начала тура")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]

        [Required]
        [DisplayName("Кол-во дней в туре")]
        public int DayCount { get; set; }


        // ==== поля-ключи внешние ====
        [Display(Name = "Туроператор")]
        public int TourOperatorId { get; set; }
        public TourOperator TourOperator { get; set; }

        [Display(Name = "Вид")]
        public int KindId { get; set; }
        public Kind Kind { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

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