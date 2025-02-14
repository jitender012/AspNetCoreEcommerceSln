using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.DTO.VendorDTOs;

namespace eCommerce.Application.ServiceContracts.ProductServiceContracts
{
    public interface IProductService
    {
        Task<Guid> AddAsync(ProductDTO data);
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetProductById(Guid id);
        Task<bool> UpdateProduct(ProductDTO data);
        Task<bool> DeleteAsync(Guid id);
        Task<List<SellerProductDTO>> GetProductsBySeller();
        Task<List<ProductDTO>> GetPtoductsByBrand(Guid id);
    }
}
