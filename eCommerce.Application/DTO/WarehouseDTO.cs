using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTO
{
    public class WarehouseDTO
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public Guid? UserId { get; set; }

        public Warehouse ToWarehouse()
        {
            return new Warehouse
            {
                City = this.City,
                State = this.State,
                PostalCode = this.PostalCode,
                UserId = this.UserId,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                WarehouseId = this.WarehouseId,
                Name = this.Name,
                Street = this.Street
            };
        }

        public static WarehouseDTO FromWarehouse(Warehouse store)
        {
            return new WarehouseDTO
            {
                WarehouseId = store.WarehouseId,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber,
                Email = store.Email,
                Street = store.Street,
                City = store.City,
                State = store.State,
                PostalCode = store.PostalCode,
                UserId = store.UserId
            };
        }

        public static List<WarehouseDTO> FromWarehouseList(IEnumerable<Warehouse> stores)
        {
            return stores.Select(s=> FromWarehouse(s)).ToList();
        }
    }
}
