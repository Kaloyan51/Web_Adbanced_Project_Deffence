using Microsoft.AspNetCore.Mvc;
using Moq;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Web.Controllers;
using SellingMobileApp.Web.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MobilleApp.Tests.Repositories
{
    public class ReviewControllerTests
    {
        private readonly Mock<MobilleAppIRepository> _serviceMock;
        private readonly ReviewController _controller;

        public ReviewControllerTests()
        {
            _serviceMock = new Mock<MobilleAppIRepository>();
            _controller = new ReviewController(_serviceMock.Object);
        }

        [Fact]
        public async Task AddReview_ListinNotFound_RedirectsToAllListings()
        {
            int listingId = 1;
            var reviewModel = new ReviewViewModel();
            _serviceMock.Setup(s => s.GetListingByIdAsync(listingId)).ReturnsAsync((CreateListing)null); // Обявата не съществува

            var result = await _controller.AddReview(listingId, reviewModel);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectResult.ActionName);
            Assert.Equal("Listings", redirectResult.ControllerName);
            Assert.Equal("Обявата не съществува.", _controller.TempData["Error"]);
        }

        [Fact]
        public async Task AddReview_UserNotFound_RedirectsToLogin()
        {
            int listingId = 1;
            var reviewModel = new ReviewViewModel();
            var listing = new CreateListing { Id = listingId };
            _serviceMock.Setup(s => s.GetListingByIdAsync(listingId)).ReturnsAsync(listing); // Обявата съществува
            _serviceMock.Setup(s => s.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null); // Потребителят не съществува

            var result = await _controller.AddReview(listingId, reviewModel);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectResult.ActionName);
            Assert.Equal("Account", redirectResult.ControllerName);
            Assert.Equal("Потребителят не е намерен.", _controller.TempData["Error"]);
        }

        [Fact]
        public async Task AddReview_SuccessfullyAddsReview_RedirectsToAllListings()
        {
            int listingId = 1;
            var reviewModel = new ReviewViewModel { UserId = "user123", UserName = "Test User", ListingId = listingId };
            var listing = new CreateListing { Id = listingId };
            var user = new User { Id = "user123", Name = "Test User" };

            _serviceMock.Setup(s => s.GetListingByIdAsync(listingId)).ReturnsAsync(listing); // Обявата съществува
            _serviceMock.Setup(s => s.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(user); // Потребителят съществува
            _serviceMock.Setup(s => s.AddReviewAsync(It.IsAny<ReviewViewModel>())).Returns(Task.CompletedTask); // Мокваме добавянето на отзив

            var result = await _controller.AddReview(listingId, reviewModel);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectResult.ActionName);
            Assert.Equal("Listings", redirectResult.ControllerName);
            Assert.Equal("Отзивът беше успешно добавен!", _controller.TempData["Message"]);
        }

        [Fact]
        public void GetUserId_ReturnsCorrectUserId()
        {
            // Arrange
            var controller = new ReviewController(_serviceMock.Object);
            var userId = "user123";
            var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(
                new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId)
                })
            );
            controller.ControllerContext.HttpContext.User = claimsPrincipal;


            var methodInfo = typeof(ReviewController).GetMethod("GetUserId", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = methodInfo.Invoke(controller, null);

            Assert.Equal(userId, result);
        }
    }
}
