using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class StoreController : Controller
    {
        private readonly IWarehouseService _storeService;
        public StoreController(IWarehouseService storeService)
        {
            _storeService = storeService;
        }
        public async Task<IActionResult> Index()
        {
            var stores = await _storeService.GetAllStores();
            return View(stores);
        }
    }
}
