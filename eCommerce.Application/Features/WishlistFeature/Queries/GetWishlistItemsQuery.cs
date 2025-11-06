using AutoMapper;
using eCommerce.Application.Features.WishlistFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.WishlistFeature.Queries
{
    public record GetWishlistItemsQuery : IRequest<List<WishlistItemDto>>;

    public class GetWishlistItemsQueryHandler : IRequestHandler<GetWishlistItemsQuery, List<WishlistItemDto>>
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public GetWishlistItemsQueryHandler(IWishlistRepository wishlistRepository, IUserContextService userContextService, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<List<WishlistItemDto>> Handle(GetWishlistItemsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            if (userId != Guid.Empty)
            {
                var wishlistItems = await _wishlistRepository.FetchByCustomerIdAsync(userId);
                if (wishlistItems != null && wishlistItems.Count > 0)
                {
                    var wishlistItemDtos = _mapper.Map<List<WishlistItemDto>>(wishlistItems);
                    return wishlistItemDtos;
                }
            }

            //empty list
            return [];
        }
    }

}
