using Microsoft.AspNetCore.Identity;
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
    public class User : IdentityUser
    {
        [Key]
        [Comment ("Id of the user")]
        public  string Id { get; set; }

        [Required]
        [Comment ("Name of the user")]
        public string Name { get; set; } = null!;

        [Required]
        [Comment ("Email of the user")]
        [EmailAddress]
        [MaxLength (AppConstants.EmailMaxLength, ErrorMessage = "Имейлът не може да бъде повече от 256 символа")]
        public string Email { get; set; } = null!;

        [Comment ("List of the users listings")]
        public virtual ICollection<CreateListing> Listings { get; set; } = new List<CreateListing>();
    }
}
