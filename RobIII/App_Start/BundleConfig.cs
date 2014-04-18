using System.Web.Optimization;

namespace MvcBootstrap
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Bootstrap.js / jQuery and modernizr/respond.js are loaded from CDN or separately
            //TODO: Take a look at http://www.asp.net/mvc/tutorials/mvc-4/bundling-and-minification; turns out CDN's can be used/specified here too! Coolio!

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/js/main.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        "~/css/bootstrap.min.css",
                        "~/css/main.css"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}