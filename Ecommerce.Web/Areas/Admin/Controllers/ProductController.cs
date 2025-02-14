using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    public class ProductController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

        public async Task<IActionResult> Index() 
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
    }
}
