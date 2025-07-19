using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
