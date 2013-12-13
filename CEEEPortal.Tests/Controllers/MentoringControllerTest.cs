using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CEEEPortal.Controllers;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CEEEPortal.Tests.Controllers
{
    [TestClass]
    public class MentoringControllerTest
    {
        private IUnityContainer UnityIoc;
        private IServiceLocator serviceLocator;


        public MentoringControllerTest()
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
        public void TestIndexMentoring()
        {
            //Arrange:
            var controller = serviceLocator.GetInstance<MentoringController>();

            //Act:
            var mentoringIndex = controller.IndexMentoring() as ViewResult;

            //Assert:
            Assert.IsNotNull(mentoringIndex);
            Assert.AreEqual(mentoringIndex.ViewName, "IndexMentoring");
            Assert.AreEqual(mentoringIndex.ViewBag.MenuSelected, "Mentoring");

        }

        [TestMethod]
        public void MentoringDetails()
        {
            //Arrange:
            var controller = serviceLocator.GetInstance<MentoringController>();

            //Act:
            var mentoringDetails = controller.MentoringDetails() as ViewResult;

            //Assert:
            Assert.IsNotNull(mentoringDetails);
            Assert.AreEqual(mentoringDetails.ViewName, "MentoringDetails");
            Assert.AreEqual(mentoringDetails.ViewBag.MenuSelected, "Mentoring");
        }
    }
}
