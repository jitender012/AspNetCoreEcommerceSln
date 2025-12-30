using eCommerce.Application.Common.Dtos;
using eCommerce.Domain.RepositoryContracts.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WarehouseFeature.Queries
{

    public record GetAvailableWarehousesForVariantQuery(Guid variantId) : IRequest<List<IdNameDto<Guid>>>;

    public class GetAvailableWarehousesForVariantQueryHandler : IRequestHandler<GetAvailableWarehousesForVariantQuery, List<IdNameDto<Guid>>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public GetAvailableWarehousesForVariantQueryHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<List<IdNameDto<Guid>>> Handle(GetAvailableWarehousesForVariantQuery request, CancellationToken cancellationToken)
        {
            var warehouseList = await _warehouseRepository.GetAvailableWarehousesForVariantQuery(request.variantId);
            return warehouseList
                .Select(w => new IdNameDto<Guid>
                {
                    Id = w.WarehouseId,
                    Name = w.Name
                })
                .ToList();
        }
    }
}
