using eCommerce.Domain.IdentityEntities;
using eCommerce.Web.Areas.Customer.Models.ProfileModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace eCommerce.Web.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize(Roles ="Customer")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            
            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                Id = user.Id,
                Name = user.UserName,
                EmailId = user.Email,
                Dob = user.DateOfBirth
            };

            return PartialView("_ProfilePartial", profileViewModel);
        }

        public IActionResult Index()
        {
            return View();
        }
      
    }
}
