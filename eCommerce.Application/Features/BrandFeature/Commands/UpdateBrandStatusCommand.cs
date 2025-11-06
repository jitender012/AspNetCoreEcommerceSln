using eCommerce.Domain.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.BrandFeature.Commands
{
    public record UpdateBrandStatusCommand(Guid brandId) : IRequest<bool>;
    public class UpdateBrandStatusCommandHandler : IRequestHandler<UpdateBrandStatusCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;
        public UpdateBrandStatusCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<bool> Handle(UpdateBrandStatusCommand request, CancellationToken cancellationToken)
        {            
            try
            {
                await _brandRepository.UpdateStatusAsync(request.brandId);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
