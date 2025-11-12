using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class StoreController : Controller
    {
        private readonly IMediator _mediator;
        public StoreController(IMediator mediator)
        {
            _mediator= mediator;
        }
        public async Task<IActionResult> Index()
        {            
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
