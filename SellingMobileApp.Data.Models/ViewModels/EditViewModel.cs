using SellingMobileApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class EditViewModel
    {
        [Required]
        [Display(Name = "Заглавие")]
        [StringLength(AppConstants.TitleMaxLength, MinimumLength = AppConstants.TitleMinLength, ErrorMessage = "Заглавието трябва да бъде между 10 и 100 символа")]
        public string Title { get; set; } = string.Empty;

      
        [Required]
        [Display(Name = "Описание")]
        [StringLength(AppConstants.DescriptionMaxLength, MinimumLength = AppConstants.DescriptionMinLength, ErrorMessage = "Описанието трябва да бъде между 20 и 1000 символа")]
        public string Description { get; set; } = string.Empty;


        /*[Display(Name = "Снимка")]
        public string? ImageUrl { get; set; } NE ZNAM DALI SHTE TRQBVA TUK*/

       
        [Required]
        [Display(Name = "Цена")]
        [Range((double)AppConstants.PriceMinValue, (double)AppConstants.PriceMaxValue, ErrorMessage = "Цената трябва да бъде между 5.00 и 10,000лв")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Марка")]
        [StringLength(AppConstants.BrandMaxLength, MinimumLength = AppConstants.BrandMinLength, ErrorMessage = "Марката трябва да бъде между 3 и 50 символа")]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Модел")]
        [StringLength(AppConstants.ModelMaxLength, MinimumLength = AppConstants.ModelMinLength, ErrorMessage = "Моделът трябва да бъде между 3 и 50 символа.")]
        public string Model { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Година на производство")]
        [Range(AppConstants.ManufactureYearMaxLength, AppConstants.ManufactureYearMinLength, ErrorMessage = "Годината на производство трябва да бъде между 2000 и 2100.")]
        public int ManufactureYear { get; set; }

        [Required]
        [Display(Name = "Вътрешна памет (GB)")]
        [Range(AppConstants.StorageCapacityMinLength, AppConstants.StorageCapacityMaxLength, ErrorMessage = "Паметта трябва да бъде между 1 и 1024 GB.")]
        public int StorageCapacity { get; set; }

        [Required]
        [Display(Name = "RAM памет (GB)")]
        [Range(AppConstants.RamCapacityMinLength, AppConstants.RamCapacityMaxLength, ErrorMessage = "RAM паметта трябва да бъде между 1 и 512 GB.")]
        public int RamCapacity { get; set; }
    }
}
