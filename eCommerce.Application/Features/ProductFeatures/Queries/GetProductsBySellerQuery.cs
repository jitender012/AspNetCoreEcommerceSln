using AutoMapper;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;

namespace eCommerce.Application.Features.ProductFeatures.Queries
{
    public record GetProductsBySellerQuery : IRequest<List<ProductListDto>>;

    public class GetProductsBySellerHandler : IRequestHandler<GetProductsBySellerQuery, List<ProductListDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public GetProductsBySellerHandler(IProductRepository productRepository, IUserContextService userContextService, IMapper mapper)
        {
            _productRepository = productRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public async Task<List<ProductListDto>> Handle(GetProductsBySellerQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetUserId();
            var products = await _productRepository.FetchBySellerIdAsync(userId);
            var productListDto = _mapper.Map<List<ProductListDto>>(products);

            return productListDto;
        }

    }
}
