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
        public string UserId { get; set; } = string.Empty!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public int ListingId { get; set; }

        [ForeignKey(nameof(ListingId))]
        public CreateListing CreateListing { get; set; } = null!;

        [Range (AppConstants.ReviewRatingMinLength, AppConstants.ReviewRatingMaxLength)]
        [Comment ("Rating of current listing")]
        public int Rating { get; set; }

        [MaxLength (AppConstants.ReviewCommentMaxLenght)]
        [Comment ("Comment of current listing")]
        public string? Comment { get; set; } = string.Empty!;

        [Required]
        public string UserName { get; set; } = null!;
    }
}
