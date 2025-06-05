using AutoMapper;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Web.Areas.Vendor.Models;
using eCommerce.Web.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Vendor.Controllers
{
    [Area("Vendor")]   
    public class ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger, IBrandService brandService, ICategoryService categoryService, IFeatureCategoryService featureCategoryService) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly IBrandService _brandService = brandService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IFeatureCategoryService _featureCategoryService = featureCategoryService;

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

        public async Task<IActionResult> Create()
        {
            await PopulateBrandDropdownItems();
            await PopulateCategoryDropdownItems();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SellerCreateProductVM productViewModel)
        {

            await PopulateBrandDropdownItems();
            await PopulateCategoryDropdownItems();
            return View();
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            await PopulateBrandDropdownItems();
            await PopulateCategoryDropdownItems();
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(ProductViewModel productViewModel)
        {
            await PopulateBrandDropdownItems();
            await PopulateCategoryDropdownItems();
            return View();
        }

        private async Task PopulateBrandDropdownItems()
        {
            var brands = await _brandService.GetAllBrands();

            ViewBag.BrandList = brands.Select(x => new SelectListItem
            {
                Value = x.BrandId.ToString(),
                Text = x.BrandName
            }).ToList();
            
        }

        private async Task PopulateCategoryDropdownItems()
        {
            var categories = await _categoryService.GetChildCategoriesAsync();

            var dropdownItems = categories.Select(x => new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }).ToList();

            ViewBag.CategoryList = dropdownItems;
        }

        [HttpGet]
        public async Task<JsonResult> GetFeatureCategoriesWithFeatures(int categoryId)
        {
            var categories = await _featureCategoryService.GetByProductCategoryIdAsync(categoryId);
            return Json(categories);
        }
    }
}
