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
    public class Review
    {
        [Key]
        [Comment ("Id of review")]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [Required]
        public int ListingId { get; set; }

        [ForeignKey(nameof(ListingId))]
        public virtual CreateListing CreateListing { get; set; }

        [Range (AppConstants.ReviewMinLength, AppConstants.ReviewMaxength)]
        [Comment ("Rating of current listing")]
        public int Rating { get; set; }
    }
}
