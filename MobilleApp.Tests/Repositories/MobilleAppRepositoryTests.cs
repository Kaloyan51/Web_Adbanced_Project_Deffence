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
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly MobilleAppRepository _repository;

        public MobilleAppRepositoryTests()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _repository = new MobilleAppRepository(_mockContext.Object);
        }

        [Fact]
        public async Task AddListingAsync_AddsListingToDatabase()
        {
            // Arrange
            var listing = new ListingViewModel
            {
                Title = "Test Listing",
                Description = "Description",
                Price = 100,
                ImageUrl = "http://example.com/image.jpg",
                PhoneModel = new PhoneModelViewModel { Brand = "Brand", Model = "Model", ManufactureYear = new DateTime(2020, 1, 1), StorageCapacity = 64, RamCapacity = 4 },
                ReleaseDate = new DateTime(2024, 12, 12),
                CategoryId = 1
            };

            var userId = "userId123";

            _mockContext.Setup(ctx => ctx.PhoneModels.AddAsync(It.IsAny<PhoneModel>(), default));
            _mockContext.Setup(ctx => ctx.CreateListings.AddAsync(It.IsAny<CreateListing>(), default));

            // Act
            await _repository.AddListingAsync(listing, userId);

            // Assert
            _mockContext.Verify(ctx => ctx.PhoneModels.AddAsync(It.IsAny<PhoneModel>(), default), Times.Once);
            _mockContext.Verify(ctx => ctx.CreateListings.AddAsync(It.IsAny<CreateListing>(), default), Times.Once);
        }

        [Fact]
        public async Task GetAllListingsAsync_ReturnsAllListings()
        {
            // Arrange
            var listings = new List<CreateListing>
        {
            new CreateListing { Id = 1, Title = "Listing 1", Price = 100, PhoneModel = new PhoneModel { ManufactureYear = new DateTime(2020, 1, 1) }, ImageUrl = "image1.jpg", UserId = "user1" },
            new CreateListing { Id = 2, Title = "Listing 2", Price = 200, PhoneModel = new PhoneModel { ManufactureYear = new DateTime(2021, 1, 1) }, ImageUrl = "image2.jpg", UserId = "user2" }
        };

            var dbSetMock = MockDbSet(listings.AsQueryable());

            _mockContext.Setup(ctx => ctx.CreateListings).Returns(dbSetMock.Object);

            // Act
            var result = await _repository.GetAllListingsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddListingToMyFavouriteAsync_AddsFavourite()
        {
            // Arrange
            var userId = "userId123";
            var createListing = new CreateListing { Id = 1, Title = "Favourite Listing" };

            var dbSetMock = MockDbSet(new List<UserCreateListing>().AsQueryable());
            _mockContext.Setup(ctx => ctx.UsersCreateListings).Returns(dbSetMock.Object);

            // Act
            await _repository.AddListingToMyFavouriteAsync(userId, createListing);

            // Assert
            _mockContext.Verify(ctx => ctx.UsersCreateListings.AddAsync(It.Is<UserCreateListing>(ul => ul.UserId == userId && ul.ListingId == createListing.Id), default), Times.Once);
            _mockContext.Verify(ctx => ctx.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetListingByIdAsync_ReturnsListingWithId()
        {
            // Arrange
            var listings = new List<CreateListing>
        {
            new CreateListing { Id = 1, Title = "Listing 1" },
            new CreateListing { Id = 2, Title = "Listing 2" }
        };

            var dbSetMock = MockDbSet(listings.AsQueryable());
            _mockContext.Setup(ctx => ctx.CreateListings).Returns(dbSetMock.Object);

            // Act
            var result = await _repository.GetListingByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        private Mock<DbSet<T>> MockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }

}
