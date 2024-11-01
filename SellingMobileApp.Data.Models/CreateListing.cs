using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public string Title { get; set; } = string.Empty;

        [Required]
        [Comment ("Description detailing the specific phone")]
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        [Comment ("Price of the phone")]
        public decimal? Price { get; set; } //moje da dade problem

        [Comment ("Date of the listing publication")]
        public DateTime ReleaseDate { get; set; }

        public CreateListing()
        {
            ReleaseDate = DateTime.Now;
        }

        [Required]
        public int OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; } = null!;
    }
}
