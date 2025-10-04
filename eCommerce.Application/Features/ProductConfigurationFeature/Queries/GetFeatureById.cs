using AutoMapper;
using eCommerce.Application.Features.ProductConfigurationFeature.DTOs;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;

namespace eCommerce.Application.Features.ProductConfigurationFeature.Queries
{
    public record GetFeatureById(int FeatureId) : IRequest<FeatureDetailsDTO>;

    public class GetFeatureByIdHandler : IRequestHandler<GetFeatureById, FeatureDetailsDTO>
    {
        private readonly IProductFeatureRepository _productFeatureRepository;
        private readonly IMapper _mapper;
        public GetFeatureByIdHandler(IProductFeatureRepository productFeatureRepository, IMapper mapper)
        {

            _productFeatureRepository = productFeatureRepository;
            _mapper = mapper;
        }      
        public async Task<FeatureDetailsDTO> Handle(GetFeatureById request, CancellationToken cancellationToken)
        {
            if (request.FeatureId <= 0)
                throw new ArgumentNullException("ID must be greater than zero");
            var feature = await _productFeatureRepository.FetchByIdAsync(request.FeatureId);

            var featureDTO = _mapper.Map<FeatureDetailsDTO>(feature);
            return featureDTO;
        }
    }

}
