using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts.VendorServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Vendor.Controllers
{
    [Area("Vendor")]
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
