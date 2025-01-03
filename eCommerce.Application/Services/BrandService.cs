using eCommerce.Application.DTO;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.AdminServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository baseRepository)
        {
            _brandRepository = baseRepository;
        }
        public async Task<Guid> AddBrand(BrandDTO data)
        {
            if (string.IsNullOrEmpty(data.BrandName))
                throw new ArgumentException("Brand name is required.");

            Brand brand = new Brand()
            {
                BrandId = Guid.NewGuid(),
                BrandName = data.BrandName,
                BrandDescription = data.BrandDescription,
                BrandImage = data.BrandImage,
                CreatedBy = data.CreatedBy,
                CreatedAt = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

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
            await _brandRepository.DeleteAsync(x=>x.BrandId == id);
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
            // Fetch the existing brand from the database
            var brand = await _brandRepository.GetByIdAsync(data.BrandId);

            if (brand == null)
            {
                return false;
            }
            brand.BrandId = data.BrandId;
            brand.BrandName = data.BrandName;
            brand.BrandImage = data.BrandImage;
            brand.BrandDescription = data.BrandDescription;
            brand.UpdatedAt = DateTime.Now;
            brand.UpdatedBy = data.UpdatedBy;

            await _brandRepository.UpdateAsync(brand);

            return true;
        }

    }
}
