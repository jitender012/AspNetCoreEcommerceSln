using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Web.Areas.Admin.Models.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IFileUploadService _fileUploadService;
        public BrandController(IBrandService brandService, IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllBrands();
            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }
    
        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateViewModel data, IEnumerable<IFormFile> ImageFile)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the highlighted errors and try again.";
                return View(data);
            }

            // Get the current user's ID
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "User authentication failed. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            // Map ViewModel to DTO
            var brandDTO = new CreateBrandDTO
            {
                BrandName = data.BrandName,
                BrandDescription = data.BrandDescription,
                CreatedBy = Guid.Parse(userId)
            };

            // Handle image upload
            if (ImageFile.Any())
            {
                try
                {
                    var folderPath = "Images/BrandImages";
                    var fileNames = await _fileUploadService.UploadFilesAsync(ImageFile, folderPath);
                    brandDTO.BrandImage = fileNames.FirstOrDefault();
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("brandImg", ex.Message);
                    TempData["Error"] = "Invalid file format or size. Please try again.";
                    return View(data);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An unexpected error occurred during file upload. Please try again.";
                    //_logger.LogError(ex, "File upload error in Create method.");
                    return View(data);
                }
            }

            // Save brand to the database
            try
            {
                await _brandService.AddBrand(brandDTO);
                TempData["Success"] = "Brand created successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An unexpected error occurred while creating the brand. Please try again.";
                //_logger.LogError(ex, "Error in Create method while adding a brand.");
                return View(data);
            }
        }

    }
}
