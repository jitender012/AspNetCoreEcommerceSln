using AutoMapper;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.Features.ProductVariantFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts.Common;
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
            try
            {
                var product = await productRepository.GetProductDetailSeller(request.pId);
                var productDto = mapper.Map<ProductDetailsDto>(product);
                
                return productDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
