using Microsoft.EntityFrameworkCore;
using SellingMobileApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models
{
    public class Category
    {
        [Key]
        [Comment ("Id of the category")]
        public int Id { get; set; }

        [Required]
        [Comment ("Name of the category: new, used or recycled")]
        [MaxLength (AppConstants.NameOfCategoryMaxLength, ErrorMessage = "Името на категорията трябва да бъде между 3 и 50 символа")]
        public string Name { get; set; } = null!;

        public virtual ICollection<CreateListing> Listings { get; set; } = new List<CreateListing>();
    }
}
