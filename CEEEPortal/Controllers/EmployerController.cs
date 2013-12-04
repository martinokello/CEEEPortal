using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CEEEPortal.Models;
using CEEEServices;
using CEEEServices.Interfaces;
using CEEEPortal.Business;
using WebMatrix.WebData;

namespace CEEEPortal.Controllers
{
    public class EmployerController : Controller
    {
        //
        // GET: /Employer/
        private IMailService mailService;
        private IMailTemplateService mailTemplateService;
        public EmployerController(IMailService mailService, IMailTemplateService mailTemplateService)
        {
            this.mailService = mailService;
            this.mailTemplateService = mailTemplateService;
        }
        public ActionResult Index()
        {
            ViewBag.MenuSelected = "Employers";

            return View("Index");
        }

        [HttpGet]
        public ActionResult OrganisationRegForm()
        {
            ViewBag.MenuSelected = "Employers";
            SetUpViewBagWithTitlesUI();
            SetUpViewBagWithCompanySizeUI();
            SetUpViewBagWithHowHeardUI();
            return View("OrganisationRegForm");
        }

        [HttpPost]
        public ActionResult OrganisationRegForm(OrgRegFormViewModel model)
        {
            var titleSelectedValue = model.Title.ToString();
            //Set up Title GUI
            SetUpViewBagWithTitlesUI(model.Title.ToString());

            foreach (var titleItem in (ViewBag.Titles as List<SelectListItem>))
            {
                if (titleItem.Value.Equals(titleSelectedValue, StringComparison.OrdinalIgnoreCase))
                {
                    titleItem.Selected = true;
                    break;
                }
            }

            //Set up Company Size GUI
            SetUpViewBagWithCompanySizeUI(model.CompanySize);

            foreach (var companySize in (ViewBag.CompanySize as List<SelectListItem>))
            {
                if (companySize.Value.Equals(model.CompanySize, StringComparison.OrdinalIgnoreCase))
                {
                    companySize.Selected = true;
                    break;
                }
            }

            //Set up HowHeard GUI
            SetUpViewBagWithHowHeardUI(model.HowHeard);

            foreach (var howHeard in (ViewBag.HowH as List<SelectListItem>))
            {
                if (howHeard.Value.Equals(model.HowHeard, StringComparison.OrdinalIgnoreCase))
                {
                    howHeard.Selected = true;
                    break;
                }
            }
            if (CeeePortalBusiness.CheckUserNotExists(model.Email))
            {

                if (model.OpportunityType == null)
                {
                    model.OpportunityType = new OpportunityType();
                }


                var mailMessage = new MailMessage();
                var physicalPath = Server.MapPath("~/MailTemplate/" + ConfigurationManager.AppSettings["TemplateName"]);


                mailMessage.Body = mailTemplateService.LoadTemplateByName(physicalPath);
                mailMessage.From = new MailAddress("info@uwl.ac.uk");
                mailMessage.To.Add(new MailAddress("employment.services@uwl.ac.uk"));
                mailMessage.Subject = string.Format("New Employer {0} has registered for Employee prospects", model.OrganisationName);
                //mailService.SendMail(mailMessage);
                model.UserId = Register(new RegisterModel { Password = model.Password, UserName = model.Email });

                var actionResult = CeeePortalBusiness.SaveClientDetails(model);
                if (actionResult)
                {
                    //return RedirectToAction("Register", "Account", new RegisterModel {UserName = model.Email, Password = model.Password});
                    return View("SuccessView");
                }
                else
                {
                    ModelState.AddModelError("Error", "An unknown problem occured, and Data not saved, please contact your system Administrator.");
                    return View("OrganisationRegForm", model);
                }
            }
            else
            {
                ModelState.AddModelError("UserExists", "User with that email exists. Please register another user.");
                return View("OrganisationRegForm", model);
            }
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public int Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return WebSecurity.GetUserId(model.UserName);

                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return -1;
        }


        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult UpdateOrganisationDetails()
        {
            var model = CeeePortalBusiness.GetClientDetailsByID(CeeePortalBusiness.GetClientIdByName(User.Identity.Name));

            SetUpViewBagWithTitlesUI(model.Title.ToString());
            SetUpViewBagWithCompanySizeUI(model.CompanySize);
            SetUpViewBagWithHowHeardUI(model.HowHeard);
            var updateModel = new UpdateDetailsViewModel();
            updateModel.Address = model.Address;
            updateModel.BusinessRegNumber = model.BusinessRegNumber;
            updateModel.CharityRegNumber = model.CharityRegNumber;
            updateModel.CompanySize = model.CompanySize;
            updateModel.Email = model.Email;
            updateModel.FirstName = model.FirstName;
            updateModel.LastName = model.LastName;
            updateModel.LandLineNo = model.LandLineNo;
            updateModel.MobileNo = model.MobileNo;
            updateModel.OpportunityType = model.OpportunityType;
            updateModel.OrgWebSite = model.OrgWebSite;
            updateModel.OrganisationDoes = model.OrganisationDoes;
            updateModel.OrganisationName = model.OrganisationName;
            updateModel.Title = model.Title;
            updateModel.JobTitle = model.JobTitle;
            updateModel.HowHeard = model.HowHeard;

            var titleSelectedValue = model.Title.ToString();
            if (model.OpportunityType == null)
            {
                model.OpportunityType = new OpportunityType();
            }
            //Set up Title GUI
            SetUpViewBagWithTitlesUI(titleSelectedValue);

            foreach (var titleItem in (ViewBag.Tit as List<SelectListItem>))
            {
                if (titleItem.Value.Equals(titleSelectedValue, StringComparison.OrdinalIgnoreCase))
                {
                    titleItem.Selected = true;
                    break;
                }
            }

            //Set up Company Size GUI
            SetUpViewBagWithCompanySizeUI();

            foreach (var companySize in (ViewBag.CompanyS as List<SelectListItem>))
            {
                if (companySize.Value.Equals(model.CompanySize, StringComparison.OrdinalIgnoreCase))
                {
                    companySize.Selected = true;
                    break;
                }
            }

            //Set up HowHeard GUI
            SetUpViewBagWithHowHeardUI();

            foreach (var howHeard in (ViewBag.HowH as List<SelectListItem>))
            {
                if (howHeard.Value.Equals(model.HowHeard, StringComparison.OrdinalIgnoreCase))
                {
                    howHeard.Selected = true;
                    break;
                }
            }
            return View("UpdateDetails", updateModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateOrganisationDetails(UpdateDetailsViewModel model)
        {
            var titleSelectedValue = model.Title.ToString();
            if (model.OpportunityType == null)
            {
                model.OpportunityType = new OpportunityType();
            }
            //Set up Title GUI
            SetUpViewBagWithTitlesUI(model.Title.ToString());

            foreach (var titleItem in (ViewBag.Tit as List<SelectListItem>))
            {
                if (titleItem.Value.Equals(titleSelectedValue, StringComparison.OrdinalIgnoreCase))
                {
                    titleItem.Selected = true;
                    break;
                }
            }

            //Set up Company Size GUI
            SetUpViewBagWithCompanySizeUI(model.CompanySize);

            foreach (var companySize in (ViewBag.CompanyS as List<SelectListItem>))
            {
                if (companySize.Value.Equals(model.CompanySize, StringComparison.OrdinalIgnoreCase))
                {
                    companySize.Selected = true;
                    break;
                }
            }

            //Set up HowHeard GUI
            SetUpViewBagWithHowHeardUI(model.HowHeard);

            foreach (var howHeard in (ViewBag.HowH as List<SelectListItem>))
            {
                if (howHeard.Value.Equals(model.HowHeard, StringComparison.OrdinalIgnoreCase))
                {
                    howHeard.Selected = true;
                    break;
                }
            }

            var client = CeeePortalBusiness.GetClientDetailsByID(CeeePortalBusiness.GetClientIdByName(User.Identity.Name));
            model.UserId = CeeePortalBusiness.GetUserIdByUsername(User.Identity.Name);
            var actionResult = CeeePortalBusiness.UpdateClientDetails(model);

            if (actionResult)
            {
                WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                var mailMessage = new MailMessage();
                var physicalPath = Server.MapPath("~/MailTemplate/" + ConfigurationManager.AppSettings["TemplateName"]);


                mailMessage.Body = mailTemplateService.LoadTemplateByName(physicalPath);
                mailMessage.From = new MailAddress("info@uwl.ac.uk");
                mailMessage.To.Add(new MailAddress("employment.services@uwl.ac.uk"));
                mailMessage.Subject = string.Format("New Employer {0} has registered for Employee prospects", model.OrganisationName);
                //mailService.SendMail(mailMessage);
                return View("SuccessView");
            }
            else
            {
                ModelState.AddModelError("Error", "An unknown problem occured, and Data not saved, please contact your system Administrator.");

                return View("UpdateDetails", model);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult UpdateJobDetail(int jobId)
        {
            Session["JobID"] = jobId;
            var viewTypeId = CeeePortalBusiness.GetJobTypeById(jobId);
            switch ((JobTypeview)viewTypeId)
            {
                case JobTypeview.GraduateOrStudentJob:
                    var gradModel = CeeePortalBusiness.GetGraduateOrStudentJob(jobId);
                    //Fill model
                    return View("StudentOrGraduateJobsFormUpdate", gradModel);
                case JobTypeview.SandwichOrPlacementJob:
                    SetPositionsUI();
                    var sandModel = CeeePortalBusiness.GetSandwichOrPlacementJob(jobId);
                    //Fill model
                    return View("SandwichPlacementsInternshipsUpdate", sandModel);
                case JobTypeview.OnedDayChallengeOrCharityVolunteeringJob: 
                    var oneDayModel = CeeePortalBusiness.GetOnedDayChallengeOrCharityVolunteeringJob(jobId);
                    SetOpportunityCategoryUI();
                    SetOpportunityTypeUI(oneDayModel.OpportunityType.ToString());
                    SetOrganisationTypeUI(oneDayModel.OpportunityType.ToString());
                    //Specialise:
                    SetApplicationMethodUI();
                    SetMethodReceiveApplicationUI();
                    //Fill model
                    return View("FullViewOneDayChallengeUpdate", oneDayModel);
                case JobTypeview.PlacementOrInternationalVolunteeringJob: SetOpportunityCategoryUI();

                    var placeModel = CeeePortalBusiness.GetPlacementOrInternationalVolunteeringJob(jobId);                   
                    
                    SetDurationMethodUI(placeModel.DurationNeeded);
                    SetOrganisationTypeUI(placeModel.OrganisationType);
                    SetOpportunityTypeUI(placeModel.OpportunityType.ToString());
                    
                    //Specialise:
                    SetApplicationMethodUI();
                    SetMethodReceiveApplicationUI();
                    return View("FullVolunteerPlacementUpdate", placeModel);
            }
            return RedirectToAction("UpdateJobDetails");
        }


        [HttpGet]
        [Authorize]
        public ActionResult UpdateJobDetails()
        {
            var clientID = CeeePortalBusiness.GetClientIdByName(User.Identity.Name);
            var model = CeeePortalBusiness.GetClientJobsByClientId(clientID);

            return View("UpdateJobDetails", model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateGraduateStudentJob(GraduateEmployerModel model)
        {
            try
            {
                var jobId = (int)Session["JobID"];
                var result = CeeePortalBusiness.UpdateGraduateOrStudentJob(model, jobId);
                if (result)
                {
                    return View("SuccessView");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateSandwichPlacementJob(SandwichPlacementViewModel model)
        {
            try
            {
                var jobId = (int)Session["JobID"];
                SetPositionsUI();
                var result = CeeePortalBusiness.UpdateSandwichOrPlacementJob(model, jobId);
                if (result)
                {
                    return View("SuccessView");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateOneDayGroupChallengeJob(OneDayChallengeCharityVolunteerViewModel model)
        {
            try
            {
                var jobId = (int)Session["JobID"];
                var result = CeeePortalBusiness.UpdateOnedDayChallengeOrCharityVolunteeringJob(model, jobId);
                if (result)
                {
                    return View("SuccessView");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateVolunteeringPlacmentJob(PlacementsInternationalVolunteerViewModel model)
        {
            try
            {
                var jobId = (int)Session["JobID"];
                var result = CeeePortalBusiness.UpdatePlacementOrInternationalVolunteeringJob(model, jobId);
                if (result)
                {
                    return View("SuccessView");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult LoadOpportunities()
        {
            return View("LoadOpportunities");
        }

        [HttpGet]
        [Authorize]
        public ActionResult StudentOrGraduateJobsForm(int? id)
        {
            ViewBag.MenuSelected = "Employers";
            if (id == null)
            {
                Response.Redirect("/Employer/LoadOpportunities");
            }
            Session["id"] = id;
            SetUpViewBagWithTitlesUI();

            //If logged in: Load defualt address and Company Name.
            var employment = (EmploymentStudentType)id;

            if (employment == EmploymentStudentType.Graduate)
            {
                ViewBag.EmploymentType = "Graduate Opportunity";
            }
            else
            {
                ViewBag.EmploymentType = "Student Opportunity";
            }

            return View("StudentOrGraduateJobsForm");
        }
        [HttpPost]
        [Authorize]
        public ActionResult StudentOrGraduateJobsForm(GraduateEmployerModel model)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("/Employer/LoadOpportunities");
            }
            var contractType = model.ContractType;
            var jobType = model.JobType;
            var clientID = CeeePortalBusiness.GetClientIdByName(User.Identity.Name);

            var employmentStudentType = model.EmploymentStudentType = (EmploymentStudentType)((int)Session["id"]);

            if (employmentStudentType == EmploymentStudentType.Graduate)
            {
                ViewBag.EmploymentType = "Graduate Opportunity";
                var actionResult = CeeePortalBusiness.SaveGraduateOrStudentJob(model, clientID);
                if (actionResult)
                {
                    return View("SuccessView");
                }
                else
                {
                    ModelState.AddModelError("Error", "An unknown problem occured, and Data not saved, please contact your system Administrator.");
                }
            }
            else
            {
                ViewBag.EmploymentType = "Student Opportunity";
                var actionResult = CeeePortalBusiness.SaveGraduateOrStudentJob(model, clientID);
                if (actionResult)
                {
                    //Notify Employment Services by email
                    var mailMessage = new MailMessage();
                    var physicalPath = Server.MapPath("~/MailTemplates/" + ConfigurationManager.AppSettings["TemplateName"]);


                    mailMessage.Body = mailTemplateService.LoadTemplateByName(physicalPath);
                    mailMessage.From = new MailAddress("info@uwl.ac.uk");
                    mailMessage.To.Add(new MailAddress("employment.services@uwl.ac.uk"));
                    mailMessage.To.Add(new MailAddress("martin.okello@uwl.ac.uk"));
                    mailMessage.Subject = string.Format("New Employer {0} has uploaded new {1}", model.OrganisationName, model.EmploymentStudentType.ToString());

                    try
                    {
                        mailService.SendMail(mailMessage);
                    }
                    catch (Exception excepion)
                    {

                    }
                    return View("SuccessView");
                }
                else
                {
                    ModelState.AddModelError("Error", "An unknown problem occured, and Data not saved, please contact your system Administrator.");
                }
            }


            return View("StudentOrGraduateJobsForm", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult SandwichPlacementsInternships()
        {
            ViewBag.EmploymentType = "Sandwich, Placements And Internships";
            SetPositionsUI();
            var Address =
                CeeePortalBusiness.GetClientDetailsByID(CeeePortalBusiness.GetClientIdByName(User.Identity.Name)).Address;
            if (Address != null)
            {
                var sandwichModel = new SandwichPlacementViewModel();
                sandwichModel.Address = new AddressModel();
                sandwichModel.Address.AddressLine1 = Address.AddressLine1;
                sandwichModel.Address.AddressLine2 = Address.AddressLine2;
                sandwichModel.Address.City = Address.City;
                sandwichModel.Address.Country = Address.Country;
                sandwichModel.Address.PostCode = Address.PostCode;

                return View("SandwichPlacementsInternships", sandwichModel);
            }
            else
            {
                return View("SandwichPlacementsInternships");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult SandwichPlacementsInternships(SandwichPlacementViewModel model)
        {
            ////Saving uploaded files:
            //
            //if (model.FileUploads != null && model.FileUploads.Length > 0)
            //{
            //    //there are files:
            //    foreach (var file in model.FileUploads)
            //    {
            //        //Store Bytes in DB  
            //        BinaryReader b = new BinaryReader(file.InputStream);
            //        byte[] binData = b.ReadBytes((int)file.InputStream.Length);

            //        //Or Store Bytes in FileSystem
            //        string path = @"D:\Temp\";
            //        file.SaveAs(path + file.FileName);
            //    }
            //}
            var clientID = CeeePortalBusiness.GetClientIdByName(User.Identity.Name);
            ViewBag.EmploymentType = "Sandwich, Placements And Internships";
            SetPositionsUI();
            var actionResult = CeeePortalBusiness.SaveSandwichOrPlacementJob(model, clientID);
            if (actionResult)
            {
                //Notify Employment Services by email
                var mailMessage = new MailMessage();
                var physicalPath = Server.MapPath("~/MailTemplates/" + ConfigurationManager.AppSettings["TemplateName"]);


                mailMessage.Body = mailTemplateService.LoadTemplateByName(physicalPath);
                mailMessage.From = new MailAddress("info@uwl.ac.uk");
                mailMessage.To.Add(new MailAddress("employment.services@uwl.ac.uk"));
                mailMessage.To.Add(new MailAddress("martin.okello@uwl.ac.uk"));
                mailMessage.Subject = string.Format("New Employer {0} has uploaded new {1}", CeeePortalBusiness.GetClientDetailsByID(clientID).OrganisationName, "Sandwich Placement Opportunity");

                try
                {
                    mailService.SendMail(mailMessage);
                }
                catch (Exception excepion)
                {

                }
                return View("SuccessView");
            }
            else
            {
                ModelState.AddModelError("Error", "An unknown problem occured, and Data not saved, please contact your system Administrator.");
                return View("SandwichPlacementsInternships");
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult Volunteering()
        {
            SetOpportunityCategoryUI();
            SetOpportunityTypeUI();
            SetOrganisationTypeUI();
            SetDurationMethodUI();

            //Specialise:
            SetApplicationMethodUI();
            SetMethodReceiveApplicationUI();

            return View("Volunteering");
        }
        [HttpPost]
        public ActionResult Volunteering(VolunteeringViewModel model)
        {
            var clientID = CeeePortalBusiness.GetClientIdByName(User.Identity.Name);
            SetOpportunityCategoryUI();
            SetOrganisationTypeUI(model.OpportunityType.ToString());
            SetOpportunityTypeUI(model.OpportunityType.ToString());
            SetDurationMethodUI();

            //Specialise:
            SetApplicationMethodUI();
            SetMethodReceiveApplicationUI();

            if (model.OpportunityCategoryType == null)
            {
                ModelState.AddModelError("OpportunityCategoryError", "Opportunity Category Type is required.");
                model.OpportunityCategoryType = new OpportunityCategoryType();
            }

            return View("Volunteering", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult VolunteeringPlacement()
        {
            return Volunteering();
        }
        [HttpPost]
        [Authorize]
        public ActionResult VolunteeringPlacement(PlacementsInternationalVolunteerViewModel model)
        {
            var clientID = CeeePortalBusiness.GetClientIdByName(User.Identity.Name);
            if (model.OpportunityCategoryType == null)
            {
                ModelState.AddModelError("OpportunityCategoryError", "Opportunity Category Type is required.");
                model.OpportunityCategoryType = new OpportunityCategoryType();
            }
            SetOpportunityCategoryUI();
            SetOrganisationTypeUI(model.OpportunityType.ToString());
            SetOpportunityTypeUI(model.OpportunityType.ToString());
            SetDurationMethodUI(model.DurationNeeded);

            //Specialise:
            SetApplicationMethodUI();
            SetMethodReceiveApplicationUI();
            var actionResult = CeeePortalBusiness.SavePlacementOrInternationalVolunteeringJob(model, clientID);

            if (actionResult)
            {
                //Notify Employment Services by email
                var mailMessage = new MailMessage();
                var physicalPath = Server.MapPath("~/MailTemplates/" + ConfigurationManager.AppSettings["TemplateName"]);


                mailMessage.Body = mailTemplateService.LoadTemplateByName(physicalPath);
                mailMessage.From = new MailAddress("info@uwl.ac.uk");
                mailMessage.To.Add(new MailAddress("employment.services@uwl.ac.uk"));
                mailMessage.To.Add(new MailAddress("martin.okello@uwl.ac.uk"));
                mailMessage.Subject = string.Format("New Employer {0} has uploaded new {1}", CeeePortalBusiness.GetClientDetailsByID(clientID).OrganisationName, "Volunteerring Placement or International Volunteering Placement");

                try
                {
                    mailService.SendMail(mailMessage);
                }
                catch (Exception excepion)
                {

                }
                return View("SuccessView");
            }
            else
            {
                ModelState.AddModelError("Error", "An unknown problem occured, and Data not saved, please contact your system Administrator.");

                return View("FullVolunteerPlacement", model);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult VolunteeringOneDayChallenge()
        {
            return Volunteering();
        }
        [HttpPost]
        [Authorize]
        public ActionResult VolunteeringOneDayChallenge(OneDayChallengeCharityVolunteerViewModel model)
        {
            var clientID = CeeePortalBusiness.GetClientIdByName(User.Identity.Name);
            if (model.OpportunityCategoryType == null)
            {
                ModelState.AddModelError("OpportunityCategoryError", "Opportunity Category Type is required.");
                model.OpportunityCategoryType = new OpportunityCategoryType();
            }

            SetOpportunityCategoryUI();
            SetOrganisationTypeUI(model.OrganisationType);
            SetOpportunityTypeUI(model.OpportunityType.ToString());
            SetDurationMethodUI();
            var actionResult = CeeePortalBusiness.SaveOnedDayChallengeOrCharityVolunteeringJob(model, clientID);

            if (actionResult)
            {
                //Notify Employment Services by email
                var mailMessage = new MailMessage();
                var physicalPath = Server.MapPath("~/MailTemplates/" + ConfigurationManager.AppSettings["TemplateName"]);


                mailMessage.Body = mailTemplateService.LoadTemplateByName(physicalPath);
                mailMessage.From = new MailAddress("info@uwl.ac.uk");
                mailMessage.To.Add(new MailAddress("employment.services@uwl.ac.uk"));
                mailMessage.To.Add(new MailAddress("martin.okello@uwl.ac.uk"));
                mailMessage.Subject = string.Format("New Employer {0} has uploaded new {1}", CeeePortalBusiness.GetClientDetailsByID(clientID).OrganisationName, "Volunteerring Placement or International Volunteering Placement");

                try
                {
                    mailService.SendMail(mailMessage);
                }
                catch (Exception excepion)
                {

                }
                return View("SuccessView");
            }
            else
            {
                ModelState.AddModelError("Error", "An unknown problem occured, and Data not saved, please contact your system Administrator.");

                return View("FullViewOneDayChallenge", model);
            }
        }

        [Authorize]
        public PartialViewResult GetVolunteeringViewtype(string volunteeringType)
        {
            SetDurationMethodUI();
            if (string.IsNullOrEmpty(volunteeringType)) volunteeringType = VolunteeringTypes.VolunteerPlacement.ToString();
            var voluntaryType = (VolunteeringTypes)Enum.Parse(typeof(VolunteeringTypes), volunteeringType);
            switch (voluntaryType)
            {
                case VolunteeringTypes.VolunteerPlacement:
                    return PartialView("_Partial_Volunteer_Placement_International");
                case VolunteeringTypes.InternationalVolunteering:
                    return PartialView("_Partial_Volunteer_Placement_International");
                case VolunteeringTypes.OneDayGroupVolunteerChallenge:
                    return PartialView("_Partial_Volunteer_OneDayGroupCharityVolunteerChallenge");
                case VolunteeringTypes.CharityFundraisingEvent:
                    return PartialView("_Partial_Volunteer_OneDayGroupCharityVolunteerChallenge");
                default:
                    return PartialView("_Partial_Volunteer_Placement_International");

            }
        }

        private void SetDurationMethodUI()
        {
            ViewBag.DurationN = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text = "0-1 months", Value = "UptoOneMonth"},
                    new SelectListItem {Selected = false, Text = "1-3 months", Value = "OneTo3Months"},
                    new SelectListItem {Selected = false, Text = "1-6 months", Value = "OneTo6Months"},
                    new SelectListItem {Selected = false, Text = "6+ months", Value = "SixMonthsPlus"}
                };
        }

        private void SetDurationMethodUI(string selectedValue)
        {
            if (!string.IsNullOrEmpty(selectedValue))
                ViewBag.DurationN = new List<SelectListItem>
                    {
                        new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                        new SelectListItem
                            {
                                Selected = selectedValue.Equals("UptoOneMonth"),
                                Text = "0-1 months",
                                Value = "UptoOneMonth"
                            },
                        new SelectListItem
                            {
                                Selected = selectedValue.Equals("OneTo3Months"),
                                Text = "1-3 months",
                                Value = "OneTo3Months"
                            },
                        new SelectListItem
                            {
                                Selected = selectedValue.Equals("OneTo6Months"),
                                Text = "1-6 months",
                                Value = "OneTo6Months"
                            },
                        new SelectListItem
                            {
                                Selected = selectedValue.Equals("SixMonthsPlus"),
                                Text = "6+ months",
                                Value = "SixMonthsPlus"
                            }
                    };
            else SetDurationMethodUI();

        }
        private void SetApplicationMethodUI()
        {
            ViewBag.ApplicationM = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text = "CV", Value = "CV"},
                    new SelectListItem {Selected = false, Text = "Application Letter", Value = "ApplicationLetter"},
                    new SelectListItem {Selected = false, Text = "Application Form", Value = "ApplicationForm"}
                };
        }

        private void SetApplicationMethodUI(string selectedValue)
        {
            if (!string.IsNullOrEmpty(selectedValue))
                ViewBag.ApplicationM = new List<SelectListItem>
                    {
                        new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                        new SelectListItem {Selected = selectedValue.Equals("CV"), Text = "CV", Value = "CV"},
                        new SelectListItem
                            {
                                Selected = selectedValue.Equals("ApplicationLetter"),
                                Text = "Application Letter",
                                Value = "ApplicationLetter"
                            },
                        new SelectListItem
                            {
                                Selected = selectedValue.Equals("ApplicationForm"),
                                Text = "Application Form",
                                Value = "ApplicationForm"
                            }
                    };
            else SetApplicationMethodUI();
        }
        private void SetMethodReceiveApplicationUI()
        {
            ViewBag.MethodReceiveA = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text = "Voluntary Body", Value = "Email"},
                    new SelectListItem {Selected = false, Text = "Registered Charity", Value = "Website"}
                };
        }

        private void SetMethodReceiveApplicationUI(string selectedValue)
        {

            if (!string.IsNullOrEmpty(selectedValue))
            ViewBag.MethodReceiveA = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = selectedValue.Equals("Email"), Text = "Voluntary Body", Value = "Email"},
                    new SelectListItem {Selected = selectedValue.Equals("Website"), Text = "Registered Charity", Value = "Website"}
                };
            else SetMethodReceiveApplicationUI();
        }
        private void SetOrganisationTypeUI()
        {
            ViewBag.OrganisationT = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text = "Voluntary Body", Value = "VoluntaryBody"},
                    new SelectListItem {Selected = false, Text = "Registered Charity", Value = "RegisteredCharity"},
                    new SelectListItem {Selected = false, Text = "Social Enterprise", Value = "SocialEnterprise"},
                    new SelectListItem {Selected = false, Text = "Government", Value = "Government"},
                    new SelectListItem {Selected = false, Text = "Other", Value = "Other"}
                };
        }

        private void SetOrganisationTypeUI(string selectedValue)
        {
            if (!string.IsNullOrEmpty(selectedValue))
            ViewBag.OrganisationT = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = selectedValue.Equals("VoluntaryBody"), Text = "Voluntary Body", Value = "VoluntaryBody"},
                    new SelectListItem {Selected = selectedValue.Equals("RegisteredCharity"), Text = "Registered Charity", Value = "RegisteredCharity"},
                    new SelectListItem {Selected = selectedValue.Equals("SocialEnterprise"), Text = "Social Enterprise", Value = "SocialEnterprise"},
                    new SelectListItem {Selected = selectedValue.Equals("Government"), Text = "Government", Value = "Government"},
                    new SelectListItem {Selected = selectedValue.Equals("Other"), Text = "Other", Value = "Other"}
                };
            else SetOrganisationTypeUI();
        }
        private void SetOpportunityTypeUI()
        {
            ViewBag.OpportunityT = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text = "Volunteer placement", Value = VolunteeringTypes.VolunteerPlacement.ToString()},
                    new SelectListItem {Selected = false, Text = "One Day Group Volunteer Challenge ", Value = VolunteeringTypes.OneDayGroupVolunteerChallenge.ToString()},
                    new SelectListItem {Selected = false, Text = "International Volunteering", Value = VolunteeringTypes.InternationalVolunteering.ToString()},
                    new SelectListItem {Selected = false, Text = "Charity Fundraising Events", Value = VolunteeringTypes.CharityFundraisingEvent.ToString()}
                };
        }

        private void SetOpportunityTypeUI(string selectedValue)
        {
            if (!string.IsNullOrEmpty(selectedValue))
            ViewBag.OpportunityT = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = selectedValue.Equals(VolunteeringTypes.VolunteerPlacement.ToString()), Text = "Volunteer placement", Value = VolunteeringTypes.VolunteerPlacement.ToString()},
                    new SelectListItem {Selected = selectedValue.Equals(VolunteeringTypes.OneDayGroupVolunteerChallenge.ToString()), Text = "One Day Group Volunteer Challenge", Value = VolunteeringTypes.OneDayGroupVolunteerChallenge.ToString()},
                    new SelectListItem {Selected = selectedValue.Equals(VolunteeringTypes.InternationalVolunteering.ToString()), Text = "International Volunteering", Value = VolunteeringTypes.InternationalVolunteering.ToString()},
                    new SelectListItem {Selected = selectedValue.Equals(VolunteeringTypes.CharityFundraisingEvent.ToString()), Text = "Charity Fundraising Events", Value = VolunteeringTypes.CharityFundraisingEvent.ToString()}
                };
            else SetOpportunityTypeUI();
        }
        private void SetOpportunityCategoryUI()
        {
            ViewBag.OpportunityC = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text = "Category1", Value = "Category1"},
                    new SelectListItem {Selected = false, Text = "Category2", Value = "Category2"},
                    new SelectListItem {Selected = false, Text = "Category3", Value = "Category3"},
                    new SelectListItem {Selected = false, Text = "Category4", Value = "Category4"},
                    new SelectListItem {Selected = false, Text = "Category5", Value = "Category5"}
                };
        }

        private void SetPositionsUI()
        {
            ViewBag.Positions = new List<SelectListItem>
                {
                    new SelectListItem {Selected = false, Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text = "Position1", Value = "Position1"},
                    new SelectListItem {Selected = false, Text = "Position2", Value = "Position2"},
                    new SelectListItem {Selected = false, Text = "Position3", Value = "Position3"},
                    new SelectListItem {Selected = false, Text = "Position4", Value = "Position4"},
                    new SelectListItem {Selected = false, Text = "Position5", Value = "Position5"}
                };
        }
        private void SetUpViewBagWithTitlesUI()
        {
            ViewBag.Tit = new List<SelectListItem>
                {
                    new SelectListItem{Selected = false,Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text="Mr", Value="Mr"},
                    new SelectListItem {Selected = false,Text= "Ms", Value="Ms"},
                    new SelectListItem {Selected = false, Text= "Miss", Value="Miss"},
                    new SelectListItem {Selected = false, Text= "Mrs", Value="Mrs"},
                    new SelectListItem {Selected = false, Text= "Dr",Value= "Dr"}
                };
        }

        private void SetUpViewBagWithTitlesUI(string selectedValue)
        {
            if (!string.IsNullOrEmpty(selectedValue))
            ViewBag.Tit = new List<SelectListItem>
                {
                    new SelectListItem{Selected = false,Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = selectedValue.Equals("Mr"), Text="Mr", Value="Mr"},
                    new SelectListItem {Selected = selectedValue.Equals("Ms"),Text= "Ms", Value="Ms"},
                    new SelectListItem {Selected = selectedValue.Equals("Miss"), Text= "Miss", Value="Miss"},
                    new SelectListItem {Selected = selectedValue.Equals("Mrs"), Text= "Mrs", Value="Mrs"},
                    new SelectListItem {Selected = selectedValue.Equals("Dr"), Text= "Dr",Value= "Dr"}
                };
            else SetUpViewBagWithTitlesUI();
        }
        private void SetUpViewBagWithCompanySizeUI()
        {
            ViewBag.CompanyS = new List<SelectListItem>
                {
                    new SelectListItem{Selected = false,Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text="1 - 10", Value="1-10"},
                    new SelectListItem {Selected = false,Text= "10 - 49", Value="10-49"},
                    new SelectListItem {Selected = false, Text= "50 - 249", Value="50-249"},
                    new SelectListItem {Selected = false, Text= "250+", Value="250+"}
                };
        }

        private void SetUpViewBagWithCompanySizeUI(string selectedValue)
        {
            if (!string.IsNullOrEmpty(selectedValue))
            ViewBag.CompanyS = new List<SelectListItem>
                {
                    new SelectListItem{Selected = false,Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = selectedValue.Equals("1-10"), Text="1 - 10", Value="1-10"},
                    new SelectListItem {Selected = selectedValue.Equals("10-49"),Text= "10 - 49", Value="10-49"},
                    new SelectListItem {Selected = selectedValue.Equals("50-249"), Text= "50 - 249", Value="50-249"},
                    new SelectListItem {Selected = selectedValue.Equals("250"), Text= "250+", Value="250+"}
                };
            else SetUpViewBagWithCompanySizeUI();
        }

        private void SetUpViewBagWithHowHeardUI()
        {
            ViewBag.HowH = new List<SelectListItem>
                {
                    new SelectListItem{Selected = false,Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = false, Text="Direct Marketing", Value="DM"},
                    new SelectListItem {Selected = false,Text= "Facebook", Value="FBK"},
                    new SelectListItem {Selected = false, Text= "Google Search", Value="GOOGLE"},
                    new SelectListItem {Selected = false, Text= "Twitter", Value="TWITTER"},
                    new SelectListItem {Selected = false, Text= "University Staff Member", Value="UNISTAFF"},
                    new SelectListItem {Selected = false, Text= "UWL Alumi", Value="UWLALUMNI"},
                    new SelectListItem {Selected = false, Text= "Word Of Mouth", Value="WOFMOUTH"},
                    new SelectListItem {Selected = false, Text= "Other", Value="OTHER"}
                };
        }

        private void SetUpViewBagWithHowHeardUI(string selectedValue)
        {
            if (!string.IsNullOrEmpty(selectedValue))
            ViewBag.HowH = new List<SelectListItem>
                {
                    new SelectListItem{Selected = false,Text = "<<Choose>>", Value = ""},
                    new SelectListItem {Selected = selectedValue.Equals("DM"), Text="Direct Marketing", Value="DM"},
                    new SelectListItem {Selected = selectedValue.Equals("FBK"),Text= "Facebook", Value="FBK"},
                    new SelectListItem {Selected = selectedValue.Equals("GOOGLE"), Text= "Google Search", Value="GOOGLE"},
                    new SelectListItem {Selected = selectedValue.Equals("TWITTER"), Text= "Twitter", Value="TWITTER"},
                    new SelectListItem {Selected = selectedValue.Equals("UNISTAFF"), Text= "University Staff Member", Value="UNISTAFF"},
                    new SelectListItem {Selected = selectedValue.Equals("UWLALUMNI"), Text= "UWL Alumi", Value="UWLALUMNI"},
                    new SelectListItem {Selected = selectedValue.Equals("WOFMOUTH"), Text= "Word Of Mouth", Value="WOFMOUTH"},
                    new SelectListItem {Selected = selectedValue.Equals("OTHER"), Text= "Other", Value="OTHER"}
                };
            SetUpViewBagWithHowHeardUI();
        }
    }
}
