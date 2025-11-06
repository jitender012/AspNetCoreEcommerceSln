using AutoMapper;
using eCommerce.Application.Features.WarehouseFeature.Dtos;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Seller;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eCommerce.Application.Features.WarehouseFeature.Commands
{
    public record UpdateWarehouseCommand(WarehouseSaveDto dto) : IRequest<bool>;
    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, bool>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateWarehouseCommandHandler> _logger;
        public UpdateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMapper mapper, ILogger<CreateWarehouseCommandHandler> logger)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Brand data cannot be null.");
            }

            try
            {
                // Fetch the existing brand from the database
                var existingWarehouse = await _warehouseRepository.FetchByIdAsync(data.WarehouseId);
                if (existingWarehouse == null)
                {
                    _logger?.LogWarning("Warehouse with ID {WarehouseId} not found for update.", data.WarehouseId);
                    return false;
                }

                var updatedWarehouse = _mapper.Map<Warehouse>(data);

                var result = await _warehouseRepository.ModifyAsync(updatedWarehouse);

                _logger?.LogInformation("Warehouse with ID {WarehouseId} successfully updated.", data.WarehouseId);
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while updating Warehouse with ID {WarehouseId}.", data.WarehouseId);
                throw new ApplicationException("An error occurred while updating the Warehouse. Please try again later.", ex);
            }
        }
    }
}
