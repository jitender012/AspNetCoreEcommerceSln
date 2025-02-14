using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly eCommerceDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(eCommerceDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        #region Common CRUD methods
        public async Task<IEnumerable<Product>> FetchAllAsync()
        {
            try
            {
                return await _context.Products
                                            .Include(p => p.ProductVariants)
                                                .ThenInclude(v => v.ProductImages)
                                            .ToListAsync(); ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching all records.");
                throw;
            }
        }

        public async Task<IEnumerable<Product>> FetchBySellerIdAsync(Guid vendorId)
        {
            if (vendorId == Guid.Empty)
                throw new ArgumentException("Invalid vendor ID.");

            var products = await _context.Products
                                        .Where(p => p.CreatedBy == vendorId)
                                        .Include(p => p.ProductVariants)
                                            .ThenInclude(v => v.ProductImages)
                                        .Include(b=>b.Brand)
                                        .ToListAsync();
            return products;
        }

        public async Task<Product?> FetchByIdAsync(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Invalid product ID.");

            return await _context.Products
                    .Include(x => x.Category)
                    .Include(x => x.ProductVariants)
                    .FirstOrDefaultAsync(temp => temp.ProductId == productId);

        }
        public async Task<Guid> InsertAsync(Product product, ProductVariant productVariant, IEnumerable<ProductImage> productImages, IEnumerable<ProductConfiguration> configurations)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                productVariant.ProductId = product.ProductId;

                await _context.ProductVariants.AddAsync(productVariant);
                await _context.SaveChangesAsync();

                foreach (var image in productImages)
                {
                    image.ProductVariantId = productVariant.ProductIvarientId;
                }
                await _context.ProductImages.AddRangeAsync(productImages);

                foreach (var conf in configurations)
                {
                    conf.ProductVarientId = productVariant.ProductIvarientId;
                }
                await _context.ProductConfigurations.AddRangeAsync(configurations);

                await _context.SaveChangesAsync();
                transaction.Commit();

                return product.ProductId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error while inserting product data.");
                return Guid.Empty;
            }
        }

        public async Task<bool> ModifyAsync(Product product, ProductVariant productVariant, IEnumerable<ProductImage> productImages, IEnumerable<ProductConfiguration> configurations)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingProduct = await _context.Products
                    .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.ProductImages)
                    .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.ProductConfigurations)
                    .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

                if (existingProduct == null)
                {
                    _logger.LogWarning("Product not found with ID: {ProductId}", product.ProductId);
                    return false;
                }

                // Update product details
                _context.Entry(existingProduct).CurrentValues.SetValues(product);

                // Update product variant
                var existingVariant = existingProduct.ProductVariants.FirstOrDefault(v => v.ProductIvarientId == productVariant.ProductIvarientId);
                if (existingVariant != null)
                {
                    _context.Entry(existingVariant).CurrentValues.SetValues(productVariant);
                }
                else
                {
                    _logger.LogWarning("Product variant not found for product ID: {ProductId}", product.ProductId);
                    return false;
                }

                // Remove old images and insert new ones
                _context.ProductImages.RemoveRange(existingVariant.ProductImages);
                foreach (var image in productImages)
                {
                    image.ProductVariantId = existingVariant.ProductIvarientId;
                }
                await _context.ProductImages.AddRangeAsync(productImages);

                // Remove old configurations and insert new ones
                _context.ProductConfigurations.RemoveRange(existingVariant.ProductConfigurations);
                foreach (var config in configurations)
                {
                    config.ProductVarientId = existingVariant.ProductIvarientId;
                }
                await _context.ProductConfigurations.AddRangeAsync(configurations);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error while modifying product data.");
                throw;
            }
        }

        public Task<bool> RemoveAsync(Guid productId)
        {
            throw new NotImplementedException();
        }
        #endregion







        public Task<bool> SoftDeleteProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetRecentlyAddedProductsAsync(int count)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByBrandAsync(Guid brandId)
        {
            throw new NotImplementedException();
        }
    }
}
