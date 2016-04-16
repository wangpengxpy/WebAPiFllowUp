using Common;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAPiFllowUp.Filters;
using WebAPiFllowUp.StartUnity;

namespace WebAPiFllowUp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            logger.LogInfo("--------通过Unity依赖注入服务");

            Bootstrapper.Initial();

            //GlobalConfiguration.Configuration.Filters.Add(new ApiAuthenticationFilter());


        }
    }
}
