using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(eCommerceDbContext context) : base(context) { }

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
                .Include(x=>x.Products)
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


    }
}
