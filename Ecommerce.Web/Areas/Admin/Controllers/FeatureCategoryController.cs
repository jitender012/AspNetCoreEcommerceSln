using AutoMapper;
using eCommerce.Application.DTO;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Application.Services.ProductServices;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Web.Areas.Admin.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureCategoryController(IFeatureCategoryService featureCategoryService, ICategoryService categoryService, IMapper mapper) : Controller
    {
        private readonly IFeatureCategoryService _featureCategoryService = featureCategoryService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var featureCategories = await _featureCategoryService.GetAllAsync();
            List<FeatureCategoryViewModel> featureCategoriesVM = _mapper.Map<List<FeatureCategoryViewModel>>(featureCategories);

            return View(featureCategoriesVM);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateProductCategoryDropdown();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(FeatureCategoryViewModel data)
        {
            await PopulateProductCategoryDropdown();

            if (!ModelState.IsValid)
            {
                return View(data);
            }

            var featureCategoryDTO = _mapper.Map<FeatureCategoryDTO>(data);
            await _featureCategoryService.AddAsync(featureCategoryDTO);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var featureCategory = await _featureCategoryService.GetByIdAsync(id);
            if (featureCategory == null)
            {
                return Json(new { success = false, message = "Feature category not found." });
            }

            await _featureCategoryService.DeleteAsync(id);
            return Json(new { success = true, message = "Feature category deleted successfully." });
        }

        private async Task PopulateProductCategoryDropdown()
        {
            var productCategories = await _categoryService.GetAllAsync();

            var dropdownItems = productCategories.Select(x => new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }).ToList();

            ViewBag.ProductCategories = dropdownItems;
        }
    }
}
