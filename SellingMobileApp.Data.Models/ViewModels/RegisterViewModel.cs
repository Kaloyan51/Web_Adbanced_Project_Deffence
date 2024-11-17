using SellingMobileApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейлът е задължителен")]
        [EmailAddress(ErrorMessage = "Невалиден формат на имейл")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Паролата е задължителна")]
        [DataType(DataType.Password)]
        [StringLength(AppConstants.PasswordMaxLength, MinimumLength = AppConstants.PasswordMinLength, ErrorMessage = "Паролата трябва да бъде между 6 и 100 символа")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Потвърждаването на паролата е задължително")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Потребителското име е задължително")]
        [StringLength(AppConstants.UserNameMaxLength, MinimumLength = AppConstants.UserNameMinLength, ErrorMessage = "Потребителското име трябва да бъде между 3 и 50 символа")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефонният номер е задължителен")]
        [Phone(ErrorMessage = "Невалиден телефонен номер")]
        [StringLength(AppConstants.PhoneNumberMaxLength, MinimumLength = AppConstants.PhoneNumberMinLength, ErrorMessage = "Телефонният номер трябва да бъде между 4 и 15 символа")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
