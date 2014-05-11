using MvcBootstrap;
using System;
using System.Globalization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RobIII
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var ci = new CultureInfo("en-US");

            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
