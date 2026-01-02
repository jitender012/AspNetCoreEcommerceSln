using AutoMapper;
using eCommerce.Application.Features.WarehouseFeature.Commands;
using eCommerce.Application.Features.WarehouseFeature.Dtos;
using eCommerce.Application.Features.WarehouseFeature.Queries;
using eCommerce.Web.Areas.Seller.Models.StoreModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]

    public class WarehouseController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public WarehouseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var warehouses = await _mediator.Send(new GetSellerWarehousesQuery());
            return View(warehouses);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var warehouses = await _mediator.Send(new GetWarehouseByIdQuery(id));            
            //if (warehouse == null)
            //{
            //    return NotFound();
            //}
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WarehouseSaveVm warehouseSaveVm)
        {
            if (!ModelState.IsValid)
            {
                return View(warehouseSaveVm);
            }
            var warehouseSaveDto = _mapper.Map<WarehouseSaveDto>(warehouseSaveVm);
            var result = await _mediator.Send(new CreateWarehouseCommand(warehouseSaveDto));

            if (result != Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            return View(warehouseSaveVm);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableStoresForVariantAsync(Guid variantId)
        {

            var warehouses = await _mediator.Send(new GetAvailableWarehousesForVariantQuery(variantId));
            return Json(warehouses);
        }
    }
}
