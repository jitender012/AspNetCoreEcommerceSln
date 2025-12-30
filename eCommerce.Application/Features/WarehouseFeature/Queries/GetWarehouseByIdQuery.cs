using AutoMapper;
using eCommerce.Application.Features.WarehouseFeature.Dtos;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WarehouseFeature.Queries
{
    public record GetWarehouseByIdQuery(Guid id) : IRequest<WarehouseDetailsDto>;

    public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, WarehouseDetailsDto>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        public GetWarehouseByIdQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }
        public async Task<WarehouseDetailsDto> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.FetchByIdAsync(request.id);

            var warehouseDto = _mapper.Map<WarehouseDetailsDto>(warehouse);
            return warehouseDto;
        }
    }
}
