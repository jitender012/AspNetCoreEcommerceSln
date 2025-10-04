using AutoMapper;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Queries
{
    public record GetFeatureCategoryByIdQuery(int id) : IRequest<FeatureCategoryDetailsDTO>;

    public class GetFeatureCategoryByIdQueryHandler : IRequestHandler<GetFeatureCategoryByIdQuery, FeatureCategoryDetailsDTO>
    {
        private readonly IFeatureCategoryRepository _featureCategoryRepository;
        private readonly IMapper _mapper;
        public GetFeatureCategoryByIdQueryHandler(IFeatureCategoryRepository featureCategoryRepository, IMapper mapper)
        {
            _featureCategoryRepository = featureCategoryRepository;
            _mapper = mapper;
        }
        public async Task<FeatureCategoryDetailsDTO> Handle(GetFeatureCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var featureCategory = await _featureCategoryRepository.FindByIdAsync(request.id);
            var featureCategoryDetailsDTO = _mapper.Map<FeatureCategoryDetailsDTO>(featureCategory);
            return featureCategoryDetailsDTO;
        }
    }
}
