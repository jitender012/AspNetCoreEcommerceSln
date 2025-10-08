using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Queries
{
    public record GetFeatureCategoriesWithLinkStatusQuery(int ProductCategoryId) : IRequest<List<FeatureCategoriesWithLinkStatusDto>>;

    public class GetFeatureCategoriesWithLinkStatusHandler : IRequestHandler<GetFeatureCategoriesWithLinkStatusQuery, List<FeatureCategoriesWithLinkStatusDto>>
    {
        private readonly IFeatureCategoryRepository _featureCategoryRepository;

        public GetFeatureCategoriesWithLinkStatusHandler(IFeatureCategoryRepository featureCategoryRepository)
        {
            _featureCategoryRepository = featureCategoryRepository;
        }
        public async Task<List<FeatureCategoriesWithLinkStatusDto>> Handle(GetFeatureCategoriesWithLinkStatusQuery request, CancellationToken cancellationToken)
        {
            var featureCaegories = await _featureCategoryRepository.FetchAllAsync();

            var featureCaegoriesDto = featureCaegories.Select(x=> new FeatureCategoriesWithLinkStatusDto
            {
                FeatureCategoryId = x.FeatureCategoryId,
                FeatureCategoryName = x.Name,
                Features = x.ProductFeatures.Select(y=> new FeatureDto
                {
                    FeatureId = y.ProductFeaturesId,
                    FeatureName = y.Name,
                    IsLinked = y.ProductCategoryProductFeatures.Any(link=> link.ProductCategoryId == request.ProductCategoryId) 
                }).ToList(),
            }).ToList();            

            return featureCaegoriesDto;
        }
    }
}
