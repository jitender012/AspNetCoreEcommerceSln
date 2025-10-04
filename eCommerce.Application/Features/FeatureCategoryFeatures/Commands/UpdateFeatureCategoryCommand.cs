using AutoMapper;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Commands
{
    public record UpdateFeatureCategoryCommand(FeatureCategorySaveDto dto) : IRequest<bool>;

    public class UpdateFeatureCategoryHandler : IRequestHandler<UpdateFeatureCategoryCommand, bool>
    {
        private readonly IFeatureCategoryRepository _repository;
        private readonly IMapper _mapper;
        public UpdateFeatureCategoryHandler(IFeatureCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateFeatureCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var featureCategory = _mapper.Map<Domain.Entities.FeatureCategory>(data);
            var result = await _repository.ModifyAsync(featureCategory);
            return result;
        }
    }
}
