﻿using Microsoft.AspNetCore.Mvc;
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
        private class MockRepository : MobilleAppIRepository
        {
            public List<ListingViewModel> Listings = new();
            public List<CreateListing> CreateListings = new();
            public List<ListingAddToMyFavouriteViewModel> FavouriteListings = new();
            public List<ReviewViewModel> Reviews = new();
            public Dictionary<string, User> Users = new();

            public Task AddListingAsync(ListingViewModel listing, string userId)
            {
                Listings.Add(listing);
                return Task.CompletedTask;
            }

            public Task AddListingToMyFavouriteAsync(string userId, CreateListing createListing)
            {
                FavouriteListings.Add(new ListingAddToMyFavouriteViewModel
                {
                    Id = createListing.Id,
                    Title = createListing.Title
                });
                return Task.CompletedTask;
            }

            public Task<IEnumerable<ListingAddToMyFavouriteViewModel>> AllFavouriteListingAsync(string userId)
            {
                return Task.FromResult((IEnumerable<ListingAddToMyFavouriteViewModel>)FavouriteListings);
            }

            public Task<ListingViewModel> GetAddModelAsync()
            {
                return Task.FromResult(new ListingViewModel { Title = "Mock Model" });
            }

            public Task<IEnumerable<AllListingsViewModel>> GetAllListingsAsync()
            {
                return Task.FromResult<IEnumerable<AllListingsViewModel>>(new List<AllListingsViewModel>());
            }

            public Task<CreateListing> GetListingByIdAsync(int id)
            {
                return Task.FromResult(CreateListings.Find(l => l.Id == id));
            }

            public Task<DetailsViewModel?> GetListingDetailsAsync(int id)
            {
                return Task.FromResult<DetailsViewModel?>(null);
            }

            public Task StrikeOutMyFavouriteAsync(string userId, CreateListing createListing)
            {
                FavouriteListings.RemoveAll(l => l.Id == createListing.Id);
                return Task.CompletedTask;
            }

            public Task DeleteGameAsync(CreateListing createListing)
            {
                CreateListings.Remove(createListing);
                return Task.CompletedTask;
            }

            public Task<EditViewModel> GetListingEditModelAsync(int id)
            {
                return Task.FromResult<EditViewModel>(null);
            }

            public Task EditListingAsync(EditViewModel editListing, CreateListing createListing)
            {
                return Task.CompletedTask;
            }

            public Task AddReviewAsync(ReviewViewModel reviewModel)
            {
                Reviews.Add(reviewModel);
                return Task.CompletedTask;
            }

            public Task<IEnumerable<ReviewViewModel>> GetReviewsByListingIdAsync(int listingId)
            {
                return Task.FromResult((IEnumerable<ReviewViewModel>)Reviews);
            }

            public Task<User?> GetUserByIdAsync(string userId)
            {
                return Task.FromResult(Users.GetValueOrDefault(userId));
            }

            public Task<IEnumerable<AllListingsViewModel>> SearchByBrandAsync(string brand)
            {
                return Task.FromResult<IEnumerable<AllListingsViewModel>>(new List<AllListingsViewModel>());
            }
        }

        private readonly MockRepository _mockRepository;
        private readonly MobilleAppController _controller;

        public MobilleAppControllerTests()
        {
            _mockRepository = new MockRepository();
            _controller = new MobilleAppController(_mockRepository);
        }

        [Fact]
        public async Task Add_Get_ReturnsViewWithModel()
        {
            var result = await _controller.Add();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public async Task Add_Post_RedirectsToHomeIndexOnSuccess()
        {
            var listingModel = new ListingViewModel { Title = "Test Listing" };

            var result = await _controller.Add(listingModel);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Home", redirectResult.ControllerName);
        }

        [Fact]
        public async Task AddToMyFavourite_Post_AddsToFavoritesAndRedirects()
        {
            int listingId = 1;
            _mockRepository.CreateListings.Add(new CreateListing { Id = listingId, Title = "Test" });

            var result = await _controller.AddToMyFavourite(listingId);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyFavourite", redirectResult.ActionName);
            Assert.Equal("MobilleApp", redirectResult.ControllerName);
        }

        [Fact]
        public async Task MyFavourite_ReturnsViewWithFavouriteListings()
        {
            _mockRepository.FavouriteListings.Add(new ListingAddToMyFavouriteViewModel { Id = 1, Title = "Favourite" });

            var result = await _controller.MyFavourite();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotEmpty((IEnumerable<ListingAddToMyFavouriteViewModel>)viewResult.Model);
        }

        [Fact]
        public async Task RemoveFromMyFavourite_RemovesFavoriteAndRedirects()
        {
            int listingId = 1;
            _mockRepository.CreateListings.Add(new CreateListing { Id = listingId });
            _mockRepository.FavouriteListings.Add(new ListingAddToMyFavouriteViewModel { Id = listingId });

            var result = await _controller.RemoveFromMyFavourite(listingId);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyFavourite", redirectResult.ActionName);
            Assert.Equal("MobilleApp", redirectResult.ControllerName);
        }

        [Fact]
        public async Task AddReview_Post_AddsReviewAndRedirects()
        {
            int listingId = 1;
            var reviewModel = new ReviewViewModel { Rating = 5, Comment = "Great!" };
            var userId = "testUser";

            _mockRepository.Users[userId] = new User { Id = userId, UserName = "Test User" };
            _mockRepository.CreateListings.Add(new CreateListing { Id = listingId });

            var result = await _controller.AddReview(listingId, reviewModel);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectResult.ActionName);
            Assert.Equal("MobilleApp", redirectResult.ControllerName);
        }
    }

}