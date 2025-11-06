using AutoMapper;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Seller;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WarehouseFeature.Commands
{
    public record DeleteWarehouseCommand(int id) : IRequest<bool>;
    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, bool>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateWarehouseCommandHandler> _logger;
        public DeleteWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IUserContextService userContextService, IMapper mapper, ILogger<CreateWarehouseCommandHandler> logger)
        {
            _warehouseRepository = warehouseRepository;
            _userContextService = userContextService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var id = request.id;
            var userId = _userContextService.GetUserId();
            try
            {
                var warehouse = await _warehouseRepository.FetchByIdAsync(id);
                if (warehouse != null)
                {
                    await _warehouseRepository.RemoveAsync(request.id, userId);
                    return true;
                }
                return false;
            }

            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while deleting Warehouse: {id}.", id);

                throw new ApplicationException("Unable to delete warehouse to the database.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error while deleting Warehouse: {Name}.", id);

                throw new ApplicationException("An unexpected error occured.", ex);
            }
        }
    }
}
