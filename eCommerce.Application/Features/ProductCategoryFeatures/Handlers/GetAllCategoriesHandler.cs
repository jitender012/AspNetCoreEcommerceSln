using AutoMapper;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using eCommerce.Application.Features.ProductCategoryFeatures.Queries;
using eCommerce.Domain.RepositoryContracts;
using MediatR;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, List<ProductCategoryDto>>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoriesHandler(IProductCategoryRepository categoryRepository, IMapper mapper)
        {
            _productCategoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductCategoryDto>> Handle(GetAllCategories request, CancellationToken cancellationToken)
        {
            var categories = await _productCategoryRepository.GetAllCategories();
            var categoriesDto = _mapper.Map<List<ProductCategoryDto>>(categories);

            return categoriesDto;
        }
    }
}
