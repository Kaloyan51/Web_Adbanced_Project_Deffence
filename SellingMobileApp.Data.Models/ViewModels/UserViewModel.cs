using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellingMobileApp.Common;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Име")]
        [StringLength(AppConstants.UserNameMaxLength, MinimumLength = AppConstants.UserNameMinLength, ErrorMessage = "Името трябва да бъде между 3 и 50 символа")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейът е задължителен")]
        [EmailAddress(ErrorMessage = "Невалиден формат на имейл")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(AppConstants.PhoneNumberMaxLength, MinimumLength = AppConstants.PhoneNumberMinLength, ErrorMessage = "Телефонният номер трябва да бъде между 4 и 15 символа")]
        public string PhoneNumber { get; set; } = string.Empty;

        public List<ListingViewModel> Listings { get; set; } = new List<ListingViewModel>();
    }
}
