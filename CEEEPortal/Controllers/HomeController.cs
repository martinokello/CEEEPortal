using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CEEEPortal.Models;
using CEEEServices;
using CEEEServices.Interfaces;
using Microsoft.Practices.Unity;

namespace CEEEPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        public ActionResult TestView()
        {
            return View("Test");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult LookUpPostCode(string postCode)
        {
            var address = GetAddress(postCode);
            return Json(address, JsonRequestBehavior.AllowGet);
        }

        private IList<AddressModel> GetAddress(string postCode)
        {
            //Call PostCode anywhere: API.
            var addresses = new List<AddressModel>
                {
                    new AddressModel
                        {
                            AddressLine1 = "2 St Johns Terrace",
                            AddressLine2 = "Flat 1",
                            City = "London",
                            Country = "England"
                        },                    
                        new AddressModel
                        {
                            AddressLine1 = "2 St Johns Terrace",
                            AddressLine2 = "Flat 2",
                            City = "London",
                            Country = "England"
                        },                    
                        new AddressModel
                        {
                            AddressLine1 = "2 St Johns Terrace",
                            AddressLine2 = "Flat 3",
                            City = "London",
                            Country = "England"
                        },                    
                        new AddressModel
                        {
                            AddressLine1 = "2 St Johns Terrace",
                            AddressLine2 = "Flat 4",
                            City = "London",
                            Country = "England"
                        },                    
                        new AddressModel
                        {
                            AddressLine1 = "2 St Johns Terrace",
                            AddressLine2 = "Flat 5",
                            City = "London",
                            Country = "England"
                        },                    
                        new AddressModel
                        {
                            AddressLine1 = "2 St Johns Terrace",
                            AddressLine2 = "Flat 6",
                            City = "London",
                            Country = "England"
                        },                    
                        new AddressModel
                        {
                            AddressLine1 = "2 St Johns Terrace",
                            AddressLine2 = "Flat 7",
                            City = "London",
                            Country = "England"
                        },                    
                        new AddressModel
                        {
                            AddressLine1 = "2 St Johns Terrace",
                            AddressLine2 = "Flat 8",
                            City = "London",
                            Country = "England"
                        }
                };
            return addresses;
        }
        //
        // GET: /Account/Login
    }
}
