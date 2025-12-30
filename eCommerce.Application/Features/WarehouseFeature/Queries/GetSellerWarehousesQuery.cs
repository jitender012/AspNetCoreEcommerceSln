using AutoMapper;
using eCommerce.Application.Features.WarehouseFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.CustomException;
using eCommerce.Domain.RepositoryContracts.Seller;
using MediatR;

namespace eCommerce.Application.Features.WarehouseFeature.Queries
{
    public record GetSellerWarehousesQuery : IRequest<List<WarehouseDto>>;
    public class GetSellerWarehousesQueryHandler : IRequestHandler<GetSellerWarehousesQuery, List<WarehouseDto>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public GetSellerWarehousesQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper, IUserContextService userContextService)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<List<WarehouseDto>> Handle(GetSellerWarehousesQuery request, CancellationToken cancellationToken)
        {
            var sellerId = _userContextService.GetUserId();
            if (sellerId != Guid.Empty)
            {
                try
                {
                    var warehouses = await _warehouseRepository.FetchBySellerIdAsync(sellerId);
                    return _mapper.Map<List<WarehouseDto>>(warehouses);
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
