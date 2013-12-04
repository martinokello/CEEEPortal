using System.Configuration;
using System.Web.Mvc;
using CEEEServices;
using CEEEServices.Interfaces;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace CEEEPortal
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>(); 

            container.RegisterType<IMailService, EmailService>(new InjectionConstructor("smtp.tesco.net"));
            container.RegisterType<IMailTemplateService, MailTemplateService>();
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}