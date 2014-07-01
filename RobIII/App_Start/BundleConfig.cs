using System.Web.Optimization;

namespace MvcBootstrap
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Bootstrap.js / jQuery from CDN or separately
            //Modernizr/respond.js are loaded 
            //TODO: Take a look at http://www.asp.net/mvc/tutorials/mvc-4/bundling-and-minification; turns out CDN's can be used/specified here too! Coolio!

            bundles.Add(new ScriptBundle("~/bundles/js-head").Include(
                "~/js/vendor/modernizr-2.8.1.js",
                "~/js/vendor/respond-1.4.2.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js-body").Include(
                "~/js/vendor/ekko-lightbox.js",
                "~/js/main.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/css/bootstrap.min.css",
                "~/css/ekko-lightbox.min.css",
                "~/css/main.css"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}