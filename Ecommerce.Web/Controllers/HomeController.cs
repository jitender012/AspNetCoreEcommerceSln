using eCommerce.Domain.IdentityEntities;
using eCommerce.Infrastructure.Data;
using eCommerce.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger, eCommerceDbContext context, UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly eCommerceDbContext _context = context;
        private readonly ILogger<HomeController> _logger = logger;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Inside Index of Home Controller");

            if (User.Identity?.IsAuthenticated ?? false)
            {
                var user = await _userManager.GetUserAsync(User);

                if (await _userManager.IsInRoleAsync(user!, "Admin"))
                    return RedirectToAction("Index", "Home", new { area = "Admin" });

                if (await _userManager.IsInRoleAsync(user!, "Seller"))
                    return RedirectToAction("Index", "Home", new { area = "Seller" });

                if (await _userManager.IsInRoleAsync(user!, "Customer"))
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
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
