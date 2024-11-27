using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;

namespace SellingMobileApp.Web.Repositories.Contracts
{
    public interface MobilleAppIRepository
    {
        Task AddListingAsync(ListingViewModel listing, string userId);

        Task<ListingViewModel> GetAddModelAsync();

        Task<IEnumerable<AllListingsViewModel>> GetAllListingsAsync();

        /*Task<T> GetByIdAsync(int id);

        Task<DetailsViewModel?> GetListingDetailsByIdAsync(Guid id);

        Task<EditViewModel?> GetListingEditByIdAsync(Guid id);*/


    }
}
