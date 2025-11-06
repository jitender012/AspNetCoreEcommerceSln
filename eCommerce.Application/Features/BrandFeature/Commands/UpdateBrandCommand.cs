using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Commands
{
    public record UpdateBrandCommand(BrandSaveDto dto) : IRequest<bool>;

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
            var brandSaveDto = request.dto;
            try
            {
                var brand = _mapper.Map<Brand>(brandSaveDto);
                brand.UpdatedBy = _userContextService.GetUserId();
                brand.UpdatedAt = DateTime.UtcNow;

                await _brandRepository.UpdateAsync(brand);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database update failed.", ex);
            }
        }
    }
}
