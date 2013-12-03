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
    public class EmployerControllerTest
    {
        private IUnityContainer UnityIoc;
        private IServiceLocator serviceLocator;


        public EmployerControllerTest()
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
        public void OrganisationDetailsRegistration()
        {
            //Arrange:
            var controller = serviceLocator.GetInstance<EmployerController>();

            //Act:
            var organisationReg = controller.OrganisationRegForm() as ViewResult;

            //Assert:
            Assert.IsNotNull(organisationReg);
            Assert.AreEqual(organisationReg.ViewBag.MenuSelected, "Employers");
            Assert.AreEqual(organisationReg.ViewName, "OrganisationRegForm");
        }

        [TestMethod]
        public void OrganisationLoadEmployeeOportunity()
        {
            //Arrange:
            var controller = serviceLocator.GetInstance<EmployerController>();

            //Act:
            ViewResult employmentOpp = controller.StudentOrGraduateJobsForm(0) as ViewResult;

            //Assert:
            Assert.IsNotNull(employmentOpp);
            Assert.AreEqual(employmentOpp.ViewName, "GraduateJobsForm");
            Assert.AreEqual(employmentOpp.ViewBag.MenuSelected, "Employers");
            Assert.AreEqual(employmentOpp.ViewBag.EmploymentType, "Load Employment Opportunity");
        }

        [TestMethod]
        public void OrganisationLoadVolunteeringOportunity()
        {
            //Arrange:
            EmployerController controller = serviceLocator.GetInstance<EmployerController>();

            //Act:
            ViewResult volunteeringOpp = controller.StudentOrGraduateJobsForm(1) as ViewResult;

            //Assert:
            Assert.IsNotNull(volunteeringOpp);
            Assert.AreEqual(volunteeringOpp.ViewName, "LoadEmploymentOpportunity");
            Assert.AreEqual(volunteeringOpp.ViewBag.MenuSelected, "Employers");
            Assert.AreEqual(volunteeringOpp.ViewBag.EmploymentType, "Load Placement Opportunity");
        }
    }
}
