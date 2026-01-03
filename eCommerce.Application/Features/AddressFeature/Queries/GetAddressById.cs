using eCommerce.Application.Features.AddressFeature.Dtos;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.AddressFeature.Queries
{
    public record GetAddressById(int addressId, Guid userId) : IRequest<AddressDto>;

    public class GetAddressByIdHandler
     : IRequestHandler<GetAddressById, AddressDto>
    {
        private readonly IAddressRepository _addressRepository;

        public GetAddressByIdHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressDto> Handle(GetAddressById request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository
                                  .GetByIdAsync(request.addressId, request.userId);

            if (address == null)
                throw new Exception("Address not found");

            return new AddressDto
            {
                AddressId = address.Id,
                UserId = address.UserId,
                AddressType = address.AddressType,
                Street = address.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                IsDefault = address.IsDefault ?? false,
            };
        }
    }

}
