using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.Features.BrandFeature.Queries;
using eCommerce.Domain.RepositoryContracts;
using MediatR;

namespace eCommerce.Application.Features.BrandFeature.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, List<BrandDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        
        public GetAllBrandsHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<List<BrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetAllAsync();
            return _mapper.Map<List<BrandDto>>(brands);
        }
    }
}
