using AutoMapper;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.Features.FeatureCategoryFeatures.Queries;
using eCommerce.Application.Features.MeasurementUnitFeature.Queries;
using eCommerce.Application.Features.ProductConfigurationFeature.Commands;
using eCommerce.Application.Features.ProductConfigurationFeature.DTOs;
using eCommerce.Application.Features.ProductConfigurationFeature.Queries;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Web.Areas.Admin.Models.Product;
using eCommerce.Web.Areas.Admin.Models.ProductFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductFeatureController : Controller
    {
        private readonly IProductFeatureService _productFeatureService;
        private readonly IFeatureCategoryService _featureCategoryService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductFeatureController(IProductFeatureService productFeatureService, IFeatureCategoryService featureCategoryService, IMapper mapper, IMediator mediator)
        {
            _featureCategoryService = featureCategoryService;
            _productFeatureService = productFeatureService;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {

            var featureListDTO = await _mediator.Send(new GetAllFeaturesQuery());
            var featureListVM = _mapper.Map<List<FeatureListVM>>(featureListDTO);
            return View(featureListVM);
        }
        public async Task<IActionResult> Details(int ProductFeatureId)
        {
            if (ProductFeatureId <= 0)
                return BadRequest("Invalid Id.");

            var featureDetailsDTO = await _mediator.Send(new GetFeatureById(ProductFeatureId));

            if (featureDetailsDTO == null)
                return NotFound();
            var productFeatureVM = _mapper.Map<FeatureDetailsVM>(featureDetailsDTO);

            return View(productFeatureVM);
        }

        public async Task<IActionResult> Create()
        {
            var groups = Enum.GetValues(typeof(UnitType))
                .Cast<UnitType>()
                .Select(ut => new SelectListGroup { Name = ut.ToString() })
                .ToDictionary(g => g.Name, g => g);

            FeatureSaveVM featureSaveVM = new FeatureSaveVM()
            {
                FeatureCategoryDropdown = (await _mediator.Send(new GetFeatureCategoriesQuery()))
                .Select(fc => new SelectListItem
                {
                    Value = fc.FeatureCategoryId.ToString(),
                    Text = fc.Name
                }).ToList(),
                MeasurementUnitDropdown = (await _mediator.Send(new GetMeasurementUnitsQuery()))
                .Select(mu => new SelectListItem
                {
                    Value = mu.MeasurementUnitId.ToString(),
                    Text = $"{mu.UnitName} ({mu.UnitSymbol})",
                    Group = groups[mu.UnitType.ToString()]
                }).ToList(),
                InputTypeDropdown = Enum.GetValues(typeof(FeatureInputType))
                    .Cast<FeatureInputType>()
                    .Select(it => new SelectListItem
                    {
                        Value = ((int)it).ToString(),
                        Text = it.ToString()
                    }).ToList(),
                InputType = FeatureInputType.Textbox
            };

            return View(featureSaveVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeatureSaveVM model)
        {

            if (!ModelState.IsValid)
            {
                //For ajax call
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = false, message = "Validation failed" });

                await PopulateDropdown(model);
                return View(model);
            }

            var featureSaveDTO = _mapper.Map<FeatureSaveDTO>(model);
            var result = await _mediator.Send(new CreateFeatureCommand(featureSaveDTO));

            if (result <= 0)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = false, message = "Create failed" });

                ModelState.AddModelError("", "Failed to create product feature.");
                await PopulateDropdown(model);
                return View(model);
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
            //await PopulateDropdown(model);
            return View(productFeatureVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductFeatureViewModel data)
        {
            //await PopulateDropdown();
            if (!ModelState.IsValid)
                return View(data);


            var productFeatureDTO = _mapper.Map<ProductFeatureDTO>(data);
            var result = await _productFeatureService.UpdateAsync(productFeatureDTO);

            if (!result)
            {
                ModelState.AddModelError("", "Failed to update product feature.");
                return View(data);
            }

            return RedirectToAction(nameof(Details), new { ProductFeatureId = data.ProductFeatureId });
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return Json(new { success = false, message = "Invalid Id." });

            var result = await _productFeatureService.DeleteAsync(id);
            if (!result)
                return Json(new { success = false, message = "Failed to delete product feature." });

            return Json(new { success = true, message = "Product feature deleted successfully." });
        }

        private async Task PopulateDropdown(FeatureSaveVM model)
        {
            model.FeatureCategoryDropdown = (await _mediator.Send(new GetFeatureCategoriesQuery()))
        .Select(fc => new SelectListItem
        {
            Value = fc.FeatureCategoryId.ToString(),
            Text = fc.Name
        }).ToList();

            model.MeasurementUnitDropdown = (await _mediator.Send(new GetMeasurementUnitsQuery()))
                .Select(mu => new SelectListItem
                {
                    Value = mu.MeasurementUnitId.ToString(),
                    Text = mu.UnitName
                }).ToList();
        }


    }
}
