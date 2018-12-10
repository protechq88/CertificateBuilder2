using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CertificateBuilder2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Index",
                url: "index",
                defaults: new { controller = "Certificate", action = "Index" }
                );

            routes.MapRoute(
                name: "GetCertificateList",
                url: "getcertificatelist",
                defaults: new { controller = "Certificate", action = "GetCertificateList" }
                );

            routes.MapRoute(
                 name: "SaveAndContinue",
                 url: "saveandcontinue",
                 defaults: new { controller = "Certificate", action = "SaveAndContinue" }
                 );

            routes.MapRoute(
                name: "SaveAndExit",
                url: "saveandexit",
                defaults: new { controller = "Certificate", action = "SaveAndExit" }
                );


            routes.MapRoute(
                 name: "Certificate",
                 url: "certificate/{CertId}",
                 defaults: new { controller = "Certificate", action = "Certificate", CertId = UrlParameter.Optional }
                 );

            routes.MapRoute(
                 name: "DeleteCertificate",
                 url: "deletecertificate/{CertId}",
                 defaults: new { controller = "Certificate", action = "DeleteCertificate", CertId = UrlParameter.Optional }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Certificate", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}