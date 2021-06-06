using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Lab1.Models.Entities
{
    public class Employee
    {        
        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("E-mail")]
        public string Email { get; set; }




        public ICollection<Voucher> Vouchers { get; set; }
        public Employee()
        {
            Vouchers = new List<Voucher>();
        }
    }
}