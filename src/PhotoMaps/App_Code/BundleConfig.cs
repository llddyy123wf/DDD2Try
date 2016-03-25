using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PhotoMaps.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css").Include("~/Style/css/bootstrap.css","~/Style/style.css"));

            bundles.Add(new ScriptBundle("~/content/jquery").Include("~/Scripts/jquery-2.0.3.js"));
            bundles.Add(new ScriptBundle("~/content/map").Include("~/Scripts/baiduMap.js"));
        }
    }
}