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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]

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

        public async Task<IActionResult> Details(Guid productId)
        {
            var product = await _mediator.Send(new GetProductDetailsSellerQuery(productId));

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
                CategoryList = (await _categoryService.GetLeafCategoriesAsync())
                .Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.CategoryName
                })
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> GetFeatures(int categoryId)
        {
            var groupedFeatures = await _mediator.Send(new GetByProductCategoryIdQuery(categoryId));


            return PartialView("_FeaturesPartial", groupedFeatures);
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
            model.CategoryList = (await _categoryService.GetLeafCategoriesAsync())
               .Select(x => new SelectListItem
               {
                   Value = x.CategoryId.ToString(),
                   Text = x.CategoryName
               });


            try
            {
                var dto = _mapper.Map<ProductSaveDTO>(model);
                var images = model.ProductVariant.ProductImages;

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
            var categories = await _categoryService.GetLeafCategoriesAsync();

            var dropdownItems = categories.Select(x => new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }).ToList();

            ViewBag.CategoryList = dropdownItems;
        }


    }
}
