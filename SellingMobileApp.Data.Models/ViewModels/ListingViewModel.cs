using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellingMobileApp.Common;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class ListingViewModel
    {
        [Required]
        [Display(Name = "Заглавие")]
        [StringLength(AppConstants.TitleMaxLength, MinimumLength = AppConstants.TitleMinLength, ErrorMessage = "Заглавието трябва да бъде между 10 и 100 символа")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Описание")]
        [StringLength(AppConstants.DescriptionMaxLength, MinimumLength = AppConstants.DescriptionMinLength, ErrorMessage = "Описанието трябва да бъде между 20 и 1000 символа")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Добавете линк към снимка(незадължително)")]
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Цена")]
        [Range((double)AppConstants.PriceMinValue, (double)AppConstants.PriceMaxValue,
        ErrorMessage = "Цената трябва да бъде между 5.00 и 10.000лв")]
        public decimal Price { get; set; }

        [Display(Name = "Дата на публикуване")]
        public DateTime ReleaseDate { get; set; }

        public int PhoneCharacteristicId { get; set; }

        public PhoneModelViewModel PhoneModel { get; set; } = new PhoneModelViewModel();

        public CategoryListingViewModel CategoryListing { get; set; } = new CategoryListingViewModel();

        public ReviewViewModel Reviews { get; set; } = new ReviewViewModel();
        
        public int CategoryId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public virtual IEnumerable<UserViewModel>? Users { get; set; }
    }
}
