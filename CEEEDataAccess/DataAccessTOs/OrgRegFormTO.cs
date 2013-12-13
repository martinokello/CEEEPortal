using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class OrgRegFormTO
    {
        public string OrganisationName { get; set; }
        public AddressTO Address { get; set; }
        public Title Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string OrganisationDoes { get; set; }
        public string LandLineNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int UserId { get; set; }
        public string OrgWebSite { get; set; }
        public string BusinessRegNumber { get; set; }
        public string CharityRegNumber { get; set; }

        public string CompanySize { get; set; }
        public string HowHeard { get; set; }

        public OpportunityTypeTO OpportunityType { get; set; }
    }

    public enum Title
    {
        Mr,
        Miss,
        Ms,
        Mrs,
        Dr
    }

}