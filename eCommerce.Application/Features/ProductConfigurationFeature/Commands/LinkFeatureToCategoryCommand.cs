using eCommerce.Application.Features.ProductConfigurationFeature.DTOs;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;

namespace eCommerce.Application.Features.ProductConfigurationFeature.Commands
{
    public record LinkFeatureToCategoryCommand(FeatureNCategoryIdsDto dto) : IRequest<bool>;

    public class LinkFeatureToCategoryHandler : IRequestHandler<LinkFeatureToCategoryCommand, bool>
    {
        private readonly IProductConfigurationRepository _productConfigurationRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public LinkFeatureToCategoryHandler(IProductConfigurationRepository productConfigurationRepository, IProductCategoryRepository categoryRepository)
        {
            _productConfigurationRepository = productConfigurationRepository;
            _productCategoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(LinkFeatureToCategoryCommand request, CancellationToken cancellationToken)
        {
            //To do: Add validation to check if CategoryId and FeatureId are valid

            var data = request.dto;
            var productCategoryIds = await _productCategoryRepository.GetAllDescendantsIds(data.CategoryId);
            productCategoryIds.Add(data.CategoryId); 

            var dataList = new List<ProductCategoryProductFeature>();

            foreach (var categoryId in productCategoryIds)
            {
                foreach (var featureId in data.FeatureIds)
                {
                    dataList.Add(new ProductCategoryProductFeature
                    {
                        ProductCategoryId = categoryId,
                        ProductFeatureId = featureId
                    });
                }
            }    
            
            var result = await _productConfigurationRepository.LinkFeatureToCategoryAsync(dataList);
            return result;
        }      
    }

}
