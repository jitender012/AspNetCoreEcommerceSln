using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.Features.BrandFeature.Queries;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Web.Areas.Admin.Models.Brand;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;
        private readonly IValidator<BrandSaveDTO> _validator;
        public BrandController(IFileUploadService fileUploadService, IMediator mediator, IMapper mapper, IValidator<BrandSaveDTO> validator)
        {
            _fileUploadService = fileUploadService;

            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _mediator.Send(new GetAllBrandsQuery());
            var brandsVM = _mapper.Map<List<BrandListDTO>, List<BrandListVM>>(brands);
            return View(brandsVM);
        }

        public async Task<IActionResult> Details(Guid brandId)
        {
            if (brandId.Equals(null))
            {
                TempData["ErrorMessage"] = "Invalid Brand Id.";
                return View("Error");
            }
            var brand = await _mediator.Send(new GetBrandByIdQuery(brandId));

            BrandDetailsVM brandVM = _mapper.Map<BrandDetailsVM>(brand);
            return View(brandVM);
        }

        public IActionResult Create()
        {
            BrandSaveVM brand = new BrandSaveVM
            {
                IsActive = true
            };
            return View(brand);
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
        public async Task<IActionResult> Create(BrandSaveVM model)
        {

            // Map VM to DTO
            var dto = _mapper.Map<BrandSaveDTO>(model);

            // Upload Image
            if (model.ImageFile != null)
            {
                var folderPath = "Images/BrandImages";
                var fileNames = await _fileUploadService.UploadImageAsync(new List<IFormFile> { model.ImageFile }, folderPath);
                dto.BrandImage = fileNames.FirstOrDefault();
            }

            // Validate DTO
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            // Send CQRS Command
            var command = new CreateBrandCommand(dto);
            var result = await _mediator.Send(command);
            if (result != Guid.Empty)
            {
                TempData["Success"] = "Brand created successfully!";

                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var brand = await _mediator.Send(new GetBrandForEditQuery(id));
            if (brand == null)
            {
                return NotFound();
            }

            BrandSaveVM brandVM = _mapper.Map<BrandSaveVM>(brand);
            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BrandSaveVM data, IEnumerable<IFormFile> ImageFile)
        {
            if (!ModelState.IsValid) return View(data);

            // Handle image upload
            if (ImageFile.Any())
            {
                try
                {
                    var folderPath = "Images/BrandImages";
                    var fileNames = await _fileUploadService.UploadImageAsync(ImageFile, folderPath);
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
        [HttpDelete]
        public async Task<JsonResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new { success = false, message = "Invalid brand ID." });
            }
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (result)
                return Json(new { success = true, message = "brand deleted successfully!" });
            else
                return Json(new { success = false, message = "An error occurred while deleting the brand. Please try again." });
        }
    }
}
