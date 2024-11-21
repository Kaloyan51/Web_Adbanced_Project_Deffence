using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models
{
    public class UserCreateListing
    {
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        public int ListingId { get; set; }

        [ForeignKey(nameof(ListingId))]
        public virtual CreateListing CreateListing { get; set; } = null!;
    }
}
