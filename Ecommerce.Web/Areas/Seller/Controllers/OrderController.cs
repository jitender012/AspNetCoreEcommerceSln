using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
