using eCommerce.Application.Features.ProductCategoryFeatures.Commands;
using eCommerce.Application.ServiceContracts;
using eCommerce.Domain.RepositoryContracts;
using MediatR;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IUserContextService _userContextService;
        public UpdateCategoryHandler(IProductCategoryRepository categoryRepository, IUserContextService userContextService)
        {
            _categoryRepository = categoryRepository;
            _userContextService = userContextService;
        }
        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = request.dto;
            var userId = _userContextService.GetUserId();
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(data.CategoryId);
            
            if (existingCategory == null)
            {
                //throw new KeyNotFoundException($"Category with ID {data.CategoryId} not found.");
                return false;
            }

            existingCategory.CategoryName = data.CategoryName;
            existingCategory.CategoryImage = data.CategoryImage;
            existingCategory.ParentCategoryId = data.ParentCategoryId;
            existingCategory.UpdatedBy = userId;
            existingCategory.UpdatedOn = DateTime.Now;
            existingCategory.IsActive = data.IsActive;
            
            await _categoryRepository.UpdateAsync(existingCategory);
            
            return true;
        }
    }
}
