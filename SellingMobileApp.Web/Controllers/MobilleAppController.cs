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
           

            string userId = GetUserId();
            await service.AddListingAsync(model, userId);
            return View("~/Views/MobilleApp/SuccessfullyCreate.cshtml");
        }

       
    }
}
