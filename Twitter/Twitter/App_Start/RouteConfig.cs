using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Twitter
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Create",
                url: "Post/New",
                defaults: new { controller = "Post", action = "Create" }
                );

            routes.MapRoute(
                name: "Pagination",
                url: "Post/{id}",
                defaults: new { controller = "Post", action = "Details" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Twitter", action = "StartPage", id = UrlParameter.Optional }
            );
        }
    }
}
