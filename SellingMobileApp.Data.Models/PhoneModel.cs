using Microsoft.EntityFrameworkCore;
using SellingMobileApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models
{
    public class PhoneModel
    {
        [Key]
        [Comment ("Id of the phoneModel")]
        public int Id { get; set; }

        [Required]
        [MaxLength(AppConstants.BrandMaxLength, ErrorMessage = "Марката трябва да бъде между 3 и 50 символа")]
        public string Brand { get; set; } = null!;

        [Required]
        [Comment ("Model of the phone")]
        [MaxLength (AppConstants.ModelMaxLength, ErrorMessage = "Моделът трябва да бъде между 3 и 50 символа")]
        public string Model { get; set; } = null!;

        [Required]
        [Comment("Manufacture Year of the phone")]
        //  [Range(AppConstants.ManufactureYearMinLength, AppConstants.ManufactureYearMaxLength, ErrorMessage = "Годината на производство трябва да бъде между 2000 и 2100 година")]
        public DateTime ManufactureYear { get; set; }

        [Required]
        [Comment("Storage capacity of the phone in GB")]
        [Range(AppConstants.StorageCapacityMinLength, AppConstants.StorageCapacityMaxLength, ErrorMessage = "Вътрешната памет трябва да бъде между 1 и 1024 GB")]
        public int StorageCapacity { get; set; }

        [Required]
        [Comment ("Ram capacity of the phone in GB")]
        [Range(AppConstants.RamCapacityMinLength, AppConstants.RamCapacityMaxLength, ErrorMessage = "RAM капацитетът трябва да бъде между 1 и 512 GB")]
        public int RamCapacity { get; set; }

        
    }
}
