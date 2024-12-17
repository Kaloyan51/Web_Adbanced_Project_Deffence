using Microsoft.AspNetCore.Mvc;
using Moq;
using SellingMobileApp.Web.Controllers;
using SellingMobileApp.Web.Repositories.Contracts;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MobilleApp.Tests.Repositories
{
    public class MyFavouritesControllerTests
    {
        private readonly Mock<MobilleAppIRepository> _mockRepository;
        private readonly MyFavouritesController _controller;

        public MyFavouritesControllerTests()
        {
            _mockRepository = new Mock<MobilleAppIRepository>();
            _controller = new MyFavouritesController(_mockRepository.Object);
        }

        [Fact]
        public async Task AddToMyFavourite_ValidListing_AddsToFavourite()
        {
            var listingId = 1;
            var userId = "user123";
            var listing = new CreateListing { Id = listingId };
            _mockRepository.Setup(r => r.GetListingByIdAsync(listingId)).ReturnsAsync(listing);
            _mockRepository.Setup(r => r.AddListingToMyFavouriteAsync(userId, listing)).Returns(Task.CompletedTask);
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId) })) }
            };

            var result = await _controller.AddToMyFavourite(listingId);

            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("MyFavourite", redirectResult.ActionName);
            Assert.Equal("MyFavourites", redirectResult.ControllerName);
            _mockRepository.Verify(r => r.AddListingToMyFavouriteAsync(userId, listing), Times.Once);
        }

        [Fact]
        public async Task MyFavourite_UserNotLoggedIn_RedirectsToLogin()
        {
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() }
            };

            var result = await _controller.MyFavourite();

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectResult.ActionName);
            Assert.Equal("Account", redirectResult.ControllerName);
        }

        [Fact]
        public async Task RemoveFromMyFavourite_ValidListing_RemovesFromFavourite()
        {
            var listingId = 1;
            var userId = "user123";
            var listing = new CreateListing { Id = listingId };
            _mockRepository.Setup(r => r.GetListingByIdAsync(listingId)).ReturnsAsync(listing);
            _mockRepository.Setup(r => r.StrikeOutMyFavouriteAsync(userId, listing)).Returns(Task.CompletedTask);
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId) })) }
            };
         
            var result = await _controller.RemoveFromMyFavourite(listingId);

            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.Equal("All", redirectResult.ActionName);
            Assert.Equal("Listings", redirectResult.ControllerName);
            _mockRepository.Verify(r => r.StrikeOutMyFavouriteAsync(userId, listing), Times.Once);
        }
    }
}
