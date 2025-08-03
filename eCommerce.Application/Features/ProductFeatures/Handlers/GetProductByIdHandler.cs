using AutoMapper;
using eCommerce.Application.Features.ProductFeatures.Dtos;
using eCommerce.Application.Features.ProductFeatures.Queries;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductFeatures.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserContextService _userContextService;
        private readonly ILogger<GetProductByIdHandler> _logger;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(
            IProductRepository productRepository,
            IUserContextService userContextService,
            ILogger<GetProductByIdHandler> logger,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _userContextService = userContextService;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FetchByIdAsync(request.ProductId);
            if (product == null)
            {
                _logger.LogError("Product Not found.");
            }

            var productVm = _mapper.Map<ProductDto>(product);
            return productVm;
        }
    }
}
