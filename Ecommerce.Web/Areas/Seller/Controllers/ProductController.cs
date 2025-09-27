using AutoMapper;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.Features.ProductFeatures.Commands;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.Features.ProductFeatures.Queries;
using eCommerce.Application.Features.ProductImageFeatures.Dtos;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Web.Areas.Seller.Models;
using eCommerce.Web.ViewModels.ProductVMs;
using FluentValidation;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger, IBrandService brandService, ICategoryService categoryService, IFeatureCategoryService featureCategoryService, IMediator mediator, IFileUploadService fileUploadService) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly IBrandService _brandService = brandService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IFeatureCategoryService _featureCategoryService = featureCategoryService;

        private readonly IMediator _mediator = mediator;

        private readonly ILogger<ProductController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsBySellerQuery());
            var productsVm = _mapper.Map<List<ProductListVM>>(products);

            return View(productsVm);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var products = await _mediator.Send(new GetProductDetailsSellerQuery(id));
            var productsVm = _mapper.Map<ProductDetailsVM>(products);

            return View(productsVm);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new ProductSaveVM
            {
                BrandList = (await _brandService.GetAllBrands())
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

            model.BrandList = (await _brandService.GetAllBrands())
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
                    var fileNames = await fileUploadService.UploadFilesAsync(images, folderPath);

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
            List<FeatureCategoryDTO>? categories = await _featureCategoryService.GetByProductCategoryIdAsync(categoryId);
            return Json(categories);
        }
    }
}
