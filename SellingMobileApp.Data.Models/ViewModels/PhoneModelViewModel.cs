using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellingMobileApp.Common;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class PhoneModelViewModel
    {
        public int Id { get; set; }


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
        [Range(AppConstants.ManufactureYearMinLength, AppConstants.ManufactureYearMaxLength, ErrorMessage = "Годината на производство трябва да бъде между 2000 и 2100.")]
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
