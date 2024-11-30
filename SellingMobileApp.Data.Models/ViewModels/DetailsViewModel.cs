using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellingMobileApp.Data.Models.ViewModels
{
    public class DetailsViewModel
    {
        public string? ImageUrl { get; set; }

        public string Title { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string UserPhoneNumber { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        

        public PhoneModelViewModel PhoneModel { get; set; } = new PhoneModelViewModel();

        public CategoryListingViewModel CategoryListing { get; set; } = new CategoryListingViewModel();
    }
}
