using eCommerce.Application.Features.ProductConfigurationFeature.Commands;
using eCommerce.Application.Features.ProductConfigurationFeature.DTOs;
using eCommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductConfigurationController : Controller
    {
        private readonly IMediator _mediator;

        public ProductConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LinkFeature(int categoryId, int featureId)
        {
            // Map single entry to a list
            var links = new FeatureNCategoryIdsDto
            {
                CategoryId = categoryId,
                FeatureIds = new List<int> { featureId }
            };

            // Call service or mediator
            var result = await _mediator.Send(new LinkFeatureToCategoryCommand(links));

            if (!result)
                return BadRequest("Failed to link feature.");

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UnlinkFeature(int categoryId, int featureId)
        {
            // Map single entry to a list
            var links = new FeatureNCategoryIdsDto
            {
                CategoryId = categoryId,
                FeatureIds = [featureId]
            };

            // Call service or mediator
            var result = await _mediator.Send(new UnlinkFeatureFromCategoryCommand(links));
            if (!result)
                return BadRequest("Failed to unlink feature.");
            return Ok();
        }
    }
}
