using System.Web.Optimization;

namespace MvcBootstrap
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Bootstrap.js / jQuery and modernizr/respond.js are loaded from CDN or separately

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