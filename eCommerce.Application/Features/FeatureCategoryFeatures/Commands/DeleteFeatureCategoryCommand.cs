using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Commands
{
    public record DeleteFeatureCategoryCommand(int id) : IRequest<bool>;

    public class DeleteFeatureCategoryHandler : IRequestHandler<DeleteFeatureCategoryCommand, bool>
    {
        private readonly IFeatureCategoryRepository _featureCategoryRepository;
        private readonly IMapper _mapper;

        public DeleteFeatureCategoryHandler(IFeatureCategoryRepository featureCategoryRepository, IMapper mapper)
        {
            _featureCategoryRepository = featureCategoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteFeatureCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.id < 0)
                return false;

            var result = await _featureCategoryRepository.RemoveAsync(request.id);

            return result;
        }
    }
}
