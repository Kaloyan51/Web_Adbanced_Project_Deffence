using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> All()
        {
            string currentUserId = GetUserId();
            var model = await service.GetAllListingsAsync();
            ViewData["CurrentUserId"] = currentUserId;
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await service.GetListingDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToMyFavourite(int id)
        {
            string userId = GetUserId();
            if(string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var createListing = await service.GetListingByIdAsync(id);
            if (createListing != null)
            {
                await service.AddListingToMyFavouriteAsync(userId, createListing);
                TempData["Message"] = "Обявата беше добавена в любимите ви!";
            }

            return RedirectToAction("All", "MobilleApp");
        }

        public async Task<IActionResult> MyFavourite()
        {
            string userId = GetUserId(); 
            if(string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var model = await service.AllFavouriteListingAsync(userId);

            return View(model);
        }
    }
}
