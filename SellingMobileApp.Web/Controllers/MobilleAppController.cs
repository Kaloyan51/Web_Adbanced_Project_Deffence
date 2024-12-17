using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Repositories.Contracts;

namespace SellingMobileApp.Web.Controllers
{
    public class MobilleAppController : BaseController
    {
        private readonly MobilleAppIRepository service;

        public MobilleAppController(MobilleAppIRepository mobileService)
        {
            service = mobileService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ListingViewModel model = await service.GetAddModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ListingViewModel model)
        {
            /*if (!ModelState.IsValid)
            {
                model.CategoryListings = (await service.GetAddModelAsync()).CategoryListings;
                return View(model);
            }*/

            string userId = GetUserId();
            await service.AddListingAsync(model, userId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int listingId, ReviewViewModel reviewModel)
        {

            var listing = await service.GetListingByIdAsync(listingId);
            if (listing == null)
            {
                TempData["Error"] = "Обявата не съществува.";
                return RedirectToAction("All", "Listings"); 
            }

            string userId = GetUserId();
            var currentUser = await service.GetUserByIdAsync(userId);
            if (currentUser == null)
            {
                TempData["Error"] = "Потребителят не е намерен.";
                return RedirectToAction("Login", "Account");
            }

            reviewModel.UserId = userId;
            reviewModel.UserName = currentUser.Name;
            reviewModel.ListingId = listingId;

            await service.AddReviewAsync(reviewModel);

            TempData["Message"] = "Отзивът беше успешно добавен!";
            return RedirectToAction("All", "Listings");
        }

    }
}
