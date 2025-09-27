using AutoMapper;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Web.ViewModels.ProductVariantVMs;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
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

                var productVariantsVm = _mapper.Map<ProductVariantSaveVM>(productVariants);
                return View(productVariantsVm);
            }
            catch (Exception)
            {
                _logger.LogError("");
                throw;
            }
        }
    }
}
