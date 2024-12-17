using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Data;
using SellingMobileApp.Data.Models.ViewModels;
using System.Security.Claims;

namespace SellingMobileApp.Web.Controllers
{
    public class DeviceTypeController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public DeviceTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Phones")]
        public IActionResult Phones()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CurrentUserId"] = currentUserId;
            var phones = _context.CreateListings
                .Where(l => l.DeviceType.Type == "Телефон")  // Използвай връзката към DeviceType вместо низ
                .Select(l => new TypePhonesViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    ImageUrl = l.ImageUrl,
                    Price = l.Price,
                    ManufactureYear = l.ReleaseDate,  // Може да бъде "ReleaseDate" вместо "ManufactureYear"
                    UserId = l.UserId,
                    UserName = l.User.UserName,  // Получаваш UserName от User обекта
                    DeviceType = l.DeviceType.Type  // Използвай Type от DeviceType, а не директно низ
                })
                .ToList();

            return View("~/Views/MobilleApp/Phones.cshtml", phones);
        }

        [HttpGet]
        [Route("Tablets")]
        public IActionResult Tablets()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CurrentUserId"] = currentUserId;

            var tablets = _context.CreateListings
                .Where(l => l.DeviceType.Type == "Таблет")  // Използвай връзката към DeviceType
                .Select(l => new TypeTabletsViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    ImageUrl = l.ImageUrl,
                    Price = l.Price,
                    ManufactureYear = l.ReleaseDate,  // Може да бъде "ReleaseDate" вместо "ManufactureYear"
                    UserId = l.UserId,
                    UserName = l.User.UserName,  // Получаваш UserName от User обекта
                    DeviceType = l.DeviceType.Type  // Използвай Type от DeviceType
                })
                .ToList();

            return View("~/Views/MobilleApp/Tablets.cshtml", tablets);
        }

        [HttpGet]
        [Route("Accessories")]
        public IActionResult Accessories()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CurrentUserId"] = currentUserId;

            var accessories = _context.CreateListings
                .Where(l => l.DeviceType.Type == "Аксесоари")  // Използвай връзката към DeviceType
                .Select(l => new TypeAccessoriesViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    ImageUrl = l.ImageUrl,
                    Price = l.Price,
                    ManufactureYear = l.ReleaseDate,  // Може да бъде "ReleaseDate" вместо "ManufactureYear"
                    UserId = l.UserId,
                    UserName = l.User.UserName,  // Получаваш UserName от User обекта
                    DeviceType = l.DeviceType.Type  // Използвай Type от DeviceType
                })
                .ToList();

            return View("~/Views/MobilleApp/Accessories.cshtml", accessories);  // Връщаш списък с обяви за аксесоари
        }
    }
}
