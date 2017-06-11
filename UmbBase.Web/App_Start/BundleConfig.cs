using System.Web.Optimization;

namespace UmbBase.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/css/base.css",
                                                                 "~/css/skeleton.css",
                                                                 "~/css/layout.css",
                                                                 "~/css/settings.css",
                                                                 "~/css/font-awesome.css",
                                                                 "~/css/owl.carousel.css",
                                                                 "~/css/retina.css",
                                                                 "~/css/animsition.min.css"
                                                                 ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/jquery-2.1.1.js",
                                                                 "~/Scripts/modernizr.custom.js",
                                                                 "~/Scripts/jquery.mobile.custom.min.js",
                                                                 "~/Scripts/retina-1.1.0.min.js",
                                                                 "~/Scripts/jquery.animsition.min.js",
                                                                 "~/Scripts/jquery.themepunch.tools.min.js",
                                                                 "~/Scripts/jquery.themepunch.revolution.min.js",
                                                                 "~/Scripts/jquery.easing.js",
                                                                 "~/Scripts/jquery.hidescroll.min.js",
                                                                 "~/Scripts/smoothScroll.js",
                                                                 "~/Scripts/jquery.parallax-1.1.3.js",
                                                                 "~/Scripts/jquery.counterup.min.js",
                                                                 "~/Scripts/waypoints.min.js",
                                                                 "~/Scripts/scrollReveal.js",
                                                                 "~/Scripts/owl.carousel.min.js"
                                                                 
                                                                 ));                       
        }
    }
}