using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.DTO.VendorDTOs;
using eCommerce.Application.ServiceContracts;
using eCommerce.Application.ServiceContracts.ProductServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Services
{
    public class ProductService(IProductRepository productRepository, IUserContextService userContextService, ILogger<ProductService> logger) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IUserContextService _userContextService = userContextService;
        private readonly ILogger<ProductService> _logger = logger;

        public async Task<Guid> AddAsync(ProductDTO data)
        {

            var userId = _userContextService.GetUserId();

            if (data.ProductVariantDTO == null || data.ProuctImages == null || data.ProductConfigurations == null)
            {
                _logger.LogError("Invalid product data: Variant, images, or configurations are missing.");
                return Guid.Empty;
            }

            Product product = new()
            {
                ProductId = Guid.NewGuid(),
                ProductName = data.ProductName,
                Price = data.Price,
                Url = data.Url,
                Description = data.Description,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = userId ?? Guid.Empty,
            };

            ProductVariant productVariant = new()
            {
                ProductIvarientId = Guid.NewGuid(),
                VarientName = data.ProductName,
                ProductId = data.ProductId,
                Quantity = data.ProductVariantDTO!.Quantity,
                Sku = data.ProductVariantDTO.Sku,
                Price = data.Price,
                IsActive = data.ProductVariantDTO.IsActive
            };

            try
            {
                var result = await _productRepository.InsertAsync(product, productVariant, data.ProuctImages!, data.ProductConfigurations!);
                if (result == Guid.Empty)
                {
                    _logger.LogError("Something went wrong while inserting product.");
                    return Guid.Empty;
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding product {ProductName}", product.ProductName);
                throw;
            }
        }
        public Task<List<ProductDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetProductById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> GetPtoductsByBrand(Guid id)
        {
            throw new NotImplementedException();
        }
       
        
        public async Task<List<SellerProductDTO>> GetProductsBySeller()
        {
            var userId = _userContextService.GetUserId();

            if (userId.HasValue && userId != Guid.Empty)
            {               
                IEnumerable<Product> products = await _productRepository.FetchBySellerIdAsync(userId.Value);
                
                return products.Select(p => new SellerProductDTO
                {
                    ProductName = p.ProductName,
                    Url = p.Url,
                    ProductVariants = p.ProductVariants.Select(pv => new SellerProductVariantDTO
                    {
                        VarientName = pv.VarientName,
                        Price = pv.Price,
                        Quantity = pv.Quantity,
                        IsActive = pv.IsActive
                    }).ToList(),
                    TotalStock = p.ProductVariants.Sum(pv=>pv.Quantity),
                    MaxPrice = p.ProductVariants.Max(pv=>pv.Price),
                    MinPrice = p.ProductVariants.Min(pv=>pv.Price)
                }).ToList();
            }
            else
            {
                return new List<SellerProductDTO>(); // Return empty list if userId is invalid
            }
        }

        public Task<bool> UpdateProduct(ProductDTO data)
        {
            throw new NotImplementedException();
        }

       
    }
}
