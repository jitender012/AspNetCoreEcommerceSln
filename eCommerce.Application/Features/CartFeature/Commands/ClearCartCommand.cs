using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.CartFeature.Commands
{
    public record ClearCartCommand(Guid cartId) : IRequest<bool>;

    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserContextService _userContextService;
        public ClearCartCommandHandler(ICartRepository cartRepository, IUserContextService userContextService)
        {
            _cartRepository = cartRepository;
            _userContextService = userContextService;
        }

        public async Task<bool> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();

            var cart = await _cartRepository.GetCartByCustomerIdAsync(userId);
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            await _cartRepository.ClearCartAsync(userId);
            return true;
        }
    }
}

