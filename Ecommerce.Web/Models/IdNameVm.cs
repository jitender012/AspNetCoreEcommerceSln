namespace eCommerce.Web.Models
{
    public class IdNameVm<TId>
    {
        public required TId Id { get; set; }
        public required string Name { get; set; } = null!;
    }
}
