using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class PlacementsInternationalVolunteerTO
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string AlternateEmail { get; set; }
        public OpportunityCategoryType OpportunityCategoryType { get; set; }
        public string OrganisationType { get; set; }
        public VolunteeringTypes OpportunityType { get; set; }
        public string JobType2 { get; set; }

        public string RoleTitle { get; set; }
        public string Location { get; set; }
        public string DayTimeRequired { get; set; }
        public string DurationNeeded { get; set; }
        public string SkillsRequired { get; set; }
        public string TrainingDetails { get; set; }
        public string AboutOrganisation { get; set; }
        public string ApplicationMethodCv { get; set; }
        public string ApplicationMethodCoverLetter { get; set; }
        public string ApplicationMethodForm { get; set; }
        public byte[] ApplicationFormFile { get; set; }
        public string ApplicationFormFileName { get; set; }
        public string ApplicationReceiveWebsite { get; set; }
        public string EmailReceipt { get; set; }
        public string ApplicationReceiveEmail { get; set; }
        public string WebSiteReciept { get; set; }
        public int? NumberOfVacancies { get; set; }
        public byte[] RoleDescriptionFile { get; set; }
        public string HasInsuranceLiability { get; set; }
        public string HasNoInsuranceLiability { get; set; }
        public string HasCarriedOutRiskAssesment { get; set; }
        public string HasNotCarriedOutRiskAssesment { get; set; }
    }
}