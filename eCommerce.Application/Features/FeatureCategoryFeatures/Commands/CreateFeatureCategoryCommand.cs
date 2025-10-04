using AutoMapper;
using eCommerce.Application.Features.FeatureCategoryFeatures.Dtos;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.FeatureCategoryFeatures.Commands
{
    public record CreateFeatureCategoryCommand(FeatureCategorySaveDto dto) : IRequest<int>;

    public class CreateFeatureCategoryHandler : IRequestHandler<CreateFeatureCategoryCommand, int>
    {
        private readonly IFeatureCategoryRepository _featureCategoryRepository;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public CreateFeatureCategoryHandler(IFeatureCategoryRepository featureCategoryRepository, IUserContextService userContextService, IMapper mapper)
        {
            _featureCategoryRepository = featureCategoryRepository;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateFeatureCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var featureCategory = _mapper.Map<FeatureCategory>(data);
            featureCategory.CreatedBy = _userContextService.GetUserId().ToString();
            var result = await _featureCategoryRepository.InsertAsync(featureCategory);
            return result;
        }
    }
}
