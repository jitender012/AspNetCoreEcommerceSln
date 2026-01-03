using eCommerce.Application.Features.AddressFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.AddressFeature.Commands
{
    public record CreateAddressCommand(AddressDto dto) : IRequest<bool>;

    public class CreateAddressCommandHandler
        : IRequestHandler<CreateAddressCommand, bool>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserContextService _userContextService;
        private readonly ILogger<CreateAddressCommandHandler> _logger;
        public CreateAddressCommandHandler(IAddressRepository addressRepository, IUserContextService userContextService, ILogger<CreateAddressCommandHandler> logger)
        {
            _addressRepository = addressRepository;
            _userContextService = userContextService;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var dto = request.dto;
            var userId = _userContextService.GetUserId();

            var address = new Address
            {                
                UserId = userId,
                AddressType = dto.AddressType,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                Country = "India",
                Landmark = dto.Landmark,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                IsDefault = dto.IsDefault,
            };
            try
            {
                await _addressRepository.AddAsync(address);
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);                
            }
            return true;
        }
    }
}
