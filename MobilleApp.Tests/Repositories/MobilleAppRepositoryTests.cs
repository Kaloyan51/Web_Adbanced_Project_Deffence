using Microsoft.EntityFrameworkCore;
using Moq;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data;
using SellingMobileApp.Web.Repositories;
using SellingMobileApp.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MobilleApp.Tests.Repositories
{
    public class MobilleAppRepositoryTests
    {
        private readonly MobilleAppRepository _repository;
        private readonly Mock<ApplicationDbContext> _mockContext;

        public MobilleAppRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _mockContext = new Mock<ApplicationDbContext>(options);
            _repository = new MobilleAppRepository(_mockContext.Object);
        }

        // Съществуващи тестове

        [Fact]
        public async Task AddListingAsync_Should_Add_New_Listing()
        {
            var listing = new ListingViewModel
            {
                Title = "Test Listing",
                Description = "Test Description",
                Price = 100.00m,
                ImageUrl = "test_image_url",
                ReleaseDate = DateTime.Now,
                CategoryId = 1,
                PhoneModel = new PhoneModelViewModel
                {
                    Brand = "Test Brand",
                    Model = "Test Model",
                    ManufactureYear = new DateTime(2020, 1, 1),
                    StorageCapacity = 128,
                    RamCapacity = 6
                }
            };

            var userId = "user123";
            await _repository.AddListingAsync(listing, userId);

            var listings = await _mockContext.Object.CreateListings.ToListAsync();
            Assert.Single(listings);
            Assert.Equal("Test Listing", listings[0].Title);
            Assert.Equal(userId, listings[0].UserId);
        }

        [Fact]
        public async Task GetAllListingsAsync_Should_Return_All_Listings()
        {
            var listing1 = new CreateListing
            {
                Title = "Test Listing 1",
                Price = 100.00m,
                ImageUrl = "image_url_1",
                ReleaseDate = DateTime.Now,
                CategoryId = 1,
                UserId = "user123",
                PhoneCharacteristicId = 1
            };

            var listing2 = new CreateListing
            {
                Title = "Test Listing 2",
                Price = 150.00m,
                ImageUrl = "image_url_2",
                ReleaseDate = DateTime.Now,
                CategoryId = 2,
                UserId = "user456",
                PhoneCharacteristicId = 2
            };

            await _mockContext.Object.CreateListings.AddAsync(listing1);
            await _mockContext.Object.CreateListings.AddAsync(listing2);
            await _mockContext.Object.SaveChangesAsync();

            var result = await _repository.GetAllListingsAsync();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetListingDetailsAsync_Should_Return_Details()
        {
            var listing = new CreateListing
            {
                Title = "Test Listing",
                Price = 100.00m,
                ImageUrl = "image_url",
                ReleaseDate = DateTime.Now,
                CategoryId = 1,
                UserId = "user123",
                PhoneCharacteristicId = 1
            };

            await _mockContext.Object.CreateListings.AddAsync(listing);
            await _mockContext.Object.SaveChangesAsync();

            var result = await _repository.GetListingDetailsAsync(listing.Id);
            Assert.NotNull(result);
            Assert.Equal("Test Listing", result.Title);
            Assert.Equal("image_url", result.ImageUrl);
        }

        [Fact]
        public async Task AddListingToMyFavouriteAsync_Should_Add_To_Favourites()
        {
            var userId = "user123";
            var listing = new CreateListing
            {
                Id = 1,
                Title = "Test Listing",
                Price = 100.00m,
                ImageUrl = "image_url",
                ReleaseDate = DateTime.Now,
                CategoryId = 1,
                UserId = userId,
                PhoneCharacteristicId = 1
            };

            await _mockContext.Object.CreateListings.AddAsync(listing);
            await _mockContext.Object.SaveChangesAsync();

            await _repository.AddListingToMyFavouriteAsync(userId, listing);

            var favouriteListing = await _mockContext.Object.UsersCreateListings
                .FirstOrDefaultAsync(ul => ul.UserId == userId && ul.ListingId == listing.Id);
            Assert.NotNull(favouriteListing);
        }

        [Fact]
        public async Task StrikeOutMyFavouriteAsync_Should_Remove_From_Favourites()
        {
            var userId = "user123";
            var listing = new CreateListing
            {
                Id = 1,
                Title = "Test Listing",
                Price = 100.00m,
                ImageUrl = "image_url",
                ReleaseDate = DateTime.Now,
                CategoryId = 1,
                UserId = userId,
                PhoneCharacteristicId = 1
            };

            var userListing = new UserCreateListing
            {
                UserId = userId,
                ListingId = listing.Id
            };

            await _mockContext.Object.UsersCreateListings.AddAsync(userListing);
            await _mockContext.Object.CreateListings.AddAsync(listing);
            await _mockContext.Object.SaveChangesAsync();

            await _repository.StrikeOutMyFavouriteAsync(userId, listing);

            var favouriteListing = await _mockContext.Object.UsersCreateListings
                .FirstOrDefaultAsync(ul => ul.UserId == userId && ul.ListingId == listing.Id);
            Assert.Null(favouriteListing);
        }

        [Fact]
        public async Task AllFavouriteListingAsync_Should_Return_Favourites_For_User()
        {
            var userId = "user123";
            var listing = new CreateListing
            {
                Id = 1,
                Title = "Test Listing",
                Price = 100.00m,
                ImageUrl = "image_url",
                ReleaseDate = DateTime.Now,
                UserId = userId,
                PhoneCharacteristicId = 1
            };

            var userListing = new UserCreateListing
            {
                UserId = userId,
                ListingId = listing.Id
            };

            await _mockContext.Object.CreateListings.AddAsync(listing);
            await _mockContext.Object.UsersCreateListings.AddAsync(userListing);
            await _mockContext.Object.SaveChangesAsync();

            var result = await _repository.AllFavouriteListingAsync(userId);
            Assert.Single(result);
            Assert.Equal("Test Listing", result.First().Title);
        }

        [Fact]
        public async Task DeleteGameAsync_Should_Remove_Listing()
        {
            var listing = new CreateListing
            {
                Id = 1,
                Title = "Test Listing",
                Price = 100.00m,
                ImageUrl = "image_url",
                ReleaseDate = DateTime.Now,
                UserId = "user123",
                PhoneCharacteristicId = 1
            };

            await _mockContext.Object.CreateListings.AddAsync(listing);
            await _mockContext.Object.SaveChangesAsync();

            await _repository.DeleteGameAsync(listing);

            var deletedListing = await _mockContext.Object.CreateListings.FindAsync(listing.Id);
            Assert.Null(deletedListing);
        }

        [Fact]
        public async Task AddReviewAsync_Should_Add_New_Review()
        {
            var review = new ReviewViewModel
            {
                UserId = "user123",
                ListingId = 1,
                Rating = 5,
                Comment = "Great product!",
                UserName = "Test User"
            };

            await _repository.AddReviewAsync(review);

            var reviews = await _mockContext.Object.Reviews.ToListAsync();
            Assert.Single(reviews);
            Assert.Equal("Great product!", reviews.First().Comment);
        }

        [Fact]
        public async Task EditListingAsync_Should_Update_Existing_Listing()
        {
            // Arrange
            var existingListing = new CreateListing
            {
                Id = 1,
                Title = "Old Title",
                Description = "Old Description",
                Price = 100.00m,
                ImageUrl = "old_image_url",
                ReleaseDate = DateTime.Now,
                CategoryId = 1,
                UserId = "user123",
                PhoneCharacteristicId = 1,
                PhoneModel = new PhoneModel
                {
                    Brand = "Old Brand",
                    Model = "Old Model",
                    ManufactureYear = new DateTime(2019, 1, 1),
                    StorageCapacity = 64,
                    RamCapacity = 4
                }
            };

            await _mockContext.Object.CreateListings.AddAsync(existingListing);
            await _mockContext.Object.SaveChangesAsync();

            var editModel = new EditViewModel
            {
                Title = "New Title",
                Description = "New Description",
                Price = 200.00m,
                ImageUrl = "new_image_url",
                CategoryId = 2,
                PhoneModel = new PhoneModelViewModel
                {
                    Brand = "New Brand",
                    Model = "New Model",
                    ManufactureYear = new DateTime(2022, 1, 1),
                    StorageCapacity = 128,
                    RamCapacity = 8
                }
            };

            // Act
            await _repository.EditListingAsync(editModel, existingListing);

            // Assert
            var updatedListing = await _mockContext.Object.CreateListings
                .Include(l => l.PhoneModel)
                .FirstOrDefaultAsync(l => l.Id == existingListing.Id);

            Assert.NotNull(updatedListing);
            Assert.Equal("New Title", updatedListing.Title);
            Assert.Equal("New Description", updatedListing.Description);
            Assert.Equal(200.00m, updatedListing.Price);
            Assert.Equal("new_image_url", updatedListing.ImageUrl);
            Assert.Equal(2, updatedListing.CategoryId);
            Assert.Equal("New Brand", updatedListing.PhoneModel.Brand);
            Assert.Equal("New Model", updatedListing.PhoneModel.Model);
            Assert.Equal(new DateTime(2022, 1, 1), updatedListing.PhoneModel.ManufactureYear);
            Assert.Equal(128, updatedListing.PhoneModel.StorageCapacity);
            Assert.Equal(8, updatedListing.PhoneModel.RamCapacity);
        }
    }
}
