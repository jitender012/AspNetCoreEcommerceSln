using AutoMapper;
using eCommerce.Application.Features.ProductConfigurationFeature.DTOs;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;

namespace eCommerce.Application.Features.ProductConfigurationFeature.Queries
{
    public record GetAllFeaturesQuery : IRequest<List<FeatureListDTO>>;

    public class GetAllFeaturesQueryHandler : IRequestHandler<GetAllFeaturesQuery, List<FeatureListDTO>>
    {
        private readonly IProductFeatureRepository _productFeatureRepository;
        private readonly IMapper _mapper;
        public GetAllFeaturesQueryHandler(IProductFeatureRepository productFeatureRepository, IMapper mapper)
        {
            _productFeatureRepository = productFeatureRepository;
            _mapper = mapper;
        }
        public async Task<List<FeatureListDTO>> Handle(GetAllFeaturesQuery request, CancellationToken cancellationToken)
        {
            var features = await _productFeatureRepository.FetchAllAsync();
            var featureListDTO = _mapper.Map<List<FeatureListDTO>>(features);
            return featureListDTO;
        }
    }
}
