using System.Web.Http;

namespace MvcBootstrap
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) =>
            // Web API routes
            config.MapHttpAttributeRoutes();
    }
}
