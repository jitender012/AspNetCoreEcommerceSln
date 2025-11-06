using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;

namespace eCommerce.Application.Features.CartFeature.Commands
{
    public record RemoveFromCartCommand(int cartItemId) : IRequest<bool>;

    public class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserContextService _userContextService;
        public RemoveFromCartHandler(ICartRepository cartRepository, IUserContextService userContextService)
        {
            _cartRepository = cartRepository;
            _userContextService = userContextService;
        }

        public async Task<bool> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();

            //Fetch user cart
            var cart = await _cartRepository.GetCartByCustomerIdAsync(userId);
            if (cart == null)
            {
                throw new ArgumentException();
            }

            //check if cart item exists
            if (cart.CartItems.Any(x => x.CartItemId == request.cartItemId))
            {
                await _cartRepository.RemoveCartItemAsync(request.cartItemId);
                return true;
            }
            return false;
        }
    }
}
