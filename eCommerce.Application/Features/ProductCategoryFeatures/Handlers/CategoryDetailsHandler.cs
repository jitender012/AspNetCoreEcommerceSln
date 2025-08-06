using AutoMapper;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using eCommerce.Application.Features.ProductCategoryFeatures.Queries;
using eCommerce.Domain.RepositoryContracts;
using MediatR;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Handlers
{    
    public class CategoryDetailsHandler : IRequestHandler<ProductCategoryDetailsQuery, ProductCategoryDetailsDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryDetailsHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<ProductCategoryDetailsDto> Handle(ProductCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.id);
            var categoryDto = _mapper.Map<ProductCategoryDetailsDto>(category);

            return categoryDto;
        }
    }
}
