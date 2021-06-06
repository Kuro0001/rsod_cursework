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
    public class Client
    {
        // ==== поля собственные ====
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Серия и номер паспорта должно быть установлено")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Длина строки должна быть 10 символов")]
        [RegularExpression(@"\d*", ErrorMessage = "Доступны только числа")]
        [DisplayName("Серия и номер паспорта")]
        public string Pasport { get; set; }

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

        [Required(ErrorMessage = "Поле Дата рождения должно быть установлено")]
        [Display(Name = "Дата рождения")]
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }        

        [Required(ErrorMessage = "Поле Пол должно быть установлено")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Длина строки должна быть 1 символ")]
        [DisplayName("Пол")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Поле телефон должно быть установлено")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Длина строки должна быть 11 символов")]
        [RegularExpression(@"\d*", ErrorMessage = "Доступны только числа")]
        [Phone]
        [DisplayName("телефон")]
        public string Phone { get; set; }

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


        // ==== поля-ключи внешние ====
        public ICollection<Voucher> Vouchers { get; set; }
        public Client()
        {
            Vouchers = new List<Voucher>();
        }
    }
}