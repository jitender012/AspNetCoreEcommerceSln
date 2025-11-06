using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.Features.BrandFeature.Queries;
using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using eCommerce.Web.Areas.Admin.Models.Brand;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("brands")]
    public class BrandController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;
        private readonly IValidator<BrandSaveDto> _validator;
        public BrandController(IFileUploadService fileUploadService, IMediator mediator, IMapper mapper, IValidator<BrandSaveDto> validator)
        {
            _fileUploadService = fileUploadService;

            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var brands = await _mediator.Send(new GetAllBrandsQuery());
            var brandsVM = _mapper.Map<List<BrandListVM>>(brands);
            return View(brandsVM);
        }

        [Route("{brandId}")]
        public async Task<IActionResult> Details(Guid brandId)
        {
            var brandDto = await _mediator.Send(new GetBrandByIdQuery(brandId));
            if (brandDto == null)
                return NotFound();

            var brandVm = _mapper.Map<BrandVM>(brandDto);
            return View(brandVm);
        }

        [Route("create")]
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
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandSaveVM model)
        {

            // Map VM to DTO
            var dto = _mapper.Map<BrandSaveDto>(model);

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
        [Route("edit/{brandId}")]
        public async Task<IActionResult> Edit([FromRoute(Name = "brandId")] Guid id)
        {
            var brand = await _mediator.Send(new GetBrandForEditQuery(id));
            if (brand == null)
            {
                return NotFound();
            }

            BrandSaveVM brandVM = _mapper.Map<BrandSaveVM>(brand);
            return View(brandVM);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(BrandSaveVM data)
        {
            if (!ModelState.IsValid) return View(data);
            IEnumerable<IFormFile> imageFiles = new List<IFormFile>();
            if (data.ImageFile != null)
            {
                imageFiles = new List<IFormFile> { data.ImageFile };
            }
            // Handle image upload
            if (imageFiles.Any())
            {
                try
                {
                    var folderPath = "Images/BrandImages";
                    var fileNames = await _fileUploadService.UploadImageAsync(imageFiles, folderPath);
                    data.BrandImage = fileNames.FirstOrDefault();
                }
                catch (Exception)
                {
                    TempData["Error"] = "Image upload failed.";
                    return View(data);
                }
            }

            // Update brand in the database
            var brandSaveDto = _mapper.Map<BrandSaveDto>(data);
            var result = await _mediator.Send(new UpdateBrandCommand(brandSaveDto));

            if (result)
            {
                TempData["Success"] = "Brand updated successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Update failed.";
            return View(data);

        }

        [HttpPost("update-status/{brandid}")]
        public async Task<IActionResult> UpdateStatus(Guid brandId)
        {
            var result = await _mediator.Send(new UpdateBrandStatusCommand(brandId));

            if (result)
            {
                var updatedBrand = await _mediator.Send(new GetBrandByIdQuery(brandId));
                var brandVm = _mapper.Map<BrandVM>(updatedBrand);
                return PartialView("_BrandRowPartial", brandVm);
            }
            return BadRequest("Failed to update brand status.");
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<JsonResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Json(new { success = false, message = "Invalid brand ID." });
            }
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (result)
            {
                TempData["Success"] = "Brand deleted successfully!";
                return Json(new { success = true, message = "Brand deleted successfully!" });
            }
            else
                return Json(new { success = false, message = "An error occurred while deleting the brand. Please try again." });
        }
    }
}
