using eCommerce.Application.Features.BrandFeature.Commands;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using MediatR;

namespace eCommerce.Application.Features.BrandFeature.Handlers
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Guid>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUserContextService _userContextService;
        public CreateBrandHandler(IBrandRepository brandRepository, IUserContextService userContextService)
        {
            _brandRepository = brandRepository;
            _userContextService = userContextService;
        }

        public async Task<Guid> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var user = _userContextService.GetUserId();
            Brand brand = request.Dto.ToBrand();
            brand.CreatedBy = _userContextService.GetUserId();
            var result = await _brandRepository.InsertAsync(brand);            

            return result.BrandId;
        }
    }
}
