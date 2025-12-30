using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace eCommerce.Infrastructure.Repositories
{
    public class BrandRepository :  IBrandRepository
    {
        private readonly eCommerceDbContext _context;
        public BrandRepository(eCommerceDbContext context)
        {
            _context = context;
        }

        #region brandRepo methods
        //public async Task<Guid> CreateAsync(Brand brand)
        //{
        //    await _context.Brands.AddAsync(brand);
        //    await _context.SaveChangesAsync();

        //    return brand.BrandId;
        //}

        public async Task<List<Brand>> GetAllBrands()
        {
            return await _context.Brands
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Products)
                .ToListAsync();
        }

        //public async Task<List<Brand>> GetAllBrands(Expression<Func<Brand, bool>> whereCondition)
        //{
        //    return await _context.Brands.
        //        Where(whereCondition)
        //        .ToListAsync();
        //}

        //public async Task<Brand?> GetBrandById(Guid id)
        //{
        //    return await _context.Brands
        //        .SingleOrDefaultAsync(temp => temp.BrandId == id);
        //}

        //public async Task UpdateAsync(Brand brand)
        //{
        //    _context.Brands.Attach(brand);
        //    _context.Entry(brand).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        public async Task<bool> SoftDeleteAsync(Guid brandId)
        {
            Brand? brand = await _context.Brands
                .Where(temp => temp.BrandId == brandId)
                .FirstOrDefaultAsync();

            if (brand == null)
                return false;

            brand.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion
        public async Task<bool> ExistsByNameAsync(string brandName)
        {
            var normalized = brandName.ToLower();
            return await _context.Brands.AnyAsync(b => b.BrandName.ToLower() == normalized);
        }

        public async Task<Brand?> GetBrandById(Guid id)
        {
            return await _context.Brands
                .Include(x => x.Products)
                .Where(b => b.BrandId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> InsertBrandAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return brand.BrandId;
        }

        public async Task UpdateAsync(Brand brand)
        {
            var existingBrand = await _context.Brands.FindAsync(brand.BrandId);
            if (existingBrand == null)
                throw new KeyNotFoundException("Brand not found");

            _context.Entry(existingBrand).CurrentValues.SetValues(brand);
            _context.Entry(existingBrand).Property(x => x.CreatedAt).IsModified = false;
            _context.Entry(existingBrand).Property(x => x.CreatedBy).IsModified = false;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(Guid brandId)
        {
            var brand = await _context.Brands.FindAsync(brandId);
            if (brand == null)
                throw new KeyNotFoundException("Brand not found");

            if (brand.IsActive.HasValue)
            {
                brand.IsActive = !brand.IsActive.Value;
            }
            await _context.SaveChangesAsync();
        }        
    }
}
