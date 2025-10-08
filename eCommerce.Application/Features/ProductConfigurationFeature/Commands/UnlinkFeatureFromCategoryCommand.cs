using eCommerce.Application.Features.ProductConfigurationFeature.DTOs;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductConfigurationFeature.Commands
{
   public record UnlinkFeatureFromCategoryCommand(FeatureNCategoryIdsDto dto) : IRequest<bool>;

    public class UnlinkFeatureFromCategoryHandler : IRequestHandler<UnlinkFeatureFromCategoryCommand, bool>
    {
        private readonly IProductConfigurationRepository _productConfigurationRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public UnlinkFeatureFromCategoryHandler(IProductConfigurationRepository productConfigurationRepository, IProductCategoryRepository productCategoryRepository)
        {
            _productConfigurationRepository = productConfigurationRepository;
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<bool> Handle(UnlinkFeatureFromCategoryCommand request, CancellationToken cancellationToken)
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

            var result = await _productConfigurationRepository.UnlinkFeatureFromCategoryAsync(dataList);
            return result;
        }
    }
}
