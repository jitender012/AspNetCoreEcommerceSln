using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Domain.CustomException;
using eCommerce.Domain.RepositoryContracts;
using MediatR;
using System.Data.Common;

namespace eCommerce.Application.Features.BrandFeature.Queries
{
    public record GetBrandByIdQuery(Guid BrandId) : IRequest<BrandDetailsDto>;

    public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdQuery, BrandDetailsDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetBrandByIdHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<BrandDetailsDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandById(request.BrandId) 
                ?? throw new NotFoundException("Brand not found");

            try
            {                
                return _mapper.Map<BrandDetailsDto>(brand); ;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get brand details.", ex);
            }
        }
    }
}
