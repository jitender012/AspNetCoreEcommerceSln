using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Vendor.Controllers
{
    [Area("Vendor")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
