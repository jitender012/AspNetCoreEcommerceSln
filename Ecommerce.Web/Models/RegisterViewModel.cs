using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name can not be blanked")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email can not be blanked")]
        [EmailAddress(ErrorMessage = "Email should be in a proper format.")]
        public required string Email { get; set; }    

        [Required(ErrorMessage = "Password can not be blanked")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password can not be blanked")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm password do not match.")]
        public required string ConfirmPassword { get; set; }

    }
}
