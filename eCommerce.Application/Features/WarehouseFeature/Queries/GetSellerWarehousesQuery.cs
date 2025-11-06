using AutoMapper;
using eCommerce.Application.Features.WarehouseFeature.Dtos;
using eCommerce.Domain.CustomException;
using eCommerce.Domain.RepositoryContracts.Seller;
using MediatR;

namespace eCommerce.Application.Features.WarehouseFeature.Queries
{
    public record GetSellerWarehousesQuery(Guid sellerId) : IRequest<List<WarehouseListDto>>;
    public class GetSellerWarehousesQueryHandler : IRequestHandler<GetSellerWarehousesQuery, List<WarehouseListDto>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        public GetSellerWarehousesQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<List<WarehouseListDto>> Handle(GetSellerWarehousesQuery request, CancellationToken cancellationToken)
        {
            var sellerId = request.sellerId;
            if (sellerId == Guid.Empty)
            {
                try
                {
                    var warehouses = await _warehouseRepository.FetchBySellerIdAsync(sellerId);
                    return _mapper.Map<List<WarehouseListDto>>(warehouses);
                }
                catch (Exception ex)
                {
                    throw new NotFoundException("No warehouse found." + ex);
                }
            }
            throw new ArgumentException($"Invalid user id.");
        }
    }
}
