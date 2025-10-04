using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Domain.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Queries
{
    public record GetAllBrandsQuery : IRequest<List<BrandListDTO>>;
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, List<BrandListDTO>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetAllBrandsHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<List<BrandListDTO>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetAllBrands();
            var brandDtos = _mapper.Map<List<BrandListDTO>>(brands);
            return brandDtos;
        }
    }
}
