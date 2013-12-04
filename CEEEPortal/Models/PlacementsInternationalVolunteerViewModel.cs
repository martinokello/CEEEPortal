using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEEEPortal.Models
{
    public class PlacementsInternationalVolunteerViewModel
    {
        [Required(ErrorMessage = "Contact Name required")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Contact Email required")]
        public string ContactEmail { get; set; }
        public string AlternateEmail { get; set; }
        public OpportunityCategoryType OpportunityCategoryType { get; set; }
        [Required(ErrorMessage = "Organisation type required")]
        public string OrganisationType { get; set; }
        [Required(ErrorMessage = "Opportunity type required")]
        public VolunteeringTypes OpportunityType { get; set; }

        [Required(ErrorMessage = "Role title required")]
        public string RoleTitle { get; set; }
        [Required(ErrorMessage = "Location required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Time of day required")]
        public string DayTimeRequired { get; set; }
        [Required(ErrorMessage = "Duration required")]
        public string DurationNeeded { get; set; }
        [Required(ErrorMessage = "Skills required")]
        public string SkillsRequired { get; set; }
        [Required(ErrorMessage = "Training details required")]
        public string TrainingDetails { get; set; }
        [Required(ErrorMessage = "About Organisation required")]
        public string AboutOrganisation { get; set; }
        public string ApplicationMethodCv { get; set; }
        public string ApplicationMethodCoverLetter { get; set; }
        public string ApplicationMethodForm { get; set; }
        public string ApplicationReceiveWebsite { get; set; }
        public string EmailReceipt { get; set; }
        public string ApplicationReceiveEmail { get; set; }
        public string WebSiteReciept { get; set; }
        [Required(ErrorMessage = "Number of vacancies required")]
        public int? NumberOfVacancies { get; set; }
        public HttpPostedFileBase RoleDescriptionFile { get; set; }
        public HttpPostedFileBase ApplicationFormFile { get; set; }
        public string HasInsuranceLiability { get; set; }
        public string HasNoInsuranceLiability { get; set; }
        public string HasCarriedOutRiskAssesment { get; set; }
        public string HasNotCarriedOutRiskAssesment { get; set; }
    }
}