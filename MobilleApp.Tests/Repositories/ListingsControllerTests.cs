using Microsoft.AspNetCore.Mvc;
using Moq;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Controllers;
using SellingMobileApp.Web.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilleApp.Tests.Repositories
{
    public class ListingsControllerTests
    {
        private readonly Mock<MobilleAppIRepository> _mockService;
        private readonly ListingsController _controller;

        public ListingsControllerTests()
        {
            _mockService = new Mock<MobilleAppIRepository>();
            _controller = new ListingsController(_mockService.Object);
        }

        [Fact]
        public async Task All_ReturnsViewWithListings()
        {
            var mockListings = new List<AllListingsViewModel>
        {
            new AllListingsViewModel { Id = 1, Title = "Test Listing 1", Price = 100 },
            new AllListingsViewModel { Id = 2, Title = "Test Listing 2", Price = 200 }
        };

            _mockService.Setup(service => service.GetAllListingsAsync())
                .ReturnsAsync(mockListings);

            var result = await _controller.All();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AllListingsViewModel>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsViewWithListingDetails_WhenListingExists()
        {
            var listingId = 1;
            var mockListing = new DetailsViewModel
            {
                Title = "Test Listing",
                Price = 100
            };

            _mockService.Setup(service => service.GetListingDetailsAsync(listingId))
                .ReturnsAsync(mockListing);

            _mockService.Setup(service => service.GetReviewsByListingIdAsync(listingId))
                .ReturnsAsync(new List<ReviewViewModel>());

            var result = await _controller.Details(listingId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<DetailsViewModel>(viewResult.Model);
            Assert.Equal(mockListing.Title, model.Title);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenListingDoesNotExist()
        {
            var listingId = 1;
            _mockService.Setup(service => service.GetListingDetailsAsync(listingId))
                .ReturnsAsync((DetailsViewModel)null);

            var result = await _controller.Details(listingId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Search_ReturnsViewWithSearchResults_WhenBrandFound()
        {
            var brand = "TestBrand";
            var mockResults = new List<AllListingsViewModel>
        {
            new AllListingsViewModel { Id = 1, Title = "Test Listing 1", Price = 100 },
            new AllListingsViewModel { Id = 2, Title = "Test Listing 2", Price = 200 }
        };

            _mockService.Setup(service => service.SearchByBrandAsync(brand))
                .ReturnsAsync(mockResults);

            var result = await _controller.Search(brand);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AllListingsViewModel>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Search_ReturnsViewWithError_WhenNoResultsFound()
        {
            var brand = "NonExistentBrand";
            _mockService.Setup(service => service.SearchByBrandAsync(brand))
                .ReturnsAsync(new List<AllListingsViewModel>());

            var result = await _controller.Search(brand);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AllListingsViewModel>>(viewResult.Model);
            Assert.Empty(model);
        }
    }
}
