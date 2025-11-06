using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {        
        public async Task<IActionResult> Index()
        {
     
            return View();
        }
    }
}