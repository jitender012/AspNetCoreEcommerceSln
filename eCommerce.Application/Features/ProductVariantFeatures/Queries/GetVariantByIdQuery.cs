using AutoMapper;
using eCommerce.Application.Features.ProductVariantFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductVariantFeatures.Queries
{
    public record GetVariantByIdQuery(Guid variantId) : IRequest<ProductVariantDetailsDto>;

    public class GetVariantByIdQueryHandler : IRequestHandler<GetVariantByIdQuery, ProductVariantDetailsDto>
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;
        public GetVariantByIdQueryHandler(IProductVariantRepository productVariantRepository, IMapper mapper)
        {
            _productVariantRepository = productVariantRepository;
            _mapper = mapper;
        }
        
        public async Task<ProductVariantDetailsDto> Handle(GetVariantByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var variant = await _productVariantRepository.GetProductVariantByIdAsync(request.variantId);
                if (variant == null)
                {
                    throw new KeyNotFoundException($"Product variant with ID {request.variantId} not found.");
                }

                var variantDetailsDto = new ProductVariantDetailsDto
                {
                    ProductIvarientId = variant.ProductVariantId,
                    VariantName = variant.VarientName,
                    Price = variant.Price,
                    Sku = variant.Sku,
                    Barcode = variant.Barcode,
                    IsActive = variant.Status == Domain.Entities.ProductStatus.Active,
                    Quantity = variant.Inventories.Sum(i => i.StockQuantity),
                    ImageUrls = variant.ProductImages.Select(pi => pi.ImageUrl).ToList(),
                    Features = variant.ProductConfigurations
                                    .Select(pc => new ProductFeatureDto
                                        {
                                            Name = pc.FeatureOption.ProductFeature.Name,
                                            Value = pc.FeatureOption.Value
                                        })
                                    .ToList()
                };

                return variantDetailsDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
