using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;

namespace SellingMobileApp.Web.Repositories.Contracts
{
    public interface MobilleAppIRepository
    {
        Task AddListingAsync(ListingViewModel listing, string userId);

        Task<ListingViewModel> GetAddModelAsync();

        Task<IEnumerable<AllListingsViewModel>> GetAllListingsAsync();

        Task<CreateListing> GetListingByIdAsync(int id);

        Task<DetailsViewModel?> GetListingDetailsAsync(int id);

        Task AddListingToMyFavouriteAsync(string userId, CreateListing createListing);

        Task StrikeOutMyFavouriteAsync(string userId, CreateListing createListing);
        Task<IEnumerable<ListingAddToMyFavouriteViewModel>> AllFavouriteListingAsync(string userId);

        Task DeleteGameAsync(CreateListing createListing);  

        Task<EditViewModel> GetListingEditModelAsync(int id);

        Task EditListingAsync(EditViewModel editListing, CreateListing createListing);

    }
}
