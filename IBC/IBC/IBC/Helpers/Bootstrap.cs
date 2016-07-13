using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC.Helpers
{
    public class Bootstrap
    {
        public const string BundleStyleBase = "~/Content/css/";
        public const string BundleScriptBase = "~/Scripts/";
        public class Theme
        {
            public const string GSDK = "GSDK";
            public const string PaperKit = "Paper";
        }

        public static HashSet<string> Themes = new HashSet<string>
        {
            Theme.GSDK, 
            Theme.PaperKit
        };

        public static string StyleBundle(string themename)
        {
            return BundleStyleBase + themename;
        }

        public static string ScriptBundle(string themename)
        {
            return BundleScriptBase + themename;
        }
    }
}