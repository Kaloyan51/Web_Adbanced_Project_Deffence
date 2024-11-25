using Microsoft.EntityFrameworkCore;
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

            var listingData = new CreateListing
            {
                Title = listing.Title,
                Description = listing.Description,
                ImageUrl = listing.ImageUrl,
                OwnerId = listing.OwnerId,
                ReleaseDate = listing.ReleaseDate,
                PhoneCharacteristicId = listing.PhoneCharacteristicId
            };

            await context.CreateListings.AddAsync(listingData);
            await context.SaveChangesAsync();

            //throw new NotImplementedException();
        }

        public async Task<ListingViewModel> GetAddModelAsync()
        {
            var phoneModel = await context.PhoneModels
                .Select(phM => new PhoneModelViewModel
                {
                    Id = phM.Id,
                    Brand = phM.Brand,
                    Model = phM.Model,
                    ManufactureYear = phM.ManufactureYear,
                    StorageCapacity = phM.StorageCapacity,
                    RamCapacity = phM.RamCapacity

                })
                .ToListAsync();

            /*var model = new ListingViewModel
            {
               
            }*/
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
