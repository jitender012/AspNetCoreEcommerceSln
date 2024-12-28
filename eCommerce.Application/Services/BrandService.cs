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
        public async Task<Guid> AddBrand(CreateBrandDTO data)
        {
            data.BrandId = Guid.NewGuid();
            data.CreatedAt = DateTime.Now;
            data.IsActive = true;
            data.IsDeleted = false;
                                    
            Brand brand = data.ToBrand();          
            var result = await _brandRepository.InsertAsync(brand);
            return result.BrandId;
        }

        public bool DeleteBrand(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CreateBrandDTO>> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllAsync();
            return CreateBrandDTO.FromBrandList(brands);
        }

        public async Task<CreateBrandDTO> GetBrandById(Guid id)
        {
            if (id.Equals(null))
                throw new ArgumentNullException("Invalid Brand Id.");

            var brand = await _brandRepository
                .GetByIdAsync(id);

            return brand == null ? throw new KeyNotFoundException($"Brand with ID {id} not found.") : CreateBrandDTO.FromBrand(brand);
        }

        public bool UpdateBrand(CreateBrandDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
