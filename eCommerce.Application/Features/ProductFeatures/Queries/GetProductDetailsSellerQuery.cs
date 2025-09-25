using AutoMapper;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.Features.ProductVariantFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductFeatures.Queries
{
    public record GetProductDetailsSellerQuery(Guid pId) : IRequest<ProductDetailsDto>;

    public class GetProductDetailsSellerHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductDetailsSellerQuery, ProductDetailsDto>
    {
        public async Task<ProductDetailsDto> Handle(GetProductDetailsSellerQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductDetailSeller(request.pId);

            var productDto = mapper.Map<ProductDetailsDto>(product);

            if (product != null)
            {
                foreach (var variantDto in productDto.ProductVariants)
                {
                    var variant = product.ProductVariants
                        .First(v => v.ProductIvarientId == variantDto.ProductIvarientId);

                    variantDto.Features = variant.ProductConfigurations
                        .Select(pc => new ProductFeatureDto
                        {
                            Name = pc.FeatureOption.ProductFeature.Name,
                            Value = pc.FeatureOption.Value
                        })
                        .ToList();
                }
            }

            return productDto;
        }
    }
}
