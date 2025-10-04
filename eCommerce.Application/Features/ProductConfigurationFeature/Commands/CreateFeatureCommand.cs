using AutoMapper;
using eCommerce.Application.Features.ProductConfigurationFeature.DTOs;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
namespace eCommerce.Application.Features.ProductConfigurationFeature.Commands
{
    public record CreateFeatureCommand(FeatureSaveDTO dto) : IRequest<int>;

    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, int>
    {
        private readonly IProductFeatureRepository _productFeatureRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public CreateFeatureCommandHandler(IProductFeatureRepository productFeatureRepository, IUserContextService userContextService, IMapper mapper)
        {
            _productFeatureRepository = productFeatureRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var productFeature = _mapper.Map<ProductFeature>(data);
            productFeature.CreatedBy = (_userContextService.GetUserId()).ToString();
            if (data.FeatureOptions != null)
            {
                productFeature.FeatureOptions = data.FeatureOptions.Select(x => new FeatureOption
                {
                    CreatedBy = productFeature.CreatedBy,
                    ProductFeatureId = productFeature.ProductFeaturesId,
                    Value = x

                }).ToList();
            }

            var result = await _productFeatureRepository.InsertAsync(productFeature);
            return result;
        }
    }
}
