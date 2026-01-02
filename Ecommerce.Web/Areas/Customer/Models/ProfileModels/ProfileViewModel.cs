namespace eCommerce.Web.Areas.Customer.Models.ProfileModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime? Dob { get; set; }
    }
}
