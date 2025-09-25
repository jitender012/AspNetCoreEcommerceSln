using AutoMapper;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Features.ProductFeatures.Commands
{
    public record CreateProductCommand(ProductSaveDTO dto) : IRequest<Guid>;

    public class CreateProductHandler(IProductRepository _productRepository, IUserContextService _userContextService, ILogger<CreateProductHandler> _logger, IMapper _mapper) : IRequestHandler<CreateProductCommand, Guid>
    {

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var userId = _userContextService.GetUserId();

            if (data.ProductVariant == null || data.Features == null)
            {
                _logger.LogError("Invalid product data: Variant, images, or configurations are missing.");
                return Guid.Empty;
            }

            var productVariantt = _mapper.Map<ProductVariant>(data.ProductVariant);
            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = data.ProductVariant.VarientName,
                Price = data.ProductVariant.Price,
                Description = data.Description,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = userId,
                BrandId = data.BrandId,
                CategoryId = data.CategoryId,
            };

            var productVariant = new ProductVariant
            {
                ProductIvarientId = Guid.NewGuid(),
                VarientName = data.ProductVariant.VarientName,
                ProductId = product.ProductId, // Not data.ProductId!
                Quantity = data.ProductVariant.Quantity,
                Sku = data.ProductVariant.SKU,
                Price = data.ProductVariant.Price,
                IsActive = data.ProductVariant.IsActive,
                Product = product
            };

            var productImages = data.ProductVariant.ImageUrls.Select(x => new ProductImage
            {
                ImageUrl = x,
                CreatedAt = DateTime.Now,
                IsPrimary = false,
                Order = 1,
            });

            var featureValues = data.Features.Select(x => new FeatureOption
            {
                ProductFeatureId = x.ProductFeaturesId,
                Value = x.Value,
                CreatedBy = userId.ToString(),
            });

            try
            {
                var result = await _productRepository.InsertAsync(product, productVariant, productImages, featureValues);
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
    }
}
