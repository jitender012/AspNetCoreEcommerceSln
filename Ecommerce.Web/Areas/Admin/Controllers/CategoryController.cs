using AutoMapper;
using eCommerce.Application.DTO;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Application.Features.FeatureCategoryFeatures.Queries;
using eCommerce.Application.Features.ProductCategoryFeatures.Commands;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using eCommerce.Application.Features.ProductCategoryFeatures.Queries;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Web.Areas.Admin.Models.Category;
using eCommerce.Web.Areas.Admin.Models.ProductCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Employee")]
    public class CategoryController : Controller
    {

        private readonly IProductCategoryService _categoryService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CategoryController(IProductCategoryService categoryService, IFileUploadService fileUploadService, IMediator mediator, IMapper mapper)
        {
            _categoryService = categoryService;
            _fileUploadService = fileUploadService;
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {

            var productCategoryListDto = await _mediator.Send(new GetProductCategoryListQuery());

            var productCategoryListVm = _mapper.Map<List<ProductCategoryListVm>>(productCategoryListDto);
            return View(productCategoryListVm);
        }


        // GET: Category/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int categoryId)
        {
            var model = await _mediator.Send(new GetProductCategoryDetailsQuery(categoryId));            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadFeatureManagement(int categoryId, int featureCategoryId)
        {
            var model = await _mediator.Send(new GetProductCategoryDetailsQuery(categoryId));

             FeatureCategoriesWithLinkStatusDto? featureCategoriesWithLink = model.FeatureCategories
                .Where(fc => fc.FeatureCategoryId == featureCategoryId).FirstOrDefault();

            return PartialView("_FeatureManagementPartial", featureCategoriesWithLink);
        }


        public async Task<JsonResult> GetAllFeaturesWithCategory(int id)
        {
            var featureCategoryListDto = await _mediator.Send(new GetFeatureCategoriesWithLinkStatusQuery(id));
            return Json(new { data = featureCategoryListDto });
        }

        // GET: Category/Create
        public async Task<ActionResult> Create()
        {

            var categories = await _categoryService.GetAllCategoriesHierarchicalAsync();
            var dropdownItems = new List<SelectListItem>();
            PopulateMainCategoryDropdown(categories, dropdownItems);
            ViewBag.Categories = dropdownItems;
            CategoryViewModel categoryViewModel = new CategoryViewModel()
            {
                categoryDTOs = categories
            };
            return View(categoryViewModel);
        }

        // POST: Category/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryViewModel data)
        {
            var categories = await _categoryService.GetAllCategoriesHierarchicalAsync();
            var dropdownItems = new List<SelectListItem>();
            PopulateMainCategoryDropdown(categories, dropdownItems);
            ViewBag.Categories = dropdownItems;
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the highlighted errors and try again.";
                return View(data);
            }

            // Map ViewModel to DTO
            var categoryDTO = new CategoryDTO
            {
                CategoryName = data.CategoryName,
                ParentCategoryId = data.ParentCategoryId,
            };

            // Handle image upload
            if (data.ImageFile != null && data.ImageFile.Any())
            {
                try
                {
                    var folderPath = "Images/CategoryImages";
                    var fileNames = await _fileUploadService.UploadImageAsync(data.ImageFile, folderPath);
                    categoryDTO.CategoryImage = fileNames.FirstOrDefault();
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("CategoryImage", ex.Message);
                    TempData["Error"] = "Invalid file format or size. Please try again.";
                    return View(data);
                }
                catch (Exception)
                {
                    TempData["Error"] = "An unexpected error occurred during file upload. Please try again.";
                    //_logger.LogError(ex, "File upload error in Create method.");
                    return View(data);
                }
            }

            // Save brand to the database
            try
            {
                await _categoryService.AddCategoryAsync(categoryDTO);
                TempData["Success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "An unexpected error occurred while creating the brand. Please try again.";
                //_logger.LogError(ex, "Error in Create method while adding a brand.");
                return View(data);
            }
        }


        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var categories = await _categoryService.GetAllCategoriesHierarchicalAsync();
            var dropdownItems = new List<SelectListItem>();
            PopulateMainCategoryDropdown(categories, dropdownItems);
            ViewBag.Categories = dropdownItems;

            if (id < 1)
            {
                TempData["ErrorMessage"] = "Invalid category Id.";
                return View("Error");
            }

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found with Id " + id + ".";
                return View("Error");
            }
            CategoryViewModel categoryVM = new()
            {
                CategoryId = category.CategoryId,
                CategoryImage = category.CategoryImage,
                CategoryName = category.CategoryName,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryName = category.ParentCategoryName,
                ParentCategoryImage = category.ParentCategoryImage,
            };
            return View(categoryVM);
        }

        // POST: Category/Edit/5      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryViewModel data)
        {
            var categories = await _categoryService.GetAllCategoriesHierarchicalAsync();
            var dropdownItems = new List<SelectListItem>();
            PopulateMainCategoryDropdown(categories, dropdownItems);
            ViewBag.Categories = dropdownItems;

            if (!ModelState.IsValid)
            {
                return View(data);
            }

            if (data.ImageFile != null)
            {
                try
                {
                    var folderPath = "Images/CategoryImages";
                    var fileNames = await _fileUploadService.UploadImageAsync(data.ImageFile, folderPath);
                    data.CategoryImage = fileNames.FirstOrDefault();
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("CategoryImage", ex.Message);
                    TempData["Error"] = "Invalid file format or size. Please try again.";
                    return View(data);
                }
                catch (Exception)
                {
                    TempData["Error"] = "An unexpected error occurred during file upload. Please try again.";
                    //_logger.LogError(ex, "File upload error in Create method.");
                    return View(data);
                }
            }

            UpdateCategoryDto categoryDTO = new UpdateCategoryDto
            {
                CategoryId = data.CategoryId,
                CategoryName = data.CategoryName,
                CategoryImage = data.CategoryImage,
                ParentCategoryId = data.ParentCategoryId,
            };
            var result = await _mediator.Send(new UpdateCategoryCommand(categoryDTO));
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(int categoryId)
        {
            if (categoryId < 1)
            {
                return Json(new { success = false, message = "Invalid brand ID." });
            }
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (category == null)
            {
                return Json(new { success = false, message = "Category not found." });
            }
            try
            {
                await _categoryService.DeleteCategoryAsync(categoryId);
                return Json(new { success = true, message = "Categpry deleted successfully!" });
            }
            catch (Exception)
            {
                //_logger.LogError(ex, "Error occurred while deleting brand with ID: {BrandId}", id);
                return Json(new { success = false, message = "An error occurred while deleting the Category. Please try again." });
            }
        }
        private void PopulateMainCategoryDropdown(List<CategoryDTO> categories, List<SelectListItem> dropdownItems, int level = 0)
        {
            foreach (var category in categories)
            {
                dropdownItems.Add(new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = new string('-', level * 2) + category.CategoryName
                });
                if (category.ChildCategoris?.Any() == true)
                {
                    PopulateMainCategoryDropdown(category.ChildCategoris, dropdownItems, level + 1);
                }
            }
        }

    }
}