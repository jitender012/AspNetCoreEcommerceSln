using eCommerce.Application.Common.Dtos;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts.Common;
using MediatR;

namespace eCommerce.Application.Features.ProductFeatures.Queries
{
    public record GetByProductCategoryIdQuery(int id) : IRequest<List<FeatureCategoryWithFeaturesDto>>;

    public class GetByProductCategoryIdQueryHandler : IRequestHandler<GetByProductCategoryIdQuery, List<FeatureCategoryWithFeaturesDto>>
    {
        private readonly IProductFeatureRepository _productFeatureRepository;
        public GetByProductCategoryIdQueryHandler(IProductFeatureRepository productFeatureRepository)
        {
            _productFeatureRepository = productFeatureRepository;
        }
        public async Task<List<FeatureCategoryWithFeaturesDto>> Handle(GetByProductCategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var features = await _productFeatureRepository.GetProductFeaturesByCategoryIdAsync(request.id);

                var groupedFeatures = features.GroupBy(f => f.FeatureCategory!.Name)
                     .Select(g => new FeatureCategoryWithFeaturesDto
                     {
                         FeatureCategoryName = g.Key,
                         ProductFeatures = g.Select(f => new FeaturesDto
                         {
                             ProductFeatureId = f.ProductFeaturesId,
                             Name = f.Name,
                             MeasurementUnit = f.MeasurementUnit != null ? f.MeasurementUnit.UnitSymbol : null,
                             InputType = f.InputType,
                             FeatureOptions = f.FeatureOptions.Select(fo => new IdNameDto<int>
                             {
                                 Id = fo.FeatureOptionId,
                                 Name = fo.Value
                             }).ToList()
                         }).ToList()
                     }).ToList();

                return groupedFeatures;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
