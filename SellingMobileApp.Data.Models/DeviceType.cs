using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models
{
    public class DeviceType
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public string Type { get; set; } = null!;

        public virtual ICollection<CreateListing> Listings { get; set; } = new List<CreateListing>();

    }
}
