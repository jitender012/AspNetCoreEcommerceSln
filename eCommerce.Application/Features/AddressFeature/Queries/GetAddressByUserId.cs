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
    public record GetAddressByUserId( Guid userId) : IRequest<List<AddressDto>>;

    public class GetAddressByUserIdHandler : IRequestHandler<GetAddressByUserId, List<AddressDto>>
    {
        private readonly IAddressRepository _addressRepository;

        public GetAddressByUserIdHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<AddressDto>> Handle(
            GetAddressByUserId request,
            CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository
                .GetByUserIdAsync(request.userId);

            return addresses.Select(a => new AddressDto
            {
                AddressId = a.AddressId,
                UserId = a.UserId,
                AddressType = a.AddressType,
                Street = a.Street,
                City = a.City,
                State = a.State,
                PostalCode = a.PostalCode,
                IsDefault = a.IsDefault
            }).ToList();
        }
    }
}
