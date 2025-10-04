using AutoMapper;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Queries
{
    public record GetFeatureCategoriesQuery : IRequest<List<FeatureCategoryListDTO>>;

    public class GetFeatureCategoriesQueryHandler : IRequestHandler<GetFeatureCategoriesQuery, List<FeatureCategoryListDTO>>
    {
        private readonly IFeatureCategoryRepository _featureCategoryRepository;
        private readonly IMapper _mapper;
        public GetFeatureCategoriesQueryHandler(IFeatureCategoryRepository featureCategoryRepository, IMapper mapper)
        {            
            _featureCategoryRepository = featureCategoryRepository;
            _mapper = mapper;
        }
        public async Task<List<FeatureCategoryListDTO>> Handle(GetFeatureCategoriesQuery request, CancellationToken cancellationToken)
        {
            var featureCategories = await _featureCategoryRepository.FetchAllAsync();
            var featureCategoryListDTOs = _mapper.Map<List<FeatureCategoryListDTO>>(featureCategories);

            return featureCategoryListDTOs;
        }
    }
}
