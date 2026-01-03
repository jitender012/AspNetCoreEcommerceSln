using eCommerce.Application.Features.AddressFeature.Commands;
using eCommerce.Application.Features.AddressFeature.Dtos;
using eCommerce.Application.Features.AddressFeature.Queries;
using eCommerce.Application.ServiceContracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eCommerce.Web.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize(Roles = "Customer")]
    public class AddressController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;
        public AddressController(IMediator mediator, IUserContextService userContextService)
        {
            _mediator = mediator;
            _userContextService = userContextService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userContextService.GetUserId();

            var addresses = await _mediator.Send(new GetAddressByUserId(userId));
            return PartialView("_AddressPartial", addresses);
        }

        public async Task<JsonResult> AddAddress(AddressDto dto)
        {

            var result = await _mediator.Send(new CreateAddressCommand(dto));

            if (result)
            {
                return Json(new { success = true, message = "Address Added" });
            }
            return Json(new {success = false, message = "Error" });
        }

        [HttpPost]
        public IActionResult EditAddresses(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteAddresses()
        {
            return View();
        }

    }
}
