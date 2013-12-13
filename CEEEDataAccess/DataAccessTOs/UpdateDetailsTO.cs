using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class UpdateDetailsTO
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
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public string OrgWebSite { get; set; }
        public string BusinessRegNumber { get; set; }
        public string CharityRegNumber { get; set; }

        public string CompanySize { get; set; }
        public string HowHeard { get; set; }

        public OpportunityTypeTO OpportunityType { get; set; }
    }

}