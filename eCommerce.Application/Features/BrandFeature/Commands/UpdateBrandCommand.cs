using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Commands
{
    public record UpdateBrandCommand(BrandSaveDTO dto) : IRequest<bool>;   

    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public UpdateBrandHandler(IBrandRepository brandRepository, IUserContextService userContextService, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brandDto = request.dto;
            var existingBrand = await _brandRepository.SingleOrDefaultAsync(x => x.BrandId == brandDto.BrandId);
            if (existingBrand == null) return false;
            
            var brand =_mapper.Map<Brand>(brandDto);

            brand.UpdatedBy = _userContextService.GetUserId();            
            brand.UpdatedAt = DateTime.UtcNow;

            await _brandRepository.UpdateAsync(brand);
            return true;
        }
    }
}
