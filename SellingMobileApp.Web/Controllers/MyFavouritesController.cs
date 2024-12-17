using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Repositories.Contracts;

namespace SellingMobileApp.Web.Controllers
{
    public class MyFavouritesController : Controller
    {
        private readonly MobilleAppIRepository service;

        public MyFavouritesController(MobilleAppIRepository service)
        {
            this.service = service;
        }

        private string GetUserId()
        {
            return User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpPost]
        [Route("AddToMyFavourite")]
        public async Task<IActionResult> AddToMyFavourite(int id)
        {
            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var createListing = await service.GetListingByIdAsync(id);
            if (createListing != null)
            {
                await service.AddListingToMyFavouriteAsync(userId, createListing);
                TempData["Message"] = "Обявата беше добавена в любимите ви!";
            }

            return RedirectToAction("MyFavourite", "MyFavourites");
        }


        public async Task<IActionResult> MyFavourite()
        {
            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var model = await service.AllFavouriteListingAsync(userId)
                 ?? new List<ListingAddToMyFavouriteViewModel>();

            return View("~/Views/MobilleApp/MyFavourite.cshtml", model);
        }

        [HttpPost]
        [Route("RemoveFromMyFavourite")]
        public async Task<IActionResult> RemoveFromMyFavourite(int id)
        {
            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var createListing = await service.GetListingByIdAsync(id);
            if (createListing != null)
            {
                await service.StrikeOutMyFavouriteAsync(userId, createListing);
                TempData["Message"] = "Обявата беше премахната от любимите ви!";
            }

            return RedirectToAction("All", "Listings");
        }
    }
}

