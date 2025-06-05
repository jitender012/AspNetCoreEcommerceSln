using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Services.AdminServices
{
    public class BrandService(IBrandRepository baseRepository, IUserContextService userContextService, ILogger<BrandService> logger) : IBrandService
    {
        private readonly IBrandRepository _brandRepository = baseRepository;
        private readonly IUserContextService _userContextService = userContextService;
        private readonly ILogger<BrandService> _logger = logger;

        public async Task<Guid> AddBrand(BrandDTO data)
        {
            if (string.IsNullOrEmpty(data.BrandName))
                throw new ArgumentNullException("Brand name is required.");

            Brand brand = new ()
            {
                BrandId = Guid.NewGuid(),
                BrandName = data.BrandName,
                BrandDescription = data.BrandDescription,
                BrandImage = data.BrandImage,
                CreatedAt = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            var userId = _userContextService.GetUserId();

            if (userId.HasValue)
            {
                brand.CreatedBy = userId.Value;
            }

            var result = await _brandRepository.InsertAsync(brand);

            return result.BrandId;
        }

        public async Task<bool> DeleteBrandAsync(Guid id)
        {
            if (id.Equals(null))
                throw new ArgumentNullException("Invalid Brand Id.");

            var brand = await _brandRepository
               .GetByIdAsync(id);

            if (brand == null)
            {
                throw new ArgumentException("Brand not found");
            }
            await _brandRepository.DeleteAsync(x => x.BrandId == id);
            return true;
        }

        public async Task<List<BrandDTO>> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllAsync();            
            return BrandDTO.FromBrandList(brands);
        }

        public async Task<BrandDTO> GetBrandByIdAsync(Guid id)
        {
            if (id.Equals(null))
                throw new ArgumentNullException("Invalid Brand Id.");

            var brand = await _brandRepository
                .GetByIdAsync(id);

            return brand == null ? throw new KeyNotFoundException($"Brand with ID {id} not found.") : BrandDTO.FromBrand(brand);
        }
        public async Task<bool> UpdateBrand(BrandDTO data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Brand data cannot be null.");
            }

            try
            {
                // Fetch the existing brand from the database
                var brand = await _brandRepository.GetByIdAsync(data.BrandId);
                if (brand == null)
                {
                    _logger?.LogWarning("Brand with ID {BrandId} not found for update.", data.BrandId);
                    return false;
                }

                // Update the brand properties
                brand.BrandName = data.BrandName;
                brand.BrandImage = data.BrandImage;
                brand.BrandDescription = data.BrandDescription;
                brand.UpdatedAt = DateTime.UtcNow; // Use UTC for consistency
                brand.UpdatedBy = _userContextService.GetUserId();

                await _brandRepository.UpdateAsync(brand);

                _logger?.LogInformation("Brand with ID {BrandId} successfully updated.", data.BrandId);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while updating brand with ID {BrandId}.", data.BrandId);
                throw new ApplicationException("An error occurred while updating the brand. Please try again later.", ex);
            }
        }


    }
}
