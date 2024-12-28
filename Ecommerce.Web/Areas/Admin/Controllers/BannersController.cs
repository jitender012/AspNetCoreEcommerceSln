using eCommerce.Web.Areas.Admin.Models.Banner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BannersController : Controller
    {
        public BannersController()
        {
                
        }
        // GET: Administration/ManageBanners
        public ActionResult BannersList()
        {
            return View();
        }
        public ActionResult BannerDetails(int id)
        {
            return View();
        }

        public ActionResult CreateBanner()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBanner(CreateBannerViewModel model)
        {
            return View();
        }

        public ActionResult EditBanner()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditBanner(EditBannerViewModel model)
        {
            return View();
        }
    }
}