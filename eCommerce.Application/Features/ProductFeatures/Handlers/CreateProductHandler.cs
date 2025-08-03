using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.Features.ProductFeatures.Commands;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductFeatures.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserContextService _userContextService;
        private readonly ILogger<CreateProductHandler> _logger;

        public CreateProductHandler(
            IProductRepository productRepository,
            IUserContextService userContextService,
            ILogger<CreateProductHandler> logger)
        {
            _productRepository = productRepository;
            _userContextService = userContextService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var userId = _userContextService.GetUserId();

            if (data.ProductVariant == null || data.ProuctImages == null || data.ProductFeatures == null)
            {
                _logger.LogError("Invalid product data: Variant, images, or configurations are missing.");
                return Guid.Empty;
            }

            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = data.ProductName,
                Price = data.Price,
                Url = data.Url,
                Description = data.Description,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = userId,
            };

            var productVariant = new ProductVariant
            {
                ProductIvarientId = Guid.NewGuid(),
                VarientName = data.ProductName,
                ProductId = product.ProductId, // Not data.ProductId!
                Quantity = data.ProductVariant.Quantity,
                Sku = data.ProductVariant.Sku,
                Price = data.Price,
                IsActive = data.ProductVariant.IsActive
            };

            var productImages = data.ProuctImages.Select(x => new ProductImage
            {
                CreatedAt = DateTime.Now,
                ImageUrl = x.ImageUrl,
                IsPrimary = x.IsPrimary,
                Order = x.Order,
            });

            var featureValues = data.ProductFeatures.Select(x => new FeatureOption
            {
                ProductFeatureId = x.ProductFeaturesId,
                Value = x.Value,
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
