using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class OpportunityTO
    {
        public Title Title { get; set; }
        public string OrganisationName { get; set; }
        public AddressTO Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string LandlineNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string OrgWebSite { get; set; }
        public string OpportunityDescription { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public OpportunityTypeTO OpportunityType { get; set; }
    }

    public enum EmploymentType
    {
        EmploymentOpportunity,
        Placement
    };
}