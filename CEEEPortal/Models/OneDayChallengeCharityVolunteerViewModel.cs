using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEEEPortal.Models
{
    public class OneDayChallengeCharityVolunteerViewModel
    {
        //main view:
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

        //Second view:
        [Required(ErrorMessage = "Project/Event title required")]
        public string ProjectEventTitle { get; set; }
        [Required(ErrorMessage = "Location required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "When is required")]
        public string When { get; set; }
        [Required(ErrorMessage = "Meeting Time required")]
        public string MeetingTime { get; set; }
        [Required(ErrorMessage = "Finishing Time required")]
        public string FinishingTime { get; set; }
        [Required(ErrorMessage = "Meeting point required")]
        public string MeetingPoint { get; set; }
        [Required(ErrorMessage = "What will we be doing required")]
        public string WhatWillBeDoing { get; set; }
        [Required(ErrorMessage = "Experience needed required")]
        public string ExperienceNeeded { get; set; }
        public string IsCordinatedByUniversity { get; set; }
        public string IsNotCordinatedByUniversity { get; set; }
        public HttpPostedFileBase RiskAssesmentFormFile { get; set; }
        public HttpPostedFileBase SessionPlanFile { get; set; }
        public HttpPostedFileBase BudgetFormFile { get; set; }
        public string HasInsuranceLiability { get; set; }
        public string HasNoInsuranceLiability { get; set; }
        public string HasCarriedOutRiskAssesment { get; set; }
        public string HasNotCarriedOutRiskAssesment { get; set; }
    }
}