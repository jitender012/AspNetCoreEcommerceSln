using AutoMapper;
using eCommerce.Application.DTO;
using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.Features.BrandFeature.Queries;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Web.Areas.Admin.Models.Brand;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;
        public BrandController(IBrandService brandService, IFileUploadService fileUploadService, IMediator mediator, IMapper mapper)
        {
            _fileUploadService = fileUploadService;
            _brandService = brandService;

            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _mediator.Send(new GetAllBrandsQuery());
            var brandsVM = _mapper.Map<List<BrandDto>, List<BrandViewModel>>(brands);
            return View(brandsVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        #region Commented Code
        //[HttpPost]
        //public async Task<IActionResult> Create(BrandViewModel data, IEnumerable<IFormFile> ImageFile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["Error"] = "Please correct the highlighted errors and try again.";
        //        return View(data);
        //    }

        //    // Map ViewModel to DTO
        //    var brandDTO = new BrandDTO
        //    {
        //        BrandName = data.BrandName,
        //        BrandDescription = data.BrandDescription,
        //    };

        //    // Handle image upload
        //    if (ImageFile.Any())
        //    {
        //        try
        //        {
        //            var folderPath = "Images/BrandImages";
        //            var fileNames = await _fileUploadService.UploadFilesAsync(ImageFile, folderPath);
        //            brandDTO.BrandImage = fileNames.FirstOrDefault();
        //        }
        //        catch (InvalidOperationException ex)
        //        {
        //            ModelState.AddModelError("brandImg", ex.Message);
        //            TempData["Error"] = "Invalid file format or size. Please try again.";
        //            return View(data);
        //        }
        //        catch (Exception)
        //        {
        //            TempData["Error"] = "An unexpected error occurred during file upload. Please try again.";
        //            //_logger.LogError(ex, "File upload error in Create method.");
        //            return View(data);
        //        }
        //    }

        //    // Save brand to the database
        //    try
        //    {
        //        await _brandService.AddBrand(brandDTO);
        //        TempData["Success"] = "Brand created successfully!";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        TempData["Error"] = "An unexpected error occurred while creating the brand. Please try again.";
        //        //_logger.LogError(ex, "Error in Create method while adding a brand.");
        //        return View(data);
        //    }
        //}
        #endregion


        [HttpPost]
        public async Task<IActionResult> Create(BrandViewModel data, IEnumerable<IFormFile> ImageFile)
        {
            if (!ModelState.IsValid)
                return View(data);

            // Map ViewModel to DTO           
            var createBrandDto = new CreateBrandDto
            {
                BrandDescription = data.BrandDescription,
                BrandName = data.BrandName,
            };

            try
            {
                if (ImageFile.Any())
                {
                    var folderPath = "Images/BrandImages";
                    var fileNames = await _fileUploadService.UploadFilesAsync(ImageFile, folderPath);
                    createBrandDto.BrandImage = fileNames.FirstOrDefault();
                }
                var command = new CreateBrandCommand(createBrandDto);
                var brandId = await _mediator.Send(command);

                TempData["Success"] = "Brand created successfully!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("brandImg", ex.Message);
                TempData["Error"] = "Invalid file format or size. Please try again.";
                return View(data);
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);

                return View(data);
            }
        }


        public async Task<IActionResult> Details(Guid brandId)
        {
            if (brandId.Equals(null))
            {
                TempData["ErrorMessage"] = "Invalid Brand Id.";
                return View("Error");
            }
            var brand = await _brandService.GetBrandByIdAsync(brandId);
            return View(brand);
        }

        public async Task<IActionResult> Edit(Guid brandId)
        {
            if (brandId.Equals(null))
            {
                TempData["ErrorMessage"] = "Invalid Brand Id.";
                return View("Error");
            }

            var brand = await _brandService.GetBrandByIdAsync(brandId);

            UpdateBrandViewModel brandViewModel = new UpdateBrandViewModel
            {
                BrandId = brandId,
                BrandName = brand.BrandName,
                BrandDescription = brand.BrandDescription,
                BrandImage = brand.BrandImage,
            };

            return View(brandViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBrandViewModel data, IEnumerable<IFormFile> ImageFile)
        {
            if (!ModelState.IsValid) return View(data);

            // Handle image upload
            if (ImageFile.Any())
            {
                try
                {
                    var folderPath = "Images/BrandImages";
                    var fileNames = await _fileUploadService.UploadFilesAsync(ImageFile, folderPath);
                    data.BrandImage = fileNames.FirstOrDefault();
                }
                catch (Exception)
                {
                    TempData["Error"] = "Image upload failed.";
                    return View(data);
                }
            }

            // Update brand in the database
            var command = _mapper.Map<UpdateBrandCommand>(data);
            var result = await _mediator.Send(command);

            if (result)
                return RedirectToAction("Index");

            TempData["Error"] = "Update failed.";
            return View(data);

        }

        public async Task<JsonResult> Delete(Guid brandId)
        {
            if (brandId == Guid.Empty)
            {
                return Json(new { success = false, message = "Invalid brand ID." });
            }
            var brand = await _brandService.GetBrandByIdAsync(brandId);

            if (brand == null)
            {
                return Json(new { success = false, message = "brand not found." });
            }
            try
            {
                await _brandService.DeleteBrandAsync(brand.BrandId);
                return Json(new { success = true, message = "brand deleted successfully!" });
            }
            catch (Exception)
            {
                //_logger.LogError(ex, "Error occurred while deleting brand with ID: {BrandId}", id);
                return Json(new { success = false, message = "An error occurred while deleting the brand. Please try again." });
            }
        }
    }
}
