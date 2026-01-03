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
    public record UpdateAddressCommand(AddressDto Dto) : IRequest<bool>;

    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, bool>
    {
        private readonly IAddressRepository _addressRepository;
        public UpdateAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var address = new Address
            {
                Id = dto.AddressId,
                UserId = dto.UserId,
                AddressType = dto.AddressType,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                IsDefault = dto.IsDefault,
            };

            await _addressRepository.UpdateAsync(address);
            return true;
        }
    }
}
