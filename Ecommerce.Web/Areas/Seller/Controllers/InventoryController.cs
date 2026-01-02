using eCommerce.Application.Features.InventroryFeature.Commands;
using eCommerce.Application.Features.InventroryFeature.Dtos;
using eCommerce.Application.Features.InventroryFeature.Queries;
using eCommerce.Web.Areas.Seller.Models.InventoryModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles ="Seller")]
    public class InventoryController : Controller
    {
        private readonly IMediator _mediator;
        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //public async Task<IActionResult> Index()
        //{

        //    var inventories = await _mediator.Send(new GetInventoryBySellerIdQuery());
        //    return View(inventories);
        //}

        public IActionResult Index()
        {


            List<InventoryViewModel> inventories = _mediator.Send(new GetInventoryBySellerIdQuery()).Result
                .Select(i => new InventoryViewModel
                {
                    InventoryId = i.InventoryId,
                    ProductVariantName = i.ProductVariantName,
                    WarehouseName = i.WarehouseName,
                    StockQuantity = i.StockQuantity,
                    ReservedQuantity = i.ReservedQuantity
                }).ToList();

            return View(inventories);
        }

        [HttpPost]
        public async Task<JsonResult> CreateInventory(CreateInventoryDto createInventoryDto)
        {
            var result = await _mediator.Send(new AddVariantToWarehouseCommand(createInventoryDto));

            return result ? Json(new { success = true, message = "Inventory added successfully." }) 
                          : Json(new { success = false, message = "Failed to add inventory." });
        }
    }
}
