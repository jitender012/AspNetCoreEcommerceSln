using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WishlistFeature.Commands
{
    public record DeleteWishlistCommand(int id) : IRequest<bool>;

    public class DeleteWishlistCommandHandler : IRequestHandler<DeleteWishlistCommand, bool>
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IUserContextService _userContextService;
        public DeleteWishlistCommandHandler(IWishlistRepository wishlistRepository, IUserContextService userContextService)
        {
            _wishlistRepository = wishlistRepository;
            _userContextService = userContextService;
        }
        public async Task<bool> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            var result = await _wishlistRepository.RemoveAsync(request.id, userId);
            return result;
        }
    }
}
