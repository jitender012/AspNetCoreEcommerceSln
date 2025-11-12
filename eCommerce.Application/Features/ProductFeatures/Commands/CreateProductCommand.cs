using AutoMapper;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Application.Features.ProductFeatures.Commands
{
    public record CreateProductCommand(ProductSaveDTO dto) : IRequest<Guid>;

    public class CreateProductHandler(IProductRepository _productRepository, IUserContextService _userContextService, ILogger<CreateProductHandler> _logger) : IRequestHandler<CreateProductCommand, Guid>
    {

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var userId = _userContextService.GetUserId();

            var Features = data.FeatureCategory?
                .SelectMany(fc => fc.ProductFeatures)
                .Where(f => !string.IsNullOrWhiteSpace(f.Value))
                .ToList() ?? new List<FeaturesDto>();

            if (data.ProductVariant == null || Features == null)
            {
                _logger.LogError("Invalid product data: Variant, images, or configurations are missing.");
                return Guid.Empty;
            }

            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = data.ProductName,
                Price = data.ProductVariant.Price,
                Description = data.Description,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = userId,
                BrandId = data.BrandId,
                CategoryId = data.CategoryId,
                Url = data.ProductVariant.ImageUrls.FirstOrDefault() ?? string.Empty
            };

            var productVariant = new ProductVariant
            {
                ProductVariantId = Guid.NewGuid(),
                VarientName = data.ProductVariant.VarientName,
                ProductId = product.ProductId,                
                Sku = data.ProductVariant.SKU,
                Price = data.ProductVariant.Price,
                Status = data.ProductVariant.Status,
                Product = product
            };

            var productImages = data.ProductVariant.ImageUrls.Select(x => new ProductImage
            {
                ProductVariantId = productVariant.ProductVariantId,
                ImageUrl = x,
                CreatedAt = DateTime.Now,
                IsPrimary = false,
                Order = 1
            }).ToList();

            var featureOptions = Features.Select(x => new FeatureOption
            {
                ProductFeatureId = x.ProductFeatureId,
                Value = x.Value,
                CreatedBy = userId.ToString(),
            }).ToList();

            try
            {
                var result = await _productRepository.InsertAsync(product, productVariant, productImages, featureOptions);
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
