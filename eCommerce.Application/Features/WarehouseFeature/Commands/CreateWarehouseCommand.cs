using AutoMapper;
using eCommerce.Application.Features.WarehouseFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Seller;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Features.WarehouseFeature.Commands
{
    public record CreateWarehouseCommand(WarehouseSaveDto dto) : IRequest<Guid>;

    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Guid>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateWarehouseCommandHandler> _logger;
        public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IUserContextService userContextService, IMapper mapper, ILogger<CreateWarehouseCommandHandler> logger)
        {
            _warehouseRepository = warehouseRepository;
            _userContextService = userContextService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var userId = _userContextService.GetUserId();
            try
            {
                var warehouse = _mapper.Map<Warehouse>(data);
                warehouse.UserId = userId;

                var id = await _warehouseRepository.InsertAsync(warehouse);
                return id;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Null argument passed while creating Warehouse: {Name}.", data.Name);

                throw new ApplicationException("Warehouse data can not be null.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while creating Warehouse: {Name}.", data.Name);

                throw new ApplicationException("Unable to save warehouse to the database.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occured while creating Warehouse: {Name}.", data.Name);

                throw new ApplicationException("An unexpected error occured.", ex);
            }
        }
    }
}
