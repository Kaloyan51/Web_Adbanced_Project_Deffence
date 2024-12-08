using Microsoft.AspNetCore.Mvc;
using Moq;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Web.Controllers;
using SellingMobileApp.Web.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MobilleApp.Tests.Repositories
{
    public class MobilleAppControllerTests
    {
        private readonly Mock<MobilleAppIRepository> _mockService;
        private readonly MobilleAppController _controller;

        public MobilleAppControllerTests()
        {
            _mockService = new Mock<MobilleAppIRepository>();
            _controller = new MobilleAppController(_mockService.Object);
        }

        [Fact]
        public async Task Add_Get_Should_Return_View_With_Model()
        {
            // Arrange
            var model = new ListingViewModel();
            _mockService.Setup(s => s.GetAddModelAsync()).ReturnsAsync(model);

            // Act
            var result = await _controller.Add();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
        }

        [Fact]
        public async Task Add_Post_Should_Redirect_To_Home_Index()
        {
            // Arrange
            var model = new ListingViewModel();
            var userId = "user123";
            _controller.ControllerContext = TestHelper.GetControllerContextWithUser(userId);

            // Act
            var result = await _controller.Add(model);

            // Assert
            _mockService.Verify(s => s.AddListingAsync(model, userId), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Home", redirectResult.ControllerName);
        }

        [Fact]
        public async Task AddToMyFavourite_Should_Add_To_Favourites_And_Redirect()
        {
            // Arrange
            var id = 1;
            var userId = "user123";
            var listing = new CreateListing { Id = id };
            _controller.ControllerContext = TestHelper.GetControllerContextWithUser(userId);

            _mockService.Setup(s => s.GetListingByIdAsync(id)).ReturnsAsync(listing);

            // Act
            var result = await _controller.AddToMyFavourite(id);

            // Assert
            _mockService.Verify(s => s.AddListingToMyFavouriteAsync(userId, listing), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyFavourite", redirectResult.ActionName);
            Assert.Equal("MobilleApp", redirectResult.ControllerName);
        }

        [Fact]
        public async Task MyFavourite_Should_Return_View_With_Model()
        {
            // Arrange
            var userId = "user123";
            var favouriteListings = new List<ListingAddToMyFavouriteViewModel>();
            _controller.ControllerContext = TestHelper.GetControllerContextWithUser(userId);

            _mockService.Setup(s => s.AllFavouriteListingAsync(userId)).ReturnsAsync(favouriteListings);

            // Act
            var result = await _controller.MyFavourite();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(favouriteListings, viewResult.Model);
        }

        [Fact]
        public async Task RemoveFromMyFavourite_Should_Remove_And_Redirect()
        {
            // Arrange
            var id = 1;
            var userId = "user123";
            var listing = new CreateListing { Id = id };
            _controller.ControllerContext = TestHelper.GetControllerContextWithUser(userId);

            _mockService.Setup(s => s.GetListingByIdAsync(id)).ReturnsAsync(listing);

            // Act
            var result = await _controller.RemoveFromMyFavourite(id);

            // Assert
            _mockService.Verify(s => s.StrikeOutMyFavouriteAsync(userId, listing), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyFavourite", redirectResult.ActionName);
            Assert.Equal("MobilleApp", redirectResult.ControllerName);
        }

        [Fact]
        public async Task AddReview_Should_Add_Review_And_Redirect()
        {
            // Arrange
            var listingId = 1;
            var review = new ReviewViewModel();
            var userId = "user123";
            var listing = new CreateListing { Id = listingId };
            var user = new User { Id = userId, Name = "Test User" };

            _controller.ControllerContext = TestHelper.GetControllerContextWithUser(userId);

            _mockService.Setup(s => s.GetListingByIdAsync(listingId)).ReturnsAsync(listing);
            _mockService.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _controller.AddReview(listingId, review);

            // Assert
            _mockService.Verify(s => s.AddReviewAsync(It.Is<ReviewViewModel>(r =>
                r.ListingId == listingId && r.UserId == userId && r.UserName == user.Name
            )), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectResult.ActionName);
            Assert.Equal("MobilleApp", redirectResult.ControllerName);
        }
    }
}
