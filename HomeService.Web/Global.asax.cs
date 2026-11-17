using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HomeService.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");

            Response.Headers.Add(
                "Access-Control-Allow-Headers",
                "Content-Type, Accept");

            Response.Headers.Add(
                "Access-Control-Allow-Methods",
                "GET, POST, PUT, DELETE, OPTIONS");

            if (Request.HttpMethod == "OPTIONS")
            {
                Response.StatusCode = 200;

                Response.Flush();

                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }
}
