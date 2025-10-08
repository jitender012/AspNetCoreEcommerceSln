using AutoMapper;
using eCommerce.Application.Features.ProductCategoryFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Queries
{
    public record GetProductCategoryListQuery : IRequest<List<ProductCategoryListDto>>;

    public class ProductCategoryListHandler : IRequestHandler<GetProductCategoryListQuery, List<ProductCategoryListDto>>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;
        public ProductCategoryListHandler(IProductCategoryRepository categoryRepository, IMapper mapper)
        {
            _productCategoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductCategoryListDto>> Handle(GetProductCategoryListQuery request, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryRepository.GetAllCategories();
            var productCategoryDto = _mapper.Map<List<ProductCategoryListDto>>(productCategory);
            return productCategoryDto;
        }
    }
}
