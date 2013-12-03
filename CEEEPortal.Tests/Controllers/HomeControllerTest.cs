using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEEEPortal;
using CEEEPortal.Controllers;

namespace CEEEPortal.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IUnityContainer UnityIoc;
        private IServiceLocator serviceLocator;


        public HomeControllerTest()
        {
            UnityIoc = CEEEPortal.Bootstrapper.Initialise();
            this.serviceLocator = new UnityServiceLocator(UnityIoc);
        }

        [TestInitialize]
        public void SetUp()
        {
            
        }

        [TestCleanup]
        public void TearDown()
        {
   
        }


        [TestMethod]
        public void IndexTest()
        {
            //Arrange:
            var controller = serviceLocator.GetInstance<HomeController>();

            //Act:
            var indexView = controller.Index() as ViewResult;

            //Assert:
            Assert.IsNotNull(indexView);
        }

    }
}
