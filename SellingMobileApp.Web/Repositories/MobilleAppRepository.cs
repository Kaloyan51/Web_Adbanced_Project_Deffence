﻿using Microsoft.EntityFrameworkCore;
using SellingMobileApp.Data;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Repositories;
using SellingMobileApp.Web.Repositories.Contracts;
using System.Globalization;

namespace SellingMobileApp.Web.Repositories
{
    public class MobilleAppRepository : MobilleAppIRepository
    {
        private readonly ApplicationDbContext context;

        public MobilleAppRepository(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        public async Task AddListingAsync(ListingViewModel listing, string userId)
        {
            string dateTimeString = $"{listing.ReleaseDate}";

            if (!DateTime.TryParseExact(dateTimeString, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime parseDateTime))
            {
                throw new InvalidOperationException("Invalid date format.");
            }

            var phoneModel = new PhoneModel
            {
                Brand = listing.PhoneCharacteristic.Brand,
                Model = listing.PhoneCharacteristic.Model,
                ManufactureYear = listing.PhoneCharacteristic.ManufactureYear,
                StorageCapacity = listing.PhoneCharacteristic.StorageCapacity,
                RamCapacity = listing.PhoneCharacteristic.RamCapacity
            };

            // Добавете го в базата данни
            await context.PhoneModels.AddAsync(phoneModel);
            await context.SaveChangesAsync();


            var listingData = new CreateListing
            {
                Title = listing.Title,
                Description = listing.Description,
                ImageUrl = listing.ImageUrl,
                OwnerId = listing.OwnerId,
                ReleaseDate = listing.ReleaseDate,
                PhoneCharacteristicId = phoneModel.Id
            };

            await context.CreateListings.AddAsync(listingData);
            await context.SaveChangesAsync();

            //throw new NotImplementedException();
        }

        public async Task<ListingViewModel> GetAddModelAsync()
        {
            return new ListingViewModel();
            throw new NotImplementedException();
        }

        /*public Task<IEnumerable<AllListingsViewModel>> GetAllListingsByLocationAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DetailsViewModel?> GetListingDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EditViewModel?> GetListingEditByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }*/
    }
}
