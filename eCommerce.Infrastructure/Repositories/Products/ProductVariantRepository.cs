using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories.Products
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

        public async Task<Guid> InsertProductVariantAsync(ProductVariant productVariant, IEnumerable<ProductImage> productImages, IEnumerable<FeatureOption> featureOptions)
        {
            try
            {
                await _context.ProductVariants.AddAsync(productVariant);
                await _context.SaveChangesAsync();

                return productVariant.ProductIvarientId;
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
