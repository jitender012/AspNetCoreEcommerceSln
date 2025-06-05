using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Application.DTO.ProductDTOs;
using eCommerce.Application.DTO;

namespace eCommerce.Application.ServiceContracts.ProductServiceContracts
{
    public interface IFeatureOptionService
    {
        //→ GetAllAsync 
        //→ GetByIdAsync 
        //→ AddAsync 
        //→ UpdateAsync 
        //→ DeleteAsync

        #region Basic CRUD
        Task<List<FeatureOptionDTO>> GetAllAsync();
        Task<FeatureOptionDTO> GetByIdAsync(int id);
        Task<int> AddAsync(FeatureOptionDTO data);
        Task<bool> UpdateAsync(FeatureOptionDTO data);
        Task<bool> DeleteAsync(int id);
        #endregion                     

    }
}
