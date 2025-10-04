using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Common.Dtos
{
    public class IdNameDto<TId>
    {
        public required TId Id { get; set; } 
        public required string Name { get; set; } = null!;
    }
}
