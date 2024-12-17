using Microsoft.AspNetCore.Mvc;
using Moq;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Controllers;
using SellingMobileApp.Web.Repositories;
using SellingMobileApp.Web.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilleApp.Tests.Repositories
{
    public class MobilleAppModelsTests
    {
        private readonly Mock<MobilleAppIRepository> _mockRepository;
        private readonly ManageListingsController _manageListingsController;
        private readonly MobilleAppController _mobilleAppController;
        private readonly ListingsController _listingsController;
        private readonly MyFavouritesController _myFavouritesController;
        private readonly ReviewController _reviewController;

        public MobilleAppModelsTests()
        {
            _mockRepository = new Mock<MobilleAppIRepository>();

            _manageListingsController = new ManageListingsController(_mockRepository.Object);
            _mobilleAppController = new MobilleAppController(_mockRepository.Object);
            _listingsController = new ListingsController(_mockRepository.Object);
        }

        #region ManageListingsController Tests
        [Fact]
        public async Task ManageListings_Delete_ShouldReturnBadRequest_WhenListingNotFound()
        {
            int id = 1;
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(id)).ReturnsAsync((CreateListing)null);

            var result = await _manageListingsController.Delete(id);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ManageListings_Delete_ShouldReturnUnauthorized_WhenUserDoesNotOwnListing()
        {
            int id = 1;
            var listing = new CreateListing { UserId = "user2" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(id)).ReturnsAsync(listing);

            var result = await _manageListingsController.Delete(id);

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task ManageListings_Delete_ShouldReturnRedirectToAction_WhenListingDeleted()
        {
            int id = 1;
            var listing = new CreateListing { Id = id, Title = "Test Listing", UserId = "user1" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(id)).ReturnsAsync(listing);
            _mockRepository.Setup(repo => repo.DeleteGameAsync(listing)).Returns(Task.CompletedTask);

            var result = await _manageListingsController.Delete(id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectResult.ActionName);
        }
        #endregion

        #region MobilleAppController Tests
        [Fact]
        public async Task MobilleApp_AddListing_ShouldReturnRedirectToAction_WhenSuccessful()
        {
            var listingViewModel = new ListingViewModel
            {
                Title = "Test Listing",
                Description = "Test Description",
                Price = 100,
                ImageUrl = "http://example.com/image.jpg",
                CategoryId = 1,
                PhoneModel = new PhoneModelViewModel
                {
                    Brand = "Brand",
                    Model = "Model",
                    ManufactureYear = new DateTime(2024, 12, 12),
                    StorageCapacity = 128,
                    RamCapacity = 6
                }
            };

            
            _mockRepository.Setup(repo => repo.AddListingAsync(It.IsAny<ListingViewModel>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            
            var result = await _mobilleAppController.Add(listingViewModel);

           
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task MobilleApp_AddToFavourite_ShouldReturnRedirectToAction_WhenSuccessful()
        {
            int id = 1;
            var listing = new CreateListing { Id = id, Title = "Test Listing" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(id)).ReturnsAsync(listing);
            _mockRepository.Setup(repo => repo.AddListingToMyFavouriteAsync("user1", listing)).Returns(Task.CompletedTask);

            var result = await _myFavouritesController.AddToMyFavourite(id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyFavourite", redirectResult.ActionName);
        }

        [Fact]
        public async Task MobilleApp_RemoveFromFavourite_ShouldReturnRedirectToAction_WhenSuccessful()
        {
            int id = 1;
            var listing = new CreateListing { Id = id, Title = "Test Listing" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(id)).ReturnsAsync(listing);
            _mockRepository.Setup(repo => repo.StrikeOutMyFavouriteAsync("user1", listing)).Returns(Task.CompletedTask);

            var result = await _myFavouritesController.RemoveFromMyFavourite(id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyFavourite", redirectResult.ActionName);
        }

        [Fact]
        public async Task MobilleApp_AddReview_ShouldReturnRedirectToAction_WhenSuccessful()
        {
            var review = new Review
            {
                UserId = "user1",
                ListingId = 1,
                Rating = 5,
                Comment = "Great product!"
            };

            var reviewViewModel = new ReviewViewModel
            {
                UserId = review.UserId,
                ListingId = review.ListingId,
                Rating = review.Rating,
                Comment = review.Comment
            };

            _mockRepository.Setup(repo => repo.AddReviewAsync(It.IsAny<ReviewViewModel>()))
                           .Returns(Task.CompletedTask);

            var result = await _reviewController.AddReview(reviewViewModel.ListingId, reviewViewModel);


            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectToActionResult.ActionName);
        }
        #endregion

        #region ListingsController Tests
        [Fact]
        public async Task Listings_GetAll_ShouldReturnViewWithListings()
        {
            var listings = new List<AllListingsViewModel>
    {
        new AllListingsViewModel { Id = 1, Title = "Test Listing 1", Price = 100 },
        new AllListingsViewModel { Id = 2, Title = "Test Listing 2", Price = 200 }
    };

            _mockRepository.Setup(repo => repo.GetAllListingsAsync()).ReturnsAsync(listings);

            var result = await _listingsController.All();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<AllListingsViewModel>>(viewResult.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Listings_Details_ShouldReturnViewWithListingDetails()
        {
            int id = 1;
            var listing = new CreateListing { Id = id, Title = "Test Listing", Price = 100, Description = "Test Description" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(id)).ReturnsAsync(listing);

            var result = await _listingsController.Details(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CreateListing>(viewResult.Model);
            Assert.Equal("Test Listing", model.Title);
        }
        #endregion
    }
}
