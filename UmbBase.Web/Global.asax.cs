using System;
using System.Web.Optimization;
using Umbraco.Web;

namespace UmbBase.Web
{
    public class Global : UmbracoApplication
    {
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            base.OnApplicationStarted(sender, e);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}