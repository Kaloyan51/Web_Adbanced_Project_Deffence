using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Web.Repositories.Contracts;
using SellingMobileApp.Data.Models;
using SellingMobileApp.Data.Models.ViewModels;
using NuGet.Protocol.Core.Types;

namespace SellingMobileApp.Web.Controllers
{
    public class ListingsController : BaseController
    {
        private readonly MobilleAppIRepository service;

        public ListingsController(MobilleAppIRepository mobileService)
        {
            service = mobileService;
        }
        [Route("All")]
        public async Task<IActionResult> All()
        {
            string currentUserId = GetUserId();
            var model = await service.GetAllListingsAsync();
            ViewData["CurrentUserId"] = currentUserId;
            return View("Views/MobilleApp/All.cshtml", model);
        }

        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            string currentUserId = GetUserId();
            ViewData["CurrentUserId"] = currentUserId;

            var model = await service.GetListingDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Reviews = await service.GetReviewsByListingIdAsync(id);

            ViewData["ListingId"] = id;

            return View("~/Views/MobilleApp/Details.cshtml", model);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                TempData["Error"] = "Моля, въведете марка за търсене.";
                return RedirectToAction("All");
            }

            var searchResults = await service.SearchByBrandAsync(brand);

            if (!searchResults.Any())
            {
                TempData["Error"] = "Няма намерени обяви за въведената марка.";
                return RedirectToAction("All");
            }

            ViewData["SearchQuery"] = brand;
            return View("~/Views/MobilleApp/All.cshtml", searchResults);
        }

        [Route("AboutUs")]
        public IActionResult AboutUs()
        {
            return View("~/Views/MobilleApp/AboutUs.cshtml");
        }

        [Route("Contacts")]
        public IActionResult Contact()
        {
            return View("~/Views/MobilleApp/Contacts.cshtml");
        }
    }
}
