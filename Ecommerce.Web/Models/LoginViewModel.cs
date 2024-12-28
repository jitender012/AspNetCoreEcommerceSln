using System.ComponentModel.DataAnnotations;

namespace eCommerce.Web.Models
{
    public class LoginViewModel
    {
       
        [Required(ErrorMessage = "Email can not be blanked")]
        [EmailAddress(ErrorMessage = "Email should be in a proper format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password can not be blanked")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

    }
}
