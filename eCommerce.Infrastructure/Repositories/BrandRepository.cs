using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        //public async Task<List<Brand>> GetAllBrands()
        //{
        //    return await _context.Brands.ToListAsync();
        //}

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

        //public async Task<bool> DeleteAsync(Guid brandId)
        //{
        //    Brand? brand = await _context
        //        .Brands
        //        .Where(temp => temp.BrandId == brandId)
        //        .SingleOrDefaultAsync();

        //    if (brand == null)
        //    {
        //        return false;
        //    }
        //   _context.Brands.Remove(brand);
        //    return true;
        //}

        #endregion
    }
}
