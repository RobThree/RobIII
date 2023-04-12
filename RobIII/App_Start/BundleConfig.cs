using System.Web.Optimization;

namespace MvcBootstrap
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js-body").Include(
                "~/js/vendor/bootstrap.min.js",
                "~/js/vendor/ekko-lightbox.js",
                "~/js/main.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/css/bootstrap.min.css",
                "~/css/ekko-lightbox.css",
                "~/css/main.css"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}