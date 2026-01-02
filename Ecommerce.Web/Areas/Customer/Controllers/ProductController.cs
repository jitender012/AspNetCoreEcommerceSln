using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Customer.Controllers
{
    [Area("customer")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Search()
        {

            return View();
        }
    }
}
