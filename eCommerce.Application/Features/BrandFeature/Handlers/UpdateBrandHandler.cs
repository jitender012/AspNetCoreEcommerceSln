using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Handlers
{
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUserContextService _userContextService;
        public UpdateBrandHandler(IBrandRepository brandRepository, IUserContextService userContextService)
        {
            _brandRepository = brandRepository;
            _userContextService = userContextService;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {            
            var brand = await _brandRepository.SingleOrDefaultAsync(x => x.BrandId == request.BrandId);
            if (brand == null) return false;

            brand.UpdatedBy = _userContextService.GetUserId();
            brand.BrandDescription = request.BrandDescription;
            brand.BrandImage = request.BrandImage;
            brand.BrandName = request.BrandName;
            brand.UpdatedAt = DateTime.UtcNow;

            await _brandRepository.UpdateAsync(brand);

            return true;
        }
      
    }
}
