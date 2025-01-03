using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrands();
            return Ok(brands);
        }


        [HttpPost]
        public IActionResult AddBrand(BrandDTO brandDto)
        {
            var brand = _brandService.AddBrand(brandDto);
            return CreatedAtAction(nameof(GetAllBrands), new { id = brand.Id }, brand);
        }
    }
}
