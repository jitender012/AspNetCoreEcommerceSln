using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts.Products
{
    public interface IMeasurementUnitRepository
    {
        Task<List<MeasurementUnit>> GetAllMeasurementUnitsAsync();
        Task<MeasurementUnit?> GetMeasurementUnitByIdAsync(int id);
        Task<MeasurementUnit> AddMeasurementUnitAsync(MeasurementUnit unit);
        Task<MeasurementUnit> UpdateMeasurementUnitAsync(MeasurementUnit unit);
        Task<bool> DeleteMeasurementUnitAsync(int id);
    }
}
