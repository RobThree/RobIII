using RobIII.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace RobIII
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("static", typeof(StaticPageConstraint));

            routes.MapMvcAttributeRoutes(constraintResolver);
        }
    }
}
