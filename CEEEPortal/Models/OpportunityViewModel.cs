using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEEEPortal.Models
{
    public class OpportunityViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public Title Title { get; set; }
        [Required(ErrorMessage = "Organisation name is required")]
        public string OrganisationName { get; set; }
        public AddressModel Address { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Job title is required")]
        public string JobTitle { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Landline should be digits")]
        [Required(ErrorMessage = "Landline number is required")]
        public string LandlineNo { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Mobile No should be digits")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email should be in proper format")]
        public string Email { get; set; }
        public string OrgWebSite { get; set; }
        [Required(ErrorMessage = "Opportunity description is required")]
        public string OpportunityDescription { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public OpportunityType OpportunityType { get; set; }
    }

    public enum EmploymentType
    {
        EmploymentOpportunity,
        Placement
    };
}