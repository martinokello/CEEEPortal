using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CEEEPortal.Models
{
    public class GraduateEmployerModel
    {
        [Required(ErrorMessage = "Job title required")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Job Type required")]
        public JobType? JobType { get; set; }
        [Required(ErrorMessage = "Contract Type required")]
        public ContractType? ContractType { get; set; }
        public string ContractTypeOther { get; set; }
        [Required(ErrorMessage = "Number of opportunities required")]
        public int? NumberOfOpportunities { get; set; }
        [Required(ErrorMessage = "Start date required")]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "End date required")]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ClosingDate { get; set; }
        [Required(ErrorMessage = "Salary required")]
        public decimal? Salary { get; set; }
        public string SalaryPeriod { get; set; }
        [Required(ErrorMessage = "Opportunity description required")]
        public string OpportunityDescription { get; set; }
        [Required(ErrorMessage = "How employees approach required")]
        public string HowEmployeeApproaches { get; set; }
        public EmploymentStudentType? EmploymentStudentType { get; set; }
        public string OrganisationName { get; set; }
    }

    public enum JobType{ FullTime, PartTime}
    public enum ContractType { Permanent, Fixed, CasualZeroHour, SelfEmployed, Other}
    public enum EmploymentStudentType{ Graduate, Student}
   

}