﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShareTrader
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //SqlDependency.Start(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ShareTrader-20200316103418;");
        }

        protected void Application_End()
        {
            //SqlDependency.Stop(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ShareTrader-20200316103418;");
        }
    }

}
