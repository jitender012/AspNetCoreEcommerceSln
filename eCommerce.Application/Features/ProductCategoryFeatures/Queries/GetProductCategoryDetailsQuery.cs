using AutoMapper;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Queries
{
    public record GetProductCategoryDetailsQuery(int id) : IRequest<ProductCategoryDetailsDto>;

    public class CategoryDetailsHandler : IRequestHandler<GetProductCategoryDetailsQuery, ProductCategoryDetailsDto>
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IFeatureCategoryRepository _featureCategoryRepository;
        private readonly IMapper _mapper;
        public CategoryDetailsHandler(IProductCategoryRepository categoryRepository,IFeatureCategoryRepository featureCategoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _featureCategoryRepository = featureCategoryRepository;
            _mapper = mapper;
        }
        public async Task<ProductCategoryDetailsDto> Handle(GetProductCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.id);

            var featureCaegories = await _featureCategoryRepository.FetchAllAsync();

            var featureCaegoriesDto = featureCaegories.Select(x => new FeatureCategoriesWithLinkStatusDto
            {
                FeatureCategoryId = x.FeatureCategoryId,
                FeatureCategoryName = x.Name,
                Features = x.ProductFeatures.Select(y => new FeatureDto
                {
                    FeatureId = y.ProductFeaturesId,
                    FeatureName = y.Name,
                    UnitSymbol = y.MeasurementUnit != null ? y.MeasurementUnit.UnitSymbol : null,
                    IsLinked = y.ProductCategoryProductFeatures.Any(link => link.ProductCategoryId == request.id)
                }).ToList(),
            }).ToList();

            var categoryDto = _mapper.Map<ProductCategoryDetailsDto>(category);
            categoryDto.FeatureCategories = featureCaegoriesDto;

            return categoryDto;
        }

    }
}
