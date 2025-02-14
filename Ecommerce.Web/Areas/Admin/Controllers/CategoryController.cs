using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Web.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Employee")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IFileUploadService _fileUploadService;
        public CategoryController(ICategoryService categoryService, IFileUploadService fileUploadService)
        {
            _categoryService = categoryService;
            _fileUploadService = fileUploadService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categoriesDto = await _categoryService.GetAllAsync();
            var parentCategories = await _categoryService.GetMainCategoriesAsync();

            var parentCategoryLookup = parentCategories.ToDictionary(pc=>pc.CategoryId, pc=>pc.CategoryName);

            var categoryViewModels = categoriesDto.Select(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                CategoryImage = x.CategoryImage,
                ParentCategoryId = x.ParentCategoryId,
                ParentCategoryName = x.ParentCategoryId.HasValue && parentCategoryLookup.ContainsKey(x.ParentCategoryId.Value)
                                         ? parentCategoryLookup[x.ParentCategoryId.Value]
                                         : null
            });
            return View(categoryViewModels);
        }

        // GET: Category/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesHierarchicalAsync();
            var dropdownItems = new List<SelectListItem>();
            PopulateMainCategoryDropdown(categories, dropdownItems);
            ViewBag.Categories = dropdownItems;
         
            return View();
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
                    var fileNames = await _fileUploadService.UploadFilesAsync(data.ImageFile, folderPath);
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


        // GET: Category/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Invalid category Id.";
                return View("Error");
            }
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found.";
                return View("Error");
            }
            return View(category);
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

            if (ModelState.IsValid)
            {
                return View(data);
            }

            if (data.ImageFile != null)
            {
                try
                {
                    var folderPath = "Images/CategoryImages";
                    var fileNames = await _fileUploadService.UploadFilesAsync(data.ImageFile, folderPath);
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