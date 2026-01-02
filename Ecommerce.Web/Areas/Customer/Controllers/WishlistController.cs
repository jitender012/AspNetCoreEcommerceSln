using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Customer.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
