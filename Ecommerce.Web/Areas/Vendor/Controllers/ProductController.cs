using AutoMapper;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Web.Areas.Vendor.Models;
using eCommerce.Web.Models;
using eCommerce.Web.Models.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Vendor.Controllers
{
    [Area("Vendor")]
    [Authorize(Roles = "Seller")]
    public class ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly ILogger<ProductController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsBySeller();
            var productsVm = _mapper.Map<List<SellerProductViewModel>>(products); 
            
            return View(productsVm);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var products = await _productService.GetProductById(id);
            var productsVm = _mapper.Map<ProductVariantViewModel>(products);

            return View(products);
        }
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            return View();
        }
        public async Task<IActionResult> Edit(ProductViewModel productViewModel)
        {
            return View();
        }
    }
}
