using eCommerce.Application.Features.AddressFeature.Dtos;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;
using System;
using System.Collections.Generic;
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

        public CreateAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> Handle(
            CreateAddressCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.dto;

            var address = new Address
            {
                AddressId = dto.AddressId,
                UserId = dto.UserId,
                AddressType = dto.AddressType,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                IsDefault = dto.IsDefault,
            };

            await _addressRepository.AddAsync(address);
            return true;
        }
    }
}
