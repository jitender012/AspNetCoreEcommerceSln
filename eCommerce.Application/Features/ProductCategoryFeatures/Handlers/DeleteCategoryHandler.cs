using eCommerce.Application.Features.ProductCategoryFeatures.Commands;
using eCommerce.Domain.RepositoryContracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.ProductCategoryFeatures.Handlers
{
    public class DeleteCategoryHandler  : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IProductCategoryRepository _categoryRepository;
        public DeleteCategoryHandler(IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var isExistingCategory = await _categoryRepository.GetCategoryByIdAsync(request.id);
            if (isExistingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {request.id} not found.");
            }
            try
            {
                await _categoryRepository.DeleteAsync(c => c.ProductCategoryId == request.id);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception($"An error occurred while deleting the category: {ex.Message}", ex);
            }
        }
    }
}
