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
    public class Employee
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Имя должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Фамилия должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле Отчество должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Поле E-mail должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Логин должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле Пароль должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [PasswordPropertyText]
        [DisplayName("Пароль")]
        public string Password { get; set; }


        public ICollection<Voucher> Vouchers { get; set; }
        public Employee()
        {
            Vouchers = new List<Voucher>();
        }
    }
}