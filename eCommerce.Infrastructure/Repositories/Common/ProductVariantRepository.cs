using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Common;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Common
{
    public class ProductVariantRepository : IProductVariantRepository
    {

        private readonly eCommerceDbContext _context;
        public ProductVariantRepository(eCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductVariant>> GetProductVariantsAsync()
        {
            try
            {
                return await _context.ProductVariants.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProductVariant>> GetProductVariantBySellerId(Guid sellerId)
        {
            try
            {
                return await _context.ProductVariants
                    .Where(x => x.Product.CreatedBy == sellerId)
                    .Include(x=>x.Product)
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ProductVariant?> GetProductVariantByIdAsync(Guid productVariantId)
        {
            try
            {
                return await _context.ProductVariants
                    .Include(x=>x.ProductImages)
                    .Include(x=>x.Inventories)
                    .Include(x=>x.ProductConfigurations)
                        .ThenInclude(pc=>pc.FeatureOption)
                            .ThenInclude(fo=>fo.ProductFeature)
                    .FirstOrDefaultAsync(pv => pv.ProductVariantId == productVariantId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> InsertProductVariantAsync(ProductVariant productVariant, IEnumerable<ProductImage> productImages, IEnumerable<FeatureOption> featureOptions)
        {
            try
            {
                await _context.ProductVariants.AddAsync(productVariant);
                await _context.SaveChangesAsync();

                return productVariant.ProductVariantId;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong.", ex);
            }
        }

        public Task<bool> UpdateProductVariantAsync(ProductVariant product)
        {
            throw new NotImplementedException();
        }
    }
}
