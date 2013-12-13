using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CEEEServices;
using CEEEServices.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using WebMatrix.WebData;


namespace CEEEPortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static UnityServiceLocator serviceLocator;
        public static IUnityContainer UnityIoc;

        protected void Application_Start()
        {

            //Replace fields with the Database you will be using for membership.
            //WebSecurity.InitializeDatabaseConnection(

            //    connectionStringName: "DefaultConnection",

            //    userTableName: "UserProfile",

            //    userIdColumn: "UserID",

            //    userNameColumn: "UserName",

            //    autoCreateTables: false);

            //IOC Ninject Registration
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //Setup DI
            UnityIoc = Bootstrapper.Initialise();
            serviceLocator = new UnityServiceLocator(UnityIoc);
        }


    }
}