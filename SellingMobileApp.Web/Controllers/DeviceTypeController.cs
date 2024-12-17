using Microsoft.AspNetCore.Mvc;

namespace SellingMobileApp.Web.Controllers
{
    public class DeviceTypeController : BaseController
    {
        [HttpGet]
        [Route("Phones")]
        public IActionResult Phones()
        {
            return View("~/Views/MobilleApp/Phones.cshtml");
        }

        [HttpGet]
        [Route("Tablets")]
        public IActionResult Tablets()
        {
            return View("~/Views/MobilleApp/Tablets.cshtml");
        }

        [HttpGet]
        [Route("Accessories")]
        public IActionResult Accessories()
        {
            return View("~/Views/MobilleApp/Accessories.cshtml");
        }
    }
}
