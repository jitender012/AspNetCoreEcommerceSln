using AutoMapper;
using eCommerce.Application.Features.ProductVariantFeatures.Queries;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Web.ViewModels.ProductVariantVMs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]

    public class ProductVariantController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductVariantController> _logger;
        private readonly IMediator _mediator;
        public ProductVariantController(IMapper mapper, ILogger<ProductVariantController> logger, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> ProductVariantDropdown()
        {
            var result = await _mediator.Send(new GetProductVariantsDropdownQuery());
            return Json(result);
        }

        public async Task<IActionResult> Details(Guid variantId)
        {
            try
            {
                var productVariantDto = await _mediator.Send(new GetVariantByIdQuery(variantId));
                //var productVariantVm = _mapper.Map<ProductVariantDetailsVm>(productVariant);
                return View(productVariantDto);
            }
            catch (Exception)
            {
                _logger.LogError("");
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVariantSaveVM productVariantVm)
        {
            if (!ModelState.IsValid)
            {
                return View(productVariantVm);
            }
            try
            {
                var command = _mapper.Map<ProductVariantSaveVM>(productVariantVm);
                var variantId = await _mediator.Send(command);
                return RedirectToAction("Details", new { variantId = variantId });
            }
            catch (Exception)
            {
                _logger.LogError("");
                throw;
            }
        }

        public IActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> Edit(ProductVariantSaveVM productVariantVm)
        {
            if (!ModelState.IsValid)
            {
                return View(productVariantVm);
            }
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
