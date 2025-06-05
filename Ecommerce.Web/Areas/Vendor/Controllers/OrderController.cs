using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Vendor.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
