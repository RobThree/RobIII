using System.IO;
using System.Web;
using System.Web.Routing;

namespace RobIII.Helpers
{
    public class StaticPageConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string viewPath = httpContext.Server.MapPath(string.Format("~/Views/Static/{0}.cshtml", values[parameterName]));

            return File.Exists(viewPath);
        }
    }
}