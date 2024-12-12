using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Controllers;
using SellingMobileApp.Web.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobilleApp.Tests.Repositories
{
    public class ManageListingsControllerTests
    {
        /*private readonly Mock<MobilleAppIRepository> _mockRepository;
        private readonly ManageListingsController _controller;

        public ManageListingsControllerTests()
        {
            _mockRepository = new Mock<MobilleAppIRepository>();
            _controller = new ManageListingsController(_mockRepository.Object);
        }

        private string MockUserId => "TestUser";

        [Fact]
        public async Task Delete_Get_ReturnsBadRequestIfListingNotFound()
        {
            int listingId = 1;
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(listingId)).ReturnsAsync((CreateListing)null);
          
            var result = await _controller.Delete(listingId);

           
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_Get_ReturnsUnauthorizedIfUserNotOwner()
        {
            int listingId = 1;
            var listing = new CreateListing { Id = listingId, UserId = "AnotherUser" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(listingId)).ReturnsAsync(listing);

            _controller.ControllerContext.HttpContext = TestHttpContext.CreateWithUser(MockUserId);

            var result = await _controller.Delete(listingId);

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Delete_Get_ReturnsViewWithModelIfAuthorized()
        {
     
            int listingId = 1;
            var listing = new CreateListing { Id = listingId, UserId = MockUserId, Title = "Test Listing" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(listingId)).ReturnsAsync(listing);

            _controller.ControllerContext.HttpContext = TestHttpContext.CreateWithUser(MockUserId);

            var result = await _controller.Delete(listingId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<DeleteListingViewModel>(viewResult.Model);
            Assert.Equal(listingId, model.Id);
            Assert.Equal("Test Listing", model.Title);
        }

        [Fact]
        public async Task Delete_Post_ReturnsBadRequestIfListingNotFound()
        {

            int listingId = 1;
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(listingId)).ReturnsAsync((CreateListing)null);

            var result = await _controller.Delete(listingId, new DeleteListingViewModel());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_Post_ReturnsUnauthorizedIfUserNotOwner()
        {
         
            int listingId = 1;
            var listing = new CreateListing { Id = listingId, UserId = "AnotherUser" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(listingId)).ReturnsAsync(listing);

            _controller.ControllerContext.HttpContext = TestHttpContext.CreateWithUser(MockUserId);

            var result = await _controller.Delete(listingId, new DeleteListingViewModel());

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Delete_Post_RedirectsToAllAfterDeletion()
        {
            
            int listingId = 1;
            var listing = new CreateListing { Id = listingId, UserId = MockUserId };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(listingId)).ReturnsAsync(listing);

            _controller.ControllerContext.HttpContext = TestHttpContext.CreateWithUser(MockUserId);

            var result = await _controller.Delete(listingId, new DeleteListingViewModel());

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectResult.ActionName);
            Assert.Equal("MobilleApp", redirectResult.ControllerName);
        }

        [Fact]
        public async Task Edit_Get_ReturnsBadRequestIfModelNotFound()
        {
            int listingId = 1;
            _mockRepository.Setup(repo => repo.GetListingEditModelAsync(listingId)).ReturnsAsync((EditViewModel)null);

     
            var result = await _controller.Edit(listingId);

          
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewWithModelIfFound()
        {
            
            int listingId = 1;
            var editModel = new EditViewModel { Title = "Test Listing" };
            _mockRepository.Setup(repo => repo.GetListingEditModelAsync(listingId)).ReturnsAsync(editModel);

      
            var result = await _controller.Edit(listingId);

            
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(editModel, viewResult.Model);
        }

        [Fact]
        public async Task Edit_Post_RedirectsToAllAfterSuccessfulEdit()
        {
           
            int listingId = 1;
            var listing = new CreateListing { Id = listingId };
            var editModel = new EditViewModel { Title = "Updated Title" };
            _mockRepository.Setup(repo => repo.GetListingByIdAsync(listingId)).ReturnsAsync(listing);

           
            var result = await _controller.Edit(listingId, editModel);

            
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectResult.ActionName);
            Assert.Equal("MobilleApp", redirectResult.ControllerName);
        }

        private static class TestHttpContext
        {
            public static Microsoft.AspNetCore.Http.HttpContext CreateWithUser(string userId)
            {
                var context = new Microsoft.AspNetCore.Http.DefaultHttpContext();
                context.User = new System.Security.Claims.ClaimsPrincipal(
                    new System.Security.Claims.ClaimsIdentity(
                        new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId) }
                    )
                );
                return context;
            }
        }
    }*/
}

