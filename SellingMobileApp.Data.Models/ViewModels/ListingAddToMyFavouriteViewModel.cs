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

        public List<AllListingsViewModel> FavouriteListings { get; set; } = new List<AllListingsViewModel>();
    }
}
