using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Web.ViewModels;
using System.Diagnostics;

namespace SellingMobileApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        /* public HomeController()
         {

         }*/

        public IActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to selling phones app!";

            /*if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Game");
            }*/

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
