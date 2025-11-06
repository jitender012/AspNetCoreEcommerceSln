using AutoMapper;
using eCommerce.Application.Features.WishlistFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WishlistFeature.Commands
{
    public record CreateWishlistCommand(WishlistSaveDto dto) : IRequest<int>;

    public class CreateWishlistCommandHandler : IRequestHandler<CreateWishlistCommand, int>
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public CreateWishlistCommandHandler(IWishlistRepository wishlistRepository, IUserContextService userContextService, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (userId == Guid.Empty)
                throw new UnauthorizedAccessException("User not found.");

            var wishlist = _mapper.Map<Wishlist>(request.dto);
            wishlist.CustomerId = userId;

            var id = await _wishlistRepository.InsertAsync(wishlist);
            return id;
        }

    }
}
