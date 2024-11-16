using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellingMobileApp.Common;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Display(Name = "Име на потребител")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public int ListingId { get; set; }

        [Required]
        [Range(AppConstants.RatingMinLength, AppConstants.RatingMaxLength, ErrorMessage = "Оценката трябва да бъде между 1 и 5")]
        public int Rating { get; set; }
    }
}
