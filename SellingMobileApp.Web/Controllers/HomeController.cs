using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellingMobileApp.Web.ViewModels;
using System.Diagnostics;

namespace SellingMobileApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]

        public IActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to selling phones app!";

            

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
