using eCommerce.Application.Features.ProductCategoryFeatures.Commands;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts;
using MediatR;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserContextService _userContextService;
        public CreateCategoryHandler(ICategoryRepository categoryRepository, IUserContextService userContextService)
        {
            _categoryRepository = categoryRepository;
            _userContextService = userContextService;
        }
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var userId = _userContextService.GetUserId();

            ProductCategory productCategory = new()
            {                
                CategoryName = data.CategoryName,
                CategoryImage = data.CategoryImage,
                ParentCategoryId = data.ParentCategoryId,
                CreatedOn = DateTime.Now,
                CreatedBy = userId,
                IsDeleted = false
            };
            var result = await _categoryRepository.InsertAsync(productCategory);

            return result.ProductCategoryId;
        }
    }
}
