using AutoMapper;
using eCommerce.Application.Features.BrandFeature.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using MediatR;

namespace eCommerce.Application.Features.BrandFeature.Commands
{
    public record CreateBrandCommand(BrandSaveDTO Dto) : IRequest<Guid>;

    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Guid>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public CreateBrandHandler(IBrandRepository brandRepository, IUserContextService userContextService, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var user = _userContextService.GetUserId();
            
            var brand = _mapper.Map<Brand>(request.Dto);
            brand.BrandId = Guid.NewGuid();
            brand.CreatedBy = _userContextService.GetUserId();
            var result = await _brandRepository.InsertAsync(brand);

            return result.BrandId;
        }
    }
}
