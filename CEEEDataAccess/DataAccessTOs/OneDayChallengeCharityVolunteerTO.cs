using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class OneDayChallengeCharityVolunteerTO
    {
        //main view:
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string AlternateEmail { get; set; }
        public OpportunityCategoryType OpportunityCategoryType { get; set; }
        public string OrganisationType { get; set; }
        public VolunteeringTypes OpportunityType { get; set; }
        public string JobType2 { get; set; }
        //Second view:
        public string ProjectEventTitle { get; set; }
        public string Location { get; set; }
        public string When { get; set; }
        public string MeetingTime { get; set; }
        public string FinishingTime { get; set; }
        public string MeetingPoint { get; set; }
        public string WhatWillBeDoing { get; set; }
        public string ExperienceNeeded { get; set; }
        public string IsCordinatedByUniversity { get; set; }
        public string IsNotCordinatedByUniversity { get; set; }
        public byte[] RiskAssesmentFormFile { get; set; }
        public string RiskAssesmentFormFileName { get; set; }
        public byte[] SessionPlanFile { get; set; }
        public string SessionPlanFileName { get; set; }
        public byte[] BudgetFormFile { get; set; }
        public string BudgetFormFileName { get; set; }
        public string HasInsuranceLiability { get; set; }
        public string HasNoInsuranceLiability { get; set; }
        public string HasCarriedOutRiskAssesment { get; set; }
        public string HasNotCarriedOutRiskAssesment { get; set; }
    }
}