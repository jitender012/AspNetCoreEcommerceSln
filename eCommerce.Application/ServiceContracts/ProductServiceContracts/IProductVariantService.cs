using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts.ProductServiceContracts
{
    public interface IProductVariantService
    {
        Task<List<ProductVariant>> GetProductVariantsAsync();
        Task<Guid> AddProductVariantAsync(ProductVariantDTO productVariant);
        Task<bool> UpdateProductVariantAsync(ProductVariantDTO productVariant);
        Task<bool> DeleteProductVariantAsync(Guid productVariantId);
    }
}
