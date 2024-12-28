using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Areas.Admin.Models.Role
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? NormalizedName { get; set; }

        public string? ConcurrencyStamp { get; set; }
    }
    public class AssignRoleViewModel
    {
        [Required]
        public string? UserEmail { get; set; }

        [Required]
        public string? RoleName { get; set; }
    }
}
