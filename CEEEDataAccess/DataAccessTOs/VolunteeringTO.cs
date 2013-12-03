using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class VolunteeringTO
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string AlternateEmail { get; set; }
        public OpportunityCategoryType OpportunityCategoryType { get; set; }
        public string OrganisationType { get; set; }
        public VolunteeringTypes OpportunityType { get; set; }
    }

    public class OpportunityCategoryType
    {
        public string ChildrenYouth { get; set; }
        public string VulnerableAdults { get; set; }
        public string Animals { get; set; }
        public string Environment { get; set; }
        public string CultureHeritage { get; set; }
        public string EthnicityReligion { get; set; }
        public string HealthSocialCare { get; set; }
        public string Education { get; set; }
        public string Politics { get; set; }
        public string MentoringAdvice { get; set; }
        public string LawCriminalJustice { get; set; }
        public string IT { get; set; }
        public string GovernanceFinance { get; set; }
        public string MediaCreative { get; set; }
        public string Administration { get; set; }
        public string SocialEnterprise { get; set; }
        public string InternationalDevelopment { get; set; }
        public string EventProjectManagement { get; set; }
        public string SportsOutdoor { get; set; }
    }
    public enum VolunteeringTypes{ VolunteerPlacement, OneDayGroupVolunteerChallenge, InternationalVolunteering, CharityFundraisingEvent}
}