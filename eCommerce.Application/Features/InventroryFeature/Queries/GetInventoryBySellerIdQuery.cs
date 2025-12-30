using eCommerce.Application.Features.InventroryFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.InventroryFeature.Queries
{
    public record GetInventoryBySellerIdQuery : IRequest<IEnumerable<InventoryDto>>;
    public class GetInventoryBySellerIdQueryHandler : IRequestHandler<GetInventoryBySellerIdQuery, IEnumerable<InventoryDto>>
    {
        private readonly IUserContextService _userContextService;
        private readonly IInventroyRepository _inventoryRepository;
        public GetInventoryBySellerIdQueryHandler(IUserContextService userContextService, IInventroyRepository inventroyRepository)
        {
            _userContextService = userContextService;
            _inventoryRepository = inventroyRepository;
        }

        public async Task<IEnumerable<InventoryDto>> Handle(GetInventoryBySellerIdQuery request, CancellationToken cancellationToken)
        {
            var sellerId = _userContextService.GetUserId();
            try
            {
                var inventories = await _inventoryRepository.GetBySellerIdAsync(sellerId);
                var inventoryDtos = inventories.Select(i => new InventoryDto
                {
                    InventoryId = i.InventoryId,
                    ProductVariantName = i.ProductVariant.VarientName!,
                    ReservedQuantity = i.ReservedQuantity,
                    WarehouseName = i.Warehouse.Name,
                    StockQuantity = i.StockQuantity
                });

                return inventoryDtos;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
