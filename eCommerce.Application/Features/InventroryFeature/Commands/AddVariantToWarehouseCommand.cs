using AutoMapper;
using eCommerce.Application.Features.InventroryFeature.Dtos;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.InventroryFeature.Commands
{
    public record AddVariantToWarehouseCommand(CreateInventoryDto inventoryDto) : IRequest<bool>;

    public class AddVariantToWarehouseCommandHandler : IRequestHandler<AddVariantToWarehouseCommand, bool>
    {
        private readonly IInventroyRepository _inventroyRepository;
        private readonly IMapper _mapper;
        public AddVariantToWarehouseCommandHandler(IInventroyRepository inventroyRepository, IMapper Mapper)
        {
            _inventroyRepository = inventroyRepository;
            _mapper = Mapper;
        }

        public async Task<bool> Handle(AddVariantToWarehouseCommand request, CancellationToken cancellationToken)
        {
            var data = request.inventoryDto;
         
            try
            {
                var inventory = _mapper.Map<Inventory>(data);
                inventory.CreatedOn = DateTime.UtcNow;
                await _inventroyRepository.AddVariantAsync(inventory);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
