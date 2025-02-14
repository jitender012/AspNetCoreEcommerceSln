using eCommerce.Domain.Entities;
using eCommerce.Domain.IdentityEntities;
using eCommerce.Web.Areas.Admin.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleManagerController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();

            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Role
        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ModelState.AddModelError("", "Role name cannot be empty.");
                return View();
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                ModelState.AddModelError("", "Role already exists.");
                return View();
            }

            var result = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        // GET: Assign Role to User
        public IActionResult Assign()
        {
            return View(new AssignRoleViewModel());
        }

        // POST: Assign Role to User
        [HttpPost]
        public async Task<IActionResult> Assign(AssignRoleViewModel model)
        {
            ApplicationUser? user = new();

            if (model.UserEmail != null)
            {
                user = await _userManager.FindByEmailAsync(model.UserEmail);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }
            IdentityResult? result = new();
            if (model.RoleName != null)
            {
                if (await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    ModelState.AddModelError("", "Role does not exist.");
                    return View(model);
                }
                result = await _userManager.AddToRoleAsync(user, model.RoleName);
            }

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}
