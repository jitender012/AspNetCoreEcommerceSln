using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Customer.Controllers
{
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
    }
}
