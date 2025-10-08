using AutoMapper;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.Features.BrandFeature.Queries;
using eCommerce.Application.Features.ProductFeatures.Commands;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.Features.ProductFeatures.Queries;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Web.Areas.Seller.Models;
using eCommerce.Web.ViewModels.ProductVariantVMs;
using eCommerce.Web.ViewModels.ProductVMs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class ProductController : Controller
    {
        private readonly IProductCategoryService _categoryService;
        private readonly IFeatureCategoryService _featureCategoryService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        public ProductController(
            IProductCategoryService categoryService,
            IFeatureCategoryService featureCategoryService,
            IMapper mapper, ILogger<ProductController> logger,
            IMediator mediator,
             IFileUploadService fileUploadService)
        {            
            _categoryService = categoryService;
            _featureCategoryService = featureCategoryService;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsBySellerQuery());
            var productsVm = _mapper.Map<List<ProductListVM>>(products);

            return View(productsVm);
        }
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var products = await _mediator.Send(new GetProductDetailsSellerQuery(id));
        //    var productsVm = _mapper.Map<ProductDetailsVM>(products);

        //    return View(productsVm);
        //}

        public IActionResult Details()
        {
            var product = new ProductDetailsVM
            {
                ProductId = Guid.NewGuid(),
                ProductName = "SuperPhone X",
                Price = 999.99m,
                Description = "The latest SuperPhone with amazing features.",
                Url = "/products/superphone-x",
                CategoryName = "Smartphones",
                BrandName = "SuperTech",
                ProductVariants = new List<ProductVariantVM>
                {
                    new ProductVariantVM
                    {
                        ProductIvarientId = Guid.NewGuid(),
                        VarientName = "128GB - Black",
                        Quantity = 50,
                        Sku = "SPX-128-BLK",
                        Price = 999.99m,
                        IsActive = true,
                        ImageUrls = new List<string>
                        {
                            "/images/products/superphone-x-black-front.jpg",
                            "/images/products/superphone-x-black-back.jpg"
                        },
                        Features = new List<FeaturesVM>
                        {
                            new FeaturesVM { ProductFeaturesId = 1, Name = "Storage", Value = "128GB" },
                            new FeaturesVM { ProductFeaturesId = 2, Name = "Color", Value = "Black" },
                            new FeaturesVM { ProductFeaturesId = 3, Name = "Battery", Value = "4000mAh" }
                        }
                    },
                    new ProductVariantVM
                    {
                        ProductIvarientId = Guid.NewGuid(),
                        VarientName = "256GB - Silver",
                        Quantity = 30,
                        Sku = "SPX-256-SLV",
                        Price = 1199.99m,
                        IsActive = true,
                        ImageUrls = new List<string>
                        {
                            "/images/products/superphone-x-silver-front.jpg",
                            "/images/products/superphone-x-silver-back.jpg"
                        },
                        Features = new List<FeaturesVM>
                        {
                            new FeaturesVM { ProductFeaturesId = 4, Name = "Storage", Value = "256GB" },
                            new FeaturesVM { ProductFeaturesId = 5, Name = "Color", Value = "Silver" },
                            new FeaturesVM { ProductFeaturesId = 6, Name = "Battery", Value = "4000mAh" }
                        }
                    }
                }
            };

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new ProductSaveVM
            {
                BrandList = (await _mediator.Send(new GetAllBrandsQuery()))
                .Select(x => new SelectListItem
                {
                    Value = x.BrandId.ToString(),
                    Text = x.BrandName
                }),
                CategoryList = (await _categoryService.GetChildCategoriesAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.CategoryName
                })
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductSaveVM model, CancellationToken cancellationToken)
        {

            model.BrandList = (await _mediator.Send(new GetAllBrandsQuery()))
                  .Select(x => new SelectListItem
                  {
                      Value = x.BrandId.ToString(),
                      Text = x.BrandName
                  });
            model.CategoryList = (await _categoryService.GetChildCategoriesAsync())
               .Select(x => new SelectListItem
               {
                   Value = x.CategoryId.ToString(),
                   Text = x.CategoryName
               });

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = _mapper.Map<ProductSaveDTO>(model);
                var images = model.ProductVariant.ProuctImages;

                if (images != null && images.Count > 0)
                {
                    var folderPath = "Images/ProductImages";
                    var fileNames = await _fileUploadService.UploadImageAsync(images, folderPath);

                    dto.ProductVariant.ImageUrls = fileNames;
                }
                var id = await _mediator.Send(new CreateProductCommand(dto));
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    var propertyName = error.PropertyName.Replace("dto.", "");
                    ModelState.AddModelError(propertyName, error.ErrorMessage);
                }
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            await PopulateBrandDropdownItems();
            await PopulateCategoryDropdownItems();
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(SellerEditProductViewModel productViewModel)
        {
            await PopulateBrandDropdownItems();
            await PopulateCategoryDropdownItems();
            return View();
        }

        private async Task PopulateBrandDropdownItems()
        {
            var brands = await _mediator.Send(new GetAllBrandsQuery());

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

        //[HttpGet]
        //public async Task<JsonResult> GetFeatureCategoriesWithFeatures(int categoryId)
        //{
        //    List<FeatureCategoryDTO>? categories = await _featureCategoryService.GetByProductCategoryIdAsync(categoryId);
        //    return Json(categories);
        //}
    }
}
