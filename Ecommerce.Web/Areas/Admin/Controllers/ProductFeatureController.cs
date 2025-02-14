using AutoMapper;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Web.Areas.Admin.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductFeatureController(IProductFeatureService productFeatureService, IFeatureCategoryService featureCategoryService, IMapper mapper) : Controller
    {
        private readonly IProductFeatureService _productFeatureService = productFeatureService;
        private readonly IFeatureCategoryService _featureCategoryService = featureCategoryService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var productFeatureDTO = await _productFeatureService.GetAllAsync();
            var productFeatureVM = _mapper.Map<List<ProductFeatureViewModel>>(productFeatureDTO);

            return View(productFeatureVM);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id.");

            var productFeatureDTO = await _productFeatureService.GetByIdAsync(id);

            if (productFeatureDTO == null)
                return NotFound();

            var productFeatureVM = _mapper.Map<ProductFeatureViewModel>(productFeatureDTO);
            return View(productFeatureVM);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductFeatureViewModel data)
        {
            await PopulateDropdown();
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            var productFeatureDTO = _mapper.Map<ProductFeatureDTO>(data);
            var result = await _productFeatureService.AddAsync(productFeatureDTO);
            if (result <= 0)
            {
                ModelState.AddModelError("", "Failed to create product feature.");
                return View(data);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            var productFeatureDTO = await _productFeatureService.GetByIdAsync(id);
            if (productFeatureDTO == null)
                return NotFound();

            var productFeatureVM = _mapper.Map<ProductFeatureViewModel>(productFeatureDTO);
            await PopulateDropdown();
            return View(productFeatureVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductFeatureViewModel data)
        {
            await PopulateDropdown();
            if (!ModelState.IsValid)
                return View(data);
            
            
            var productFeatureDTO = _mapper.Map<ProductFeatureDTO>(data);
            var result = await _productFeatureService.UpdateAsync(productFeatureDTO);

            if (!result)
            {
                ModelState.AddModelError("", "Failed to update product feature.");
                return View(data);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            var result =  await _productFeatureService.DeleteAsync(id);
            if (!result)
                ModelState.AddModelError("", "Failed to delete product feature.");

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdown()
        {
            var featureCategories = await _featureCategoryService.GetAllAsync();

            var dropdownItems = featureCategories.Select(x => new SelectListItem
            {
                Value = x.FeatureCategoryId.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.FeatureCategories = dropdownItems;
        }

    }
}
