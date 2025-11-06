using eCommerce.Application.Features.CartFeature.Dtos;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.CartFeature.Queries
{
    public record GetCartByCustomerIdQuery(Guid customerId) : IRequest<CartDto>;

    public class GetCartByCustomerIdHandler : IRequestHandler<GetCartByCustomerIdQuery, CartDto>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartByCustomerIdHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> Handle(GetCartByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(request.customerId);

            if (cart == null)
                return new CartDto { CustomerId = request.customerId };

            if (cart.CartItems.Count <= 0)
            {
                return new CartDto();
            }

            var cartDto = new CartDto
            {
                CartId = cart.CartId,
                CustomerId = cart.CustomerId!.Value,
                Items = cart.CartItems.Select(ci => new CartItemDto
                {
                    CartItemId = ci.CartItemId,
                    ProductVariantId = ci.ProductVariantId,
                    ProductName = ci.ProductVariant.VarientName!,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    TotalPrice = ci.TotalPrice
                }).ToList(),
                TotalAmount = cart.CartItems.Sum(ci => ci.TotalPrice)
            };

            return cartDto;
        }
    }

}
