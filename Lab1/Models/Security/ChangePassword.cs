using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models.Security
{
    public class ChangePassword
    {
        public string Id { get; set; }


        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 50 символов")]
        public string NewPassword { get; set; }


        [Required]
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }
    }
}