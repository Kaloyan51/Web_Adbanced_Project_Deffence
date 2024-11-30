﻿using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;

namespace SellingMobileApp.Web.Repositories.Contracts
{
    public interface MobilleAppIRepository
    {
        Task AddListingAsync(ListingViewModel listing, string userId);

        Task<ListingViewModel> GetAddModelAsync();

        Task<IEnumerable<AllListingsViewModel>> GetAllListingsAsync();

        //Task<CreateListing> GetListingByIdAsync(int id);

        Task<DetailsViewModel?> GetListingDetailsAsync(int id);

        //Task<EditViewModel?> GetListingEditByIdAsync(Guid id);


    }
}
