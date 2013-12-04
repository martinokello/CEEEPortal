using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using CEEEPortal.Models;

namespace CEEEPortal.Controllers
{
    public class MentoringController : Controller
    {
        //
        // GET: /Mentoring/
        public ActionResult IndexMentoring()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            ViewBag.MenuSelected = "Mentoring";
            return View("IndexMentoring");
        }

        [Authorize]
        [HttpGet]
        public ActionResult MentoringDetails()
        {
            ViewBag.MenuSelected = "Mentoring";
            return View("MentoringDetails");
        }

        [Authorize]
        [HttpPost]
        public ActionResult MentoringDetails(MentorViewModel model,FormCollection dropInDates)
        {
            ViewBag.MenuSelected = "Mentoring";
            int totalContactHours = 0;
            int totalContactMinutes = 0;
            int numberOfDropinVisits = 0;

            if(model.MentorType == null)model.MentorType = new MentorType();
            foreach (string key in dropInDates.Keys)
            {
                if (key.StartsWith("DropinDate"))
                {
                    var iteration = GetDropinIteration(key);

                    var dropInDate = dropInDates["DropinDate" + iteration];

                    numberOfDropinVisits++;
                }
            }

            model.VisitsToDropinCentre = numberOfDropinVisits;
            ModelState.Clear();

            if (model.PrefContactMethod == null) ModelState.AddModelError("prefContact", "Preferred Contact Method is required.");
            if (model.MentorType == null) ModelState.AddModelError("mentorType", "Mentor Type is required.");
            return View("MentoringDetails",model);
        }

        private string GetDropinIteration(string keyValue)
        {
            string iteration = string.Empty;
 
            foreach (char ch in keyValue)
            {
                if (Char.IsDigit(ch))
                {
                    iteration += ch;
                }
            }

            return iteration;
        }

        [HttpGet]
        public JsonResult GetStudent(int studentID)
        {
            if (studentID == 4444)
            {
                var mentorModel = new MentorViewModel
                    {
                        CourseCode = "4444",
                        CourseName = "Computer Science",
                        StudentId = studentID,
                        StudentName = "Kurram Zahid",
                        UWLEmail = "kurram.zahid@uwl.ac.uk",
                        PhoneNo = "07809774456",
                        PersonalEmail = "kurram.zahid@gmail.com",
                        YearOfStudy = 3
                    };
                return Json(mentorModel, JsonRequestBehavior.AllowGet);
            }
            return Json(new MentorViewModel(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMentees(string mentor)
        {
            return GetMockMentees();
            
        }
        private JsonResult GetMockMentees()
        {
            var menteeList = new List<MenteeDetailView>
                {
                    new MenteeDetailView {MenteeName = "Kurram Zaid", StudentID = "4444"},
                    new MenteeDetailView {MenteeName = "Geoff Madison", StudentID = "13456"},
                    new MenteeDetailView {MenteeName = "Molly Erickson", StudentID = "23254"}
                };
            return Json(menteeList,JsonRequestBehavior.AllowGet);
        }
    }
}
