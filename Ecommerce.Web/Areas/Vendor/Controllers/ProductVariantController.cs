using AutoMapper;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Web.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Vendor.Controllers
{
    [Area("Vendor")]
    public class ProductVariantController : Controller
    {
        private readonly IProductVariantService _productVariantService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductVariantController> _logger;
        public ProductVariantController(IProductVariantService productVariantService, IMapper mapper, ILogger<ProductVariantController> logger)
        {
            _productVariantService = productVariantService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var productVariants =await _productVariantService.GetProductVariantsAsync();

                _mapper.Map<ProductVariantViewModel>(productVariants);
                return View(productVariants);
            }
            catch (Exception)
            {
                _logger.LogError("");
                throw;
            }
        }
    }
}
