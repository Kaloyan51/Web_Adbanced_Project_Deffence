﻿using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Data.Models.ViewModels;
using SellingMobileApp.Web.Repositories.Contracts;
using System.Diagnostics;

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

            string userId = GetUserId();

            if (listing.UserId != userId)
            {
                return Unauthorized();
            }


            await service.DeleteGameAsync(listing);

            TempData["Message"] = "Обявата беше успешно изтрита!";
            return View("~/Views/MobilleApp/SuccessfullyDelete.cshtml");
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
            var createListing = await service.GetListingByIdAsync(id);
            if (createListing == null)
            {
                var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                return View("Error404", new ErrorViewModel { RequestId = requestId });
            }

           await service.EditListingAsync(model, createListing);

           TempData["Message"] = "Обявата беше успешно редактирана!";
           return View("~/Views/MobilleApp/SuccessfullyEdit.cshtml");
        }
    }
}
