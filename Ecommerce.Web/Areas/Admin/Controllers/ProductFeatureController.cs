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
        public async Task<IActionResult> Details(int ProductFeatureId)
        {
            if (ProductFeatureId <= 0)
                return BadRequest("Invalid Id.");

            var productFeatureDTO = await _productFeatureService.GetByIdAsync(ProductFeatureId);

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

            if (!ModelState.IsValid)
            {
                //For ajax call
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = false, message = "Validation failed" });

                await PopulateDropdown();
                return View(data);
            }

            var productFeatureDTO = _mapper.Map<ProductFeatureDTO>(data);
            var result = await _productFeatureService.AddAsync(productFeatureDTO);

            if (result <= 0)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = false, message = "Create failed" });

                ModelState.AddModelError("", "Failed to create product feature.");
                await PopulateDropdown();
                return View(data);
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = true, message = "Feature added!" });

            return RedirectToAction(actionName: nameof(Index));
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

            return RedirectToAction(nameof(Details),new { ProductFeatureId = data.ProductFeatureId });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return Json(new { success = false, message = "Invalid Id." }); 

            var result =  await _productFeatureService.DeleteAsync(id);
            if (!result)
                return Json(new { success = false, message = "Failed to delete product feature." });

            return Json(new { success = true, message = "Product feature deleted successfully." });
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
