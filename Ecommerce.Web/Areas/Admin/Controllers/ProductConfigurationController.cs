using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    public class ProductConfigurationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
