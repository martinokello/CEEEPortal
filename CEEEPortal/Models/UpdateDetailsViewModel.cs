using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEEEPortal.Models
{
    public class UpdateDetailsViewModel
    {        
        [Required(ErrorMessage = "Organisation name is required")]
        public string OrganisationName { get; set; }
        public AddressModel Address { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public Title Title { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "What organisation description is required")]
        [StringLength(800,ErrorMessage = "Maximum characters 800")]
        public string OrganisationDoes { get; set; }
        [Required(ErrorMessage = "Land line number is required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Landline should be digits")]
        public string LandLineNo { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Mobile No. should be digits")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email should be in proper format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(10, ErrorMessage = "Email should be in proper format", MinimumLength = 8)]
        [Compare("NewPassword",ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string OrgWebSite { get; set; }
        public string BusinessRegNumber { get; set; }
        public string CharityRegNumber { get; set; }

        [Required(ErrorMessage = "Organisation size is required")]
        public string CompanySize { get; set; }
        [Required(ErrorMessage = "How did you hear is required")]
        public string HowHeard { get; set; }
        public int UserId { get; set; }

        public OpportunityType OpportunityType { get; set; }
    }

    public enum JobTypeview { GraduateOrStudentJob = 1, SandwichOrPlacementJob = 2, OnedDayChallengeOrCharityVolunteeringJob = 3, PlacementOrInternationalVolunteeringJob = 4}
}