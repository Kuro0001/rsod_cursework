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
    public class TourOperator
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле Наименование компании туроператора должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [DisplayName("Наименование компании туроператора")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Телефон должно быть установлено")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Длина строки должна быть 11 символов")]
        [RegularExpression(@"\d*", ErrorMessage = "Доступны только числа")]
        [Phone]
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле Уникальный  номер юр. лица должно быть установлено")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Длина строки должна быть 13 символов")]
        [RegularExpression(@"\d*", ErrorMessage = "Доступны только числа")]
        [DisplayName("Уникальный  номер юр. лица")]
        public string UniqCompanyNumber { get; set; }

        [Required(ErrorMessage = "Поле E-mail должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [EmailAddress(ErrorMessage = "Введеная строка не является адресом электронной почты")]
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



        public ICollection<Tour> Tours { get; set; }
        public TourOperator()
        {
            Tours = new List<Tour>();
        }
    }
}