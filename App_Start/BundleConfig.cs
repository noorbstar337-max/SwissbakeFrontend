using System.Web;
using System.Web.Optimization;

namespace SwissbakeIn
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/Main").Include(
                      "~/Content/js/angular.min.js",
                      "~/Content/js/angular-route.min.js",
                      "~/Content/js/angular-locale_hi-in.min.js",
                      "~/Content/js/jquery.js",
                      "~/Content/js/plugins.js",
                      "~/Content/js/functions.js",
                      "~/Content/js/jquery.visible.min.js",
                      "~/Content/js/custom3.js",
                      "~/Content/js/jquery-confirm.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/Rev").Include(
                "~/Content/include/rs-plugin/js/jquery.themepunch.tools.min.js",
                "~/Content/include/rs-plugin/js/jquery.themepunch.revolution.min.js",
                "~/Content/include/rs-plugin/js/addons/revolution.addon.slicey.min.js",
                "~/Content/include/rs-plugin/js/extensions/revolution.extension.slideanims.min.js",
                "~/Content/include/rs-plugin/js/extensions/revolution.extension.actions.min.js",
                "~/Content/include/rs-plugin/js/extensions/revolution.extension.kenburn.min.js",
                "~/Content/include/rs-plugin/js/extensions/revolution.extension.layeranimation.min.js",
                "~/Content/include/rs-plugin/js/extensions/revolution.extension.migration.min.js",
                "~/Content/include/rs-plugin/js/extensions/revolution.extension.navigation.min.js",
                "~/Content/include/rs-plugin/js/extensions/revolution.extension.parallax.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/PDetail").Include(
                      "~/Content/js/register/jquery.validate.min.js",
                      "~/Content/js/rangeslider.min.js",
                      "~/Content/include/angularrange/rzslider.min.js",
                      "~/Content/js/components/star-rating.js"
                      ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Main").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/style.css",
                      "~/Content/css/swiper.css",
                      "~/Content/css/dark.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/magnific-popup.css",
                      "~/Content/css/responsive.css",
                      "~/Content/css/jquery-confirm.min.css",
                      "~/Content/css/ecommerce/ecommerce.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Rev").Include(
                      "~/Content/include/rs-plugin/css/settings.css",
                      "~/Content/include/rs-plugin/css/layers.css",
                      "~/Content/include/rs-plugin/css/navigation.css"));

            bundles.Add(new StyleBundle("~/Content/PDetail").Include(
                      "~/Content/include/angularrange/rzslider.min.css",
                      "~/Content/css/rangeslider.css",
                      "~/Content/images/icons/swiss-attributes/style.css",
                      "~/Content/css/components/bs-rating.css"));


            BundleTable.EnableOptimizations = true;
        }
    }
}
