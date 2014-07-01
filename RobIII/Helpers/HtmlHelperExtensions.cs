using System;
using System.Web.Mvc;

namespace RobIII.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string ActivePage<T>(this HtmlHelper helper)
            where T : Controller
        {
            if (helper.ViewContext.Controller is T)
                return "active";

            return string.Empty;
        }


        public static string ActivePage<T>(this HtmlHelper helper, string key, string value)
            where T : Controller
        {
            if (helper.ViewContext.Controller is T)
            {
                object currentvalue = null;
                if (helper.ViewContext.RouteData.Values.TryGetValue(key, out currentvalue))
                {
                    if (((string)currentvalue).Equals(value, StringComparison.OrdinalIgnoreCase))
                        return "active";
                }
            }

            return string.Empty;
        }
    }
}