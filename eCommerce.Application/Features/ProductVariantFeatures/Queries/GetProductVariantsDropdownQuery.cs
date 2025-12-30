using eCommerce.Application.Common.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductVariantFeatures.Queries
{
    public record GetProductVariantsDropdownQuery : IRequest<List<IdNameDto<Guid>>>;

    public class GetProductVariantsDropdownHandler : IRequestHandler<GetProductVariantsDropdownQuery, List<IdNameDto<Guid>>>
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IUserContextService _userContextService;
        public GetProductVariantsDropdownHandler(IProductVariantRepository productVariantRepository, IUserContextService userContextService)
        {
            _productVariantRepository = productVariantRepository;
            _userContextService = userContextService;
        }
        public async Task<List<IdNameDto<Guid>>> Handle(GetProductVariantsDropdownQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();

            var productVariants = await _productVariantRepository.GetProductVariantBySellerId(userId);
            return productVariants
                .Select(pv => new IdNameDto<Guid>
                {
                    Id = pv.ProductVariantId,
                    Name = pv.Product.ProductName + " - " + pv.VarientName
                })
                .ToList();
        }
    }
}
