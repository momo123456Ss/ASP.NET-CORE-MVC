using QLBHHuynhHiepThien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace QLBHHuynhHiepThien
{
    public class MvcApplication : System.Web.HttpApplication
    {
        NorthwindDataContext dbContext = new NorthwindDataContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void ViewSession_Start()
        {
            Session["EmailClient"] = "";
        }
    }
}
