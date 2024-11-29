using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SellingMobileApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models
{
    public class CreateListing
    {
        [Key]
        [Comment("Identifier for the listing (ad)")]
        public int Id { get; set; }

        [Required]
        [Comment ("Title of the listing")]
        [MaxLength(AppConstants.TitleMaxLength, ErrorMessage = "Заглавието трябва да бъде между 5 и 40 символа")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Comment ("Description detailing the specific phone")]
        [MaxLength(AppConstants.DescriptionMaxLength, ErrorMessage = "Описанието трябва да бъде между 10 и 500 символа")]
        public string Description { get; set; } = string.Empty;

        //[Url(ErrorMessage = "Url адресът е невалиден")]
        public string? ImageUrl { get; set; }

        [Required]
        [Comment ("Price of the phone")]
        public decimal Price { get; set; } //moje da dade problem

        [Comment ("Date of the listing publication")]
        
        public DateTime ReleaseDate { get; set; } = DateTime.Now;


        [Required]
        public string UserId { get; set; } = string.Empty!;

        [ForeignKey(nameof(UserId))]
        public  User User { get; set; } = null!;

        [Required]
        public int PhoneCharacteristicId { get; set; }

        [ForeignKey(nameof(PhoneCharacteristicId))]
        public  PhoneModel PhoneModel { get; set; } = null!;

        public virtual ICollection<UserCreateListing> UsersCreateListings { get; set; } = new List<UserCreateListing>();
    }
}
