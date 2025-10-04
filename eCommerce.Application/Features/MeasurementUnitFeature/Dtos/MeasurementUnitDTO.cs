using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Features.MeasurementUnitFeature.Dtos
{
    public class MeasurementUnitDTO
    {
        public int MeasurementUnitId { get; set; }
        
        public string UnitName { get; set; } = null!;

        public string UnitSymbol { get; set; } = null!;

        public UnitType UnitType { get; set; }
    }
}
