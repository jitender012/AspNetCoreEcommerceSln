using eCommerce.Web.HelperClasses;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.HelperMethods
{
    public class BreadcrumbHelper
    {
        public static List<BreadcrumbItem> GetBreadcrumbs(ActionContext context)
        {
            var breadcrumbs = new List<BreadcrumbItem>();
            breadcrumbs.Add(new BreadcrumbItem { Title = "Home", Url = "/" });

            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();
            var id = context.RouteData.Values["id"]?.ToString();

            switch (controller)
            {
                case "Product":
                    if (action == "Index")
                    {
                        breadcrumbs.Add(new BreadcrumbItem { Title = "Products", Url = "/Product/Index" });
                    }
                    else if (action == "Details" && id != null)
                    {
                        breadcrumbs.Add(new BreadcrumbItem { Title = "Product Details", Url = $"/Product/Details/{id}" });
                    }
                    break;

                case "Category":
                    if (action == "Index")
                    {
                        breadcrumbs.Add(new BreadcrumbItem { Title = "Categories", Url = "/Category/Index" });
                    }
                    break;
            }

            return breadcrumbs;
        }

    }
}
