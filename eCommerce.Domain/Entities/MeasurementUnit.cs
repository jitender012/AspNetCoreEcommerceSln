using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Entities
{
    public enum UnitType
    {
        Length,
        Weight,
        Volume,
        Area,
        Quantity,
        Digital,
        Time,
        Temperatre
    }

    public class MeasurementUnit
    {
        [Key]
        public int MeasurementUnitId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UnitName { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string UnitSymbol { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public UnitType UnitType { get; set; } 
    }
}
