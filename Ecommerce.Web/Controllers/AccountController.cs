using eCommerce.Domain.IdentityEntities;
using eCommerce.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        #region RegiserActions
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = data.Email,
                UserName = data.Name
            };

            var result = await _userManager.CreateAsync(applicationUser, data.Password);
            var roleResult = await _userManager.AddToRoleAsync(applicationUser, "Customer");

            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                // deleting the user if the role assignment fails
                await _userManager.DeleteAsync(applicationUser);
                return View(data);
            }

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(applicationUser, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return View(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsSeller(RegisterViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = data.Email,
                UserName = data.Name
            };

            var result = await _userManager.CreateAsync(applicationUser, data.Password);
            var roleResult = await _userManager.AddToRoleAsync(applicationUser, "Seller");

            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                // deleting the user if the role assignment fails
                await _userManager.DeleteAsync(applicationUser);
                return View(data);
            }

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(applicationUser, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return View(data);
            }
        }
        #endregion

        #region LoginActions
        public IActionResult Login()
        {
            return View();
        }

        //Login post method
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            ApplicationUser? applicationUser = await _userManager.FindByEmailAsync(data.Email);
            if (applicationUser == null)
            {
                ModelState.AddModelError(nameof(data.Email), "Email is not registered.");
                return View(data);
            }
            var result = await _signInManager.PasswordSignInAsync(applicationUser, data.Password, true, false);
            if (result.Succeeded)
            {
                string area = applicationUser != null && await _userManager.IsInRoleAsync(applicationUser, "Admin") ? "Admin" :
                                          applicationUser != null && await _userManager.IsInRoleAsync(applicationUser, "Seller") ? "Seller" : "";

                if (!string.IsNullOrEmpty(area))
                {
                    return RedirectToAction("Index", "Home", new { area });
                }

                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Account is locked. Please try again later.");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(data);
        }
        #endregion
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
