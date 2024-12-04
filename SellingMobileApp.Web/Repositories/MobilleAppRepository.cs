using Microsoft.EntityFrameworkCore;
using SellingMobileApp.Data;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Repositories;
using SellingMobileApp.Web.Repositories.Contracts;
using System.Globalization;
using System.Reflection;
using System.Transactions;

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

            /*if (DateTime.TryParseExact(dateTimeString, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime parseDateTime))
            {
                throw new InvalidOperationException("Invalid date format.");
            }*/

            var phoneModel = new PhoneModel
            {
                Brand = listing.PhoneModel.Brand,
                Model = listing.PhoneModel.Model,
                ManufactureYear = listing.PhoneModel.ManufactureYear,
                StorageCapacity = listing.PhoneModel.StorageCapacity,
                RamCapacity = listing.PhoneModel.RamCapacity
            };

            await context.PhoneModels.AddAsync(phoneModel);
            await context.SaveChangesAsync();


            var listingData = new CreateListing
            {
                Title = listing.Title,
                Description = listing.Description,
                Price = listing.Price,
                ImageUrl = listing.ImageUrl,
                UserId = userId,
                ReleaseDate = DateTime.Now,
                PhoneCharacteristicId = phoneModel.Id,
                CategoryId = listing.CategoryId
            };

            await context.CreateListings.AddAsync(listingData);
            await context.SaveChangesAsync();

        }

        public async Task AddListingToMyFavouriteAsync(string userId, CreateListing createListing)
        {   
            var existingFavourite = await context.UsersCreateListings
                .AnyAsync(ul => ul.UserId == userId && ul.ListingId == createListing.Id);

            if (existingFavourite)
            {
                return;
            }


            var userListing = new UserCreateListing
            {
                UserId = userId,
                ListingId = createListing.Id
            };

            await context.UsersCreateListings.AddAsync(userListing);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ListingAddToMyFavouriteViewModel>> AllFavouriteListingAsync(string userId)
        {
            var favouriteListings = await context.UsersCreateListings
       .Where(ul => ul.UserId == userId)
       .Select(ul => new ListingAddToMyFavouriteViewModel
       {
           Id = ul.CreateListing.Id,
           Title = ul.CreateListing.Title,
           ImageUrl = ul.CreateListing.ImageUrl,
           Price = ul.CreateListing.Price,
           ManufactureYear = ul.CreateListing.PhoneModel.ManufactureYear,
           UserId = ul.CreateListing.UserId,
       })
       .ToListAsync();

            return favouriteListings;
        }

        public async Task<ListingViewModel> GetAddModelAsync()
        {
            var categories = await context.Categories
                .Select(c => new CategoryListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            var model =  new ListingViewModel{
                CategoryListings = categories
            };
            return model;
        }

        public async Task<IEnumerable<AllListingsViewModel>> GetAllListingsAsync()
        {
            var listings = await context.CreateListings
                .Include(l => l.PhoneModel)
        .Select(l => new AllListingsViewModel
        {
            Id = l.Id,
            Title = l.Title,
            ImageUrl = l.ImageUrl,
            Price = l.Price,
            ManufactureYear = l.PhoneModel.ManufactureYear, 
            UserId = l.UserId
        })
        .ToListAsync();

            return listings;
        }

        public async Task<CreateListing> GetListingByIdAsync(int id)
        {
            var listing = await context.CreateListings
       .Include(l => l.PhoneModel)   
       .FirstOrDefaultAsync(l => l.Id == id);

            return listing;
        }

        public async Task<DetailsViewModel?> GetListingDetailsAsync(int id)
        {
            var listing = await context.CreateListings
       .Include(l => l.PhoneModel)
       .Include(l => l.Category)
       .Include(l => l.User)
       .Include(l => l.Reviews)
       .FirstOrDefaultAsync(l => l.Id == id);

            if (listing == null)
            {
                return null;
            }


            var detailsViewModel = new DetailsViewModel
            {
                ImageUrl = listing.ImageUrl,
                Title = listing.Title,
                UserEmail = listing.User.UserEmail,
                UserPhoneNumber = listing.User.UserPhoneNumber,
                Price = listing.Price,
                Description = listing.Description,
                PhoneModel = new PhoneModelViewModel
                {
                    Brand = listing.PhoneModel.Brand,
                    Model = listing.PhoneModel.Model,
                    ManufactureYear = listing.PhoneModel.ManufactureYear,
                    StorageCapacity = listing.PhoneModel.StorageCapacity,
                    RamCapacity = listing.PhoneModel.RamCapacity
                },
                CategoryListing = new CategoryListingViewModel
                {
                    Id = listing.Category.Id,
                    Name = listing.Category.Name
                },
                Reviews = listing.Reviews.Select(r => new ReviewViewModel
                {
                    Id=r.Id,
                    UserId = r.UserId,
                    UserName = r.UserName,
                    Rating = r.Rating,
                    Comment = r.Comment
                }).ToList()
            };

            return detailsViewModel;
        }

        public async Task StrikeOutMyFavouriteAsync(string userId, CreateListing createListing)
        {
            var favourite = await context.UsersCreateListings
       .FirstOrDefaultAsync(ul => ul.UserId == userId && ul.ListingId == createListing.Id);

            if (favourite == null)
            {
                return;
            }

            context.UsersCreateListings.Remove(favourite);

            await context.SaveChangesAsync();
        }

        public Task DeleteGameAsync(CreateListing createListing)
        {
            context.CreateListings.Remove(createListing);
            return context.SaveChangesAsync();
        }

        public async Task<EditViewModel> GetListingEditModelAsync(int id)
        {
            var listing = await context.CreateListings
         .Include(l => l.PhoneModel) 
         .FirstOrDefaultAsync(l => l.Id == id); 

            if (listing == null)
            {
                return null; 
            }

            var model = new EditViewModel
            {
                Title = listing.Title,
                Description = listing.Description,
                Price = listing.Price,
                ImageUrl = listing.ImageUrl,
                ReleaseDate = DateTime.Now,
                CategoryId = listing.CategoryId, 
                PhoneModel = new PhoneModelViewModel
                {
                    Brand = listing.PhoneModel.Brand,
                    Model = listing.PhoneModel.Model,
                    ManufactureYear = listing.PhoneModel.ManufactureYear,
                    StorageCapacity = listing.PhoneModel.StorageCapacity,
                    RamCapacity = listing.PhoneModel.RamCapacity
                },
                CategoryListings = await context.Categories
                    .Select(c => new CategoryListingViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToListAsync() 
            };

            return model; 
        }

        public async Task EditListingAsync(EditViewModel editListing, CreateListing createListing)
        {

            var listing = await context.CreateListings
                .Include(l => l.PhoneModel) 
                .FirstOrDefaultAsync(l => l.Id == createListing.Id); 

            if (listing == null)
            {
                throw new InvalidOperationException("Listing not found."); 
            }

            listing.Title = editListing.Title;
            listing.Description = editListing.Description;
            listing.Price = editListing.Price;
            listing.ImageUrl = editListing.ImageUrl;
            listing.CategoryId = editListing.CategoryId;

            listing.PhoneModel.Brand = editListing.PhoneModel.Brand;
            listing.PhoneModel.Model = editListing.PhoneModel.Model;
            listing.PhoneModel.ManufactureYear = editListing.PhoneModel.ManufactureYear;
            listing.PhoneModel.StorageCapacity = editListing.PhoneModel.StorageCapacity;
            listing.PhoneModel.RamCapacity = editListing.PhoneModel.RamCapacity;

            await context.SaveChangesAsync();
        }

        public async Task AddReviewAsync(ReviewViewModel reviewModel)
        {
            var review = new Review
            {
                UserId = reviewModel.UserId,
                ListingId = reviewModel.ListingId,
                Rating = reviewModel.Rating,
                Comment = reviewModel.Comment,
                UserName = reviewModel.UserName
            };

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewsByListingIdAsync(int listingId)
        {
            return await context.Reviews
                .Where(r => r.ListingId == listingId)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    UserName = r.UserName,
                    ListingId = r.ListingId,
                    Rating = r.Rating,
                    Comment = r.Comment
                })
                .ToListAsync();

        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return context.Users
                .FirstOrDefault(u => u.Id == userId);
        }
    }
}
