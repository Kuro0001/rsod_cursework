using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class Client
    {
        // ==== поля собственные ====
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Серия и номер паспорта")]
        public string Pasport { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [StringLength(45)]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        [DisplayName("Дата рождения")]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.Date)]

        [Required]
        [StringLength(45)]
        [DisplayName("Пол")]
        public char Sex { get; set; }

        [Required]
        [StringLength(11)]
        [DisplayName("телефон")]
        public string Phone { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("E-mail")]
        public string Email { get; set; }



        // ==== поля-ключи внешние ====
        public ICollection<Voucher> Vouchers { get; set; }
        public Client()
        {
            Vouchers = new List<Voucher>();
        }
    }
}