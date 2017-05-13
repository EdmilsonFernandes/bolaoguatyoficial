using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Bolao
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            base.Application.Add("addUserBolao", 0);
        }

        private void Session_End(object sender, EventArgs e)
        {
            int num = ((int)base.Application["addUserBolao"]) - 1;
            base.Application["addUserBolao"] = num;
        }
        private void Session_Start(object sender, EventArgs e)
        {
            int num = ((int)base.Application["addUserBolao"]) + 1;
            base.Application["addUserBolao"] = num;
        }
    }
}