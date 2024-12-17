using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Repositories.Contracts;

namespace SellingMobileApp.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly MobilleAppIRepository service;

        public ReviewController(MobilleAppIRepository mobileService)
        {
            service = mobileService;
        }

        protected string GetUserId()
        {
            return User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
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
