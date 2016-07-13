using System.Web;
using System.Web.Optimization;

namespace IBC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                      "~/Scripts/tinymce/tinymce.min.js",
                      "~/Scripts/tinymce/jquery.tinymce.min.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                                  "~/Content/bootstrap.css",
                                  "~/Content/font-awesome.css",
                                  "~/Content/site.css"));
            //Configuração do GSDK
            bundles.Add(new StyleBundle("~/Content/css/GSDK").Include(
                "~/UIKit/Themes/GSDK/css/gsdk-base.css",
                "~/UIKit/Themes/GSDK/css/gsdk-checkbox-radio-switch.css",
                "~/UIKit/Themes/GSDK/css/,gsdk-presentation.css",
                "~/UIKit/Themes/GSDK/css/gsdk-sliders.css",
                "~/Content/site.css"));



            bundles.Add(new ScriptBundle("~/bundles/GSDK").Include(
                    "~/UIKit/Themes/GSDK/js/custom.js",
                    "~/UIKit/Themes/GSDK/js/get-shit-done.js",
                     "~/UIKit/Themes/GSDK/js/get-shit-done.js",
                    "~/UIKit/Themes/GSDK/js/gsdk-bootstrapswitch.js",
                    "~/UIKit/Themes/GSDK/js/gsdk-checkbox.js",
                    "~/UIKit/Themes/GSDK/js/gsdk-radio.js"));

            //Confguração do PaperKit
            bundles.Add(new StyleBundle("~/Content/css/PaperKit").Include(
               "~/UIKit/Themes/Paper/css/ct-paper.css",
               "~/Content/site.css"));


            bundles.Add(new ScriptBundle("~/bundles/PaperKit").Include(
                //"~/UIKit/Themes/Paper/js/custom.js",
                    "~/UIKit/Themes/Paper/js/bootstrap-datepicker.js",
                    "~/UIKit/Themes/Paper/js/bootstrap-select.js",
                    "~/UIKit/Themes/Paper/js/ct-paper-checkbox.js",
                    "~/UIKit/Themes/Paper/js/ct-paper-radio.js",
                    "~/UIKit/Themes/Paper/js/ct-paper.js"));


            //Calendar
            bundles.Add(new StyleBundle("~/Calendar/css").Include(
                    "~/UIKit/Components/Calendar/css/fullcalendar.css",
                    "~/UIKit/Components/Calendar/css/fullcalendar.print.css"));

            bundles.Add(new ScriptBundle("~/Calendar/js").Include(
                "~/UIKit/Components/Calendar/js/fullcalendar.js"));

            //Datepicker
            bundles.Add(new StyleBundle("~/Datepicker/css").Include(
                    "~/UIKit/Components/Datepicker/css/bootstrap-datepicker.css"));

            bundles.Add(new ScriptBundle("~/Datepicker/js").Include(
               "~/UIKit/Components/Datepicker/js/bootstrap-datepicker.js"));


            //Wizzard
            bundles.Add(new StyleBundle("~/Wizzard/css").Include(
                    "~/UIKit/Components/Wizzard/css/wizzard.css"));

            bundles.Add(new ScriptBundle("~/Wizzard/js").Include(
                "~/UIKit/Components/Wizzard/js/jquery.bootstrap.wizard.js",
                "~/UIKit/Components/Wizzard/js/wizard.js"));




#if (Release)
            {
                BundleTable.EnableOptimizations = true;
            }
#endif
        }
    }
}