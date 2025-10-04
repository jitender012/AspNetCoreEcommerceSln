using eCommerce.Domain.Entities;
using eCommerce.Domain.RepositoryContracts.Products;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories.Products
{
    public class MeasurementUnitRepository : IMeasurementUnitRepository
    {
        private readonly eCommerceDbContext _context;
        public MeasurementUnitRepository(eCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<MeasurementUnit>> GetAllMeasurementUnitsAsync()
        {
            return await _context.MeasurementUnits.ToListAsync();
        }

        public async Task<MeasurementUnit?> GetMeasurementUnitByIdAsync(int id)
        {
            var unit = await _context.MeasurementUnits.FindAsync(id);
            return unit;
        }

        public async Task<MeasurementUnit> AddMeasurementUnitAsync(MeasurementUnit unit)
        {
            await _context.MeasurementUnits.AddAsync(unit);
            await _context.SaveChangesAsync();
            return unit;
        }
        
        public async Task<MeasurementUnit> UpdateMeasurementUnitAsync(MeasurementUnit unit)
        {
           await _context.MeasurementUnits.FirstOrDefaultAsync(x=>x.MeasurementUnitId == unit.MeasurementUnitId);
            if (unit == null)
            {
                throw new ArgumentException("Measurement unit not found.", nameof(unit));
            }
            _context.MeasurementUnits.Update(unit);
            await _context.SaveChangesAsync();
            return unit;
        }

        public async Task<bool> DeleteMeasurementUnitAsync(int id)
        {
            var record = await _context.MeasurementUnits.FirstOrDefaultAsync(x => x.MeasurementUnitId == id);
            if (record == null)
            {
                return false;
                throw new ArgumentException("Measurement unit not found.", nameof(id));
            }
            _context.MeasurementUnits.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
