using AutoMapper;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.Features.FeatureCategoryFeatures.Commands;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Application.Features.FeatureCategoryFeatures.Queries;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Application.Services.AdminServices;
using eCommerce.Application.Services.ProductServices;
using eCommerce.Domain.Entities;
using eCommerce.Web.Areas.Admin.Models.FeatureCategory;
using eCommerce.Web.Areas.Admin.Models.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureCategoryController : Controller
    {
        private readonly IFeatureCategoryService _featureCategoryService;
        private readonly IProductCategoryService _categoryService;
        private readonly IProductFeatureService _featureService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public FeatureCategoryController(IFeatureCategoryService featureCategoryService, IProductCategoryService categoryService, IProductFeatureService featureService, IMapper mapper, IMediator mediator)
        {
            _featureCategoryService = featureCategoryService;
            _categoryService = categoryService;
            _featureService = featureService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var featureCategoriesDto = await _mediator.Send(new GetFeatureCategoriesQuery());
            var featureCategoriesVM = _mapper.Map<List<FeatureCategoryListVm>>(featureCategoriesDto);

            return View(featureCategoriesVM);
        }
             
        [HttpPost]
        public async Task<JsonResult> Create(FeatureCategorySaveVm data)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data." });
            }

            var featureCategorySaveDto = _mapper.Map<FeatureCategorySaveDto>(data);
            var result = await _mediator.Send(new CreateFeatureCategoryCommand(featureCategorySaveDto));

            if (result <= 0)
                return Json(new { success = false, message = "Internal Error." });
            else
                return Json(new { success = true, message = "Created Successfully." });
        }

        public async Task<IActionResult> Details(int FeatureCategoryId)
        {
            var featureCategoryDTO = await _mediator.Send(new GetFeatureCategoryByIdQuery(FeatureCategoryId));
            var featureCategoryDetailsVM = _mapper.Map<FeatureCategoryDetailsVm>(featureCategoryDTO);

            return View(featureCategoryDetailsVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            var featureCategoryDTO = await _featureCategoryService.GetByIdAsync(id);
            if (featureCategoryDTO == null)
                return NotFound();

            var featureCategoryVM = _mapper.Map<FeatureCategoryViewModel>(featureCategoryDTO);
            //await PopulateProductCategoryDropdown();
            return View(featureCategoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FeatureCategoryViewModel data)
        {
            if (!ModelState.IsValid)
            {
                //await PopulateProductCategoryDropdown();
                return View(data);
            }

            var productFeatureDTO = _mapper.Map<FeatureCategoryDTO>(data);
            var result = await _featureCategoryService.UpdateAsync(productFeatureDTO);

            if (!result)
            {
                ModelState.AddModelError("", "Failed to update feature category.");
                //await PopulateProductCategoryDropdown();
                return View(data);
            }

            return RedirectToAction(nameof(Details), new { FeatureCategoryId = data.FeatureCategoryId });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Invalid input." });
            }

            var result = await _mediator.Send(new DeleteFeatureCategoryCommand(id));

            if (!result)
                return Json(new { success = result, message = "Internal Error." });
            else
                return Json(new { success = result, message = "Deleted successfully." });
        }


        [HttpPost]
        public async Task<IActionResult> LinkFeatCatToProdCat(int featCatId, int prodCatId)
        {
            if (featCatId <= 0 || prodCatId <= 0)
            {
                return Json(new { success = false, message = "Invalid id" });
            }
            var isSuccess = await _featureCategoryService.LinkFeatCatToProdCat(featCatId, prodCatId);

            if (!isSuccess)
            {
                return Json(new { success = false, message = "Internal error." });
            }
            return Json(new { success = true, message = "Linked" });
        }

        //[HttpPost]
        //public async Task<IActionResult> UnlinkCategoryFeature(int CategoryId, int FeatureCategoryId)
        //{
        //    if (CategoryId <= 0 || FeatureCategoryId <= 0)
        //    {
        //        return Json(new { success = false, message = "Invalid id" });
        //    }
        //    bool isSuccess = await _featureCategoryService.UnlinkFeatCatProdCat(CategoryId, FeatureCategoryId);
        //    if (!isSuccess)
        //    {
        //        return Json(new { success = false, message = "Internal error." });
        //    }
        //    return Json(new { success = true, message = "Successfully removed." });
        //}

        //private async Task PopulateProductCategoryDropdown(int featureCategoryId = 0)
        //{
        //    var productCategories = featureCategoryId > 0 ? await _categoryService.GetAllAsync() : await _categoryService.GetUnlinkedProductCategories(featureCategoryId);

        //    var dropdownItems = productCategories.Select(x => new SelectListItem
        //    {
        //        Value = x.CategoryId.ToString(),
        //        Text = x.CategoryName
        //    }).ToList();

        //    ViewBag.ProductCategories = dropdownItems;
        //}

        public async Task<IActionResult> GetFeatureList(int id)
        {
            var features = await _featureService.GetByFeatureCategoryIdAsync(id);
            return PartialView("_FeatureListPartial", features);
        }

        //public async Task<IActionResult> GetProductCategoryList(int id)
        //{
        //    var categories = await _categoryService.GetByFeatureCategoryIdAsync(id);
        //    return PartialView("_ProductCategoryList", categories);
        //}
    }
}
