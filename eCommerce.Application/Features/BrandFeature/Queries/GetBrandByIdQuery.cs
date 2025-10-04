using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Domain.RepositoryContracts;
using MediatR;

namespace eCommerce.Application.Features.BrandFeature.Queries
{
    public record GetBrandByIdQuery(Guid BrandId) : IRequest<BrandDetailsDTO>;

    public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdQuery, BrandDetailsDTO>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetBrandByIdHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        // Implementation will be added later
        public async Task<BrandDetailsDTO> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand =await  _brandRepository.GetByIdAsync(request.BrandId);
            var brandDto = _mapper.Map<BrandDetailsDTO>(brand);

            return brandDto;
        }
    }
}
