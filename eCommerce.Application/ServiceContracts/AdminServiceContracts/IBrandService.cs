using eCommerce.Application.DTO;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts.AdminServiceContracts
{
    public interface IBrandService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Brands</returns>
        Task<List<CreateBrandDTO>> GetAllBrands();
        Task<CreateBrandDTO> GetBrandById(Guid id);

        /// <summary>
        /// Creates new brand
        /// </summary>
        /// <param name="data">Expects CreateBrandDTO object</param>
        /// <returns>Id of created brand</returns>
        Task<Guid> AddBrand(CreateBrandDTO data);
        bool UpdateBrand(CreateBrandDTO data);
        bool DeleteBrand(Guid id);
    }
}
