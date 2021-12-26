using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Helpers
{
    public static class CustomHelpers
    {
        public static string IsActiveController(this IHtmlHelper htmlHelper,
                                    string returnString,
                                    string[] controllers)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var routeControl = (string)routeData.Values["controller"];

            var returnActive = false;
            if (controllers.Any(routeControl.Contains))
            {
                returnActive = true;
            }
            return returnActive ? returnString : "";
        }

        public static string UrlAreaAction(this IHtmlHelper htmlHelper,
                                    string[] elements)
        {
            return "/" + String.Join("/", elements);
        }
    }
}
