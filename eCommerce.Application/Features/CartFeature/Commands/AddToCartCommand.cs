using eCommerce.Application.Features.CartFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Customer;
using eCommerce.Domain.RepositoryContracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.CartFeature.Commands
{
    public record AddToCartCommand(AddToCartDto dto) : IRequest<int>;

    public class AddToCartHandler : IRequestHandler<AddToCartCommand, int>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserContextService _userContextService;
        private readonly IProductVariantRepository _productVariantRepository;
        public AddToCartHandler(ICartRepository cartRepository, IUserContextService userContextService, IProductVariantRepository productVariantRepository)
        {
            _cartRepository = cartRepository;
            _userContextService = userContextService;
            _productVariantRepository = productVariantRepository;
        }

        public async Task<int> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var customerId = data.CustomerId;

            var productVariant = await _productVariantRepository.GetProductVariantByIdAsync(data.ProductVariantId);

            if (productVariant == null)
                throw new Exception("Product variant not found.");

            var cart = await _cartRepository.GetCartByCustomerIdAsync(customerId);

            //Check if cart exists
            if (cart == null)
            {
                // Create a new cart
                var CartId = await _cartRepository.CreateCartAsync(customerId);

                // Add the item to the new cart
                var cartItem = new CartItem
                {
                    ProductVariantId = data.ProductVariantId,
                    CartId = CartId,
                    Quantity = 1,
                    UnitPrice = productVariant.Price,
                    TotalPrice = productVariant.Price,
                    AddedAt = DateTime.UtcNow
                };
                var cartItemId = await _cartRepository.AddCartItemAsync(cartItem);

                return cartItemId;
            }
            else
            {
                //Check if item already in cart
                var existingCartItem = cart.CartItems
                    .FirstOrDefault(ci => ci.ProductVariantId == data.ProductVariantId);

                //update quantity and price if item already exists
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += 1;
                    existingCartItem.TotalPrice = existingCartItem.Quantity * existingCartItem.UnitPrice;
                    await _cartRepository.UpdateCartItemAsync(existingCartItem);

                    return existingCartItem.CartItemId;
                }
                else
                {
                    //add new item in cart
                    var newCartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductVariantId = data.ProductVariantId,
                        Quantity = 1,
                        UnitPrice = productVariant.Price,
                        TotalPrice = productVariant.Price,
                        AddedAt = DateTime.UtcNow
                    };

                    var cartItemId = await _cartRepository.AddCartItemAsync(newCartItem);
                    return cartItemId;
                }
            }
        }
    }
}
