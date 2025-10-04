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
    public record GetBrandForEditQuery(Guid BrandId) : IRequest<BrandSaveDTO>;

    // Handler
    public class GetBrandForEditQueryHandler : IRequestHandler<GetBrandForEditQuery, BrandSaveDTO>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetBrandForEditQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandSaveDTO> Handle(GetBrandForEditQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(request.BrandId);
            if (brand == null) throw new KeyNotFoundException("Brand not found");

            return _mapper.Map<BrandSaveDTO>(brand);
        }
    }

}

