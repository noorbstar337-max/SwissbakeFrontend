using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SwissbakeIn
{
    public class RouteConfig
    {
     // public   string curl, purl;
        public static void RegisterRoutes(RouteCollection routes)
        {
         

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            ////   routes.MapMvcAttributeRoutes();
            routes.MapRoute(
               name: "Home3",
               url: "",
               defaults: new { controller = "Home", action = "Index4", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "tracking",
             url: "trackingdetails/{id}",
             defaults: new { controller = "Home", action = "Trackingdetails", id = UrlParameter.Optional }
         );


            routes.MapRoute(
                   name: "categorygrid",
                   url: "category",
                   defaults: new { controller = "Home", action = "Category" }
               );

            routes.MapRoute(
                    name: "Exclusive",
                    url: "exclusive",
                    defaults: new { controller = "Home", action = "Search" }
                );


            routes.MapRoute(
          name: "Career",
          url: "Career",
          defaults: new { controller = "Misc", action = "Career" }
          );

            routes.MapRoute(
         name: "privacy",
         url: "privacy-policy",
         defaults: new { controller = "Misc", action = "privacy" }
         );

            routes.MapRoute(
         name: "tou",
         url: "terms-of-use",
         defaults: new { controller = "Misc", action = "tou" }
         );
            routes.MapRoute(
         name: "shipping",
         url: "shipping-policy",
         defaults: new { controller = "Misc", action = "shipping" }
         );

            routes.MapRoute(
                   name: "Homecont",
                   url: "{action}",
                   defaults: new { controller = "Home", action = "Index4" }
               );

            routes.MapRoute(
           name: "access",
           url: "access/{action}",
           defaults: new { controller = "Access", action = "Index" }
           );

           // routes.MapRoute(
           //name: "Misc",
           //url: "misc/{action}",
           //defaults: new { controller = "Misc", action = "Index" }
           //);

            routes.MapRoute(
                   name: "Accountcont",
                   url: "account/{action}",
                   defaults: new { controller = "Account", action = "Index" }
               );

            Route myRoute = new Route("category/{url}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Product",
                    url = ""
                })
            };
            routes.Add("Categorypro", myRoute);


            Route myRoute6 = new Route("solution/{url}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "SolDetail",
                    url = ""
                })
            };
            routes.Add("Solution", myRoute6);


            Route myRoute5 = new Route("recipe/{url}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Recipedet",
                    url = ""
                })
            };
            routes.Add("recipedet", myRoute5);


            Route myRoute2 = new Route("blogcategory/{url}/{search}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "BlogCat",
                    url = "",
                    search = ""
                })
            };
            routes.Add("Categoryblog", myRoute2);

            Route myRoute3 = new Route("blog/{url}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "BlogDetail",
                    url = ""
                })
            };
            routes.Add("Blogdetail", myRoute3);

            //routes.MapRoute(
            //             name: "Categorypro",
            //             url: "{url}",
            //             defaults: new { controller = "Home", action = "Product", url = "" }
            //         );

            Route myRoute4 = new Route("search/{value}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Search",
                    value = ""
                })
            };
            routes.Add("Search", myRoute4);




            //routes.MapRoute(
            //        name: "detail",
            //        url: "{Curl}/{Purl}",
            //        defaults: new { controller = "Home", action = "Detail", Curl = UrlParameter.Optional, Purl = UrlParameter.Optional }
            //    );

            routes.MapRoute(
                     name: "detail",
                     url: "product/{Purl}",
                     defaults: new { controller = "Home", action = "Detail", Purl = UrlParameter.Optional }
                 );


            //            routes.MapRoute(
            //                name: "Home2",
            //                url: "index2",
            //                defaults: new { controller = "Home", action = "Index2", id = UrlParameter.Optional }
            //            );

            //            routes.MapRoute(
            //        name: "categorygrid",
            //        url: "category",
            //        defaults: new { controller = "Home", action = "Category", url = UrlParameter.Optional }
            //    );


            //            routes.MapRoute(
            //name: "account",
            //url: "{action}",
            //defaults: new { controller = "account", action = "index" }
            //);
            //            routes.MapRoute(
            //             name: "category",
            //             url: "{url}",
            //             defaults: new { controller = "Home", action = "Product", url = UrlParameter.Optional }
            //         );
            //            routes.MapRoute(
            //name: "about",
            //url: "home/about",
            //defaults: new { controller = "Home", action = "about" }
            //);
            //            routes.MapRoute(
            //name: "recipe",
            //url: "home/recipe",
            //defaults: new { controller = "Home", action = "recipe" }
            //);
            //            routes.MapRoute(
            //name: "login",
            //url: "home/login",
            //defaults: new { controller = "Home", action = "login" }
            //);




            ////            routes.MapRoute(
            ////name: "logo",
            ////url: "{action}",
            ////defaults: new { controller = "account", action = "logout" }
            ////);






            //            routes.MapRoute(
            //name: "access",
            //url: "access/{action}",
            //defaults: new { controller = "access", action = "index" }
            //);






            //            routes.MapRoute(
            //        name: "detail",
            //        url: "{Curl}/{Purl}",
            //        defaults: new { controller = "Home", action = "detail" }
            //    );

            //routes.MapRoute(
            //    name: "Api",
            //    url: "api/{action}/{id}",
            //    defaults: new { controller = "Access", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index4", id = UrlParameter.Optional }
            );
        }
    }
}
