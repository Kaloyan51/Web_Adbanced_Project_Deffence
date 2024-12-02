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

            // Добавете го в базата данни
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
       .FirstOrDefaultAsync(l => l.Id == id);


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
                }
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
         .Include(l => l.PhoneModel) // Зареждаме информацията за телефона
         .FirstOrDefaultAsync(l => l.Id == id); // Извличаме съществуващата обява по id

            if (listing == null)
            {
                return null; // Ако не е намерена обява с това id, връщаме null
            }

            // Подготвяме модел за редактиране с данните от обявата
            var model = new EditViewModel
            {
                Title = listing.Title,
                Description = listing.Description,
                Price = listing.Price,
                ImageUrl = listing.ImageUrl,
                ReleaseDate = DateTime.Now,
                CategoryId = listing.CategoryId, // ID на категорията
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
                    .ToListAsync() // Вземаме всички категории за падащото меню
            };

            return model; // Връщаме подготвения модел за редактиране
        }

        public async Task EditListingAsync(EditViewModel editListing, CreateListing createListing)
        {
            // Проверяваме дали обявата съществува в базата данни
            var listing = await context.CreateListings
                .Include(l => l.PhoneModel) // Зареждаме информацията за телефона
                .FirstOrDefaultAsync(l => l.Id == createListing.Id); // Търсим обявата по id

            if (listing == null)
            {
                throw new InvalidOperationException("Listing not found."); // Ако не я намерим, хвърляме грешка
            }

            // Актуализираме стойностите на обявата
            listing.Title = editListing.Title;
            listing.Description = editListing.Description;
            listing.Price = editListing.Price;
            listing.ImageUrl = editListing.ImageUrl;
            listing.CategoryId = editListing.CategoryId;

            // Няма нужда да променяме ReleaseDate, защото то е фиксирано към текущата дата

            // Актуализираме информацията за телефона
            listing.PhoneModel.Brand = editListing.PhoneModel.Brand;
            listing.PhoneModel.Model = editListing.PhoneModel.Model;
            listing.PhoneModel.ManufactureYear = editListing.PhoneModel.ManufactureYear;
            listing.PhoneModel.StorageCapacity = editListing.PhoneModel.StorageCapacity;
            listing.PhoneModel.RamCapacity = editListing.PhoneModel.RamCapacity;

            // Записваме промените в базата данни
            await context.SaveChangesAsync();
        }
    }
}
