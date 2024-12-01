using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class ListingAddToMyFavouriteViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public DateTime ManufactureYear { get; set; }

        public string UserId { get; set; } = string.Empty;

        //public string UserName { get; set; } = string.Empty;
    }
}
