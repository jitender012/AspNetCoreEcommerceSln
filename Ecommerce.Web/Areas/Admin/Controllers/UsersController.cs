
//using eCommerce.Core.Domain.IdentityEntities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace eCommerce.Web.Areas.Admin.Controllers
//{
//    [Authorize(Roles = "Admin, Employee")]
//    public class UsersController : Controller
//    {
//        private UserManager<ApplicationUser> _userManager;
     
//        public UsersController(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }       

//        // GET: Administration/UserList
//        public ActionResult UserList()
//        {            
//            var users = _userManager.Users.ToList().
//                Select(x => new UsersListViewModel()
//            {
//                Email = x.Email,
//                UserName = x.UserName,
//                Id = x.Id,
//                Role= UserManager.GetRoles(x.Id).ToList()
//            }).ToList(); 

//            return View(users);
//        }

//        [HttpPost]
//        public ActionResult UserList(string Search)
//        {
//            var search = Search.ToLower();
//            var users = UserManager.Users.ToList().Where(m=>m.UserName.ToLower().Contains(search)).
//                Select(x => new UsersListViewModel()
//                {
//                    Email = x.Email,
//                    UserName = x.UserName,
//                    Id = x.Id,
//                    Role = UserManager.GetRoles(x.Id).ToList()
//                }).ToList();

//            return View(users);
//        }
//    }
//}