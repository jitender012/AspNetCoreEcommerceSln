using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        // GET: Administration/Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult GetData()
        {
            // Simulating data retrieval
            var data = "Hello, this is some dynamic data!";
            return PartialView("_Partial", data);
        }
    }
}