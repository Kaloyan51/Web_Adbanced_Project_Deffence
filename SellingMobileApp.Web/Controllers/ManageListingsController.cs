using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Repositories.Contracts;

namespace SellingMobileApp.Web.Controllers
{
    public class ManageListingsController : BaseController
    {
        private readonly MobilleAppIRepository service;

        public ManageListingsController(MobilleAppIRepository mobileService)
        {
            service = mobileService;
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var listing = await service.GetListingByIdAsync(id);

            if (listing == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (listing.UserId != userId)
            {
                return Unauthorized();
            }

            DeleteListingViewModel model = new DeleteListingViewModel
            {
                Id = listing.Id,
                Title = listing.Title
            };

            return View("~/Views/MobilleApp/Delete.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id, DeleteListingViewModel model)
        {
            var listing = await service.GetListingByIdAsync(id);

            /*if (listing == null)
            {
                return BadRequest();
            }*/

            string userId = GetUserId();

            if (listing.UserId != userId)
            {
                return Unauthorized();
            }

            await service.DeleteGameAsync(listing);

            TempData["Message"] = "Обявата беше успешно изтрита!";
            return View("~/Views/MobilleApp/All.cshtml", model);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await service.GetListingEditModelAsync(id);

            if (model == null)
            {
                return BadRequest();
            }
            //f

            return View("~/Views/MobilleApp/Edit.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                model.CategoryListings = (await service.GetListingEditModelAsync(id)).CategoryListings;
                return View("~/Views/MobilleApp/Edit.cshtml", model);
            }

            var createListing = await service.GetListingByIdAsync(id);
            if (createListing == null)
            {
                return NotFound();
            }

            await service.EditListingAsync(model, createListing);

            TempData["Message"] = "Обявата беше успешно редактирана!";
            return RedirectToAction("All", "Listings");
        }
    }
}
