using eCommerce.Infrastructure.Data;
using eCommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger, eCommerceDbContext context) : Controller
    {
        private readonly eCommerceDbContext _context = context;
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            _logger.LogInformation("Inside Index of HomeController");

            var categories = _context.ProductCategories.ToList();
            return View(categories);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
