using AutoMapper;
using eCommerce.Application.Features.WarehouseFeature.Dtos;
using eCommerce.Domain.RepositoryContracts.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WarehouseFeature.Queries
{

    public record GetWarehousesQuery : IRequest<List<WarehouseListDto>>;
    public class GetWarehousesQueryHandler : IRequestHandler<GetWarehousesQuery, List<WarehouseListDto>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        public GetWarehousesQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }      

        public async Task<List<WarehouseListDto>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseRepository.FetchAllAsync();

            var warehouseDtos = _mapper.Map<List<WarehouseListDto>>(warehouses);
            return warehouseDtos;
        }
    }
}
