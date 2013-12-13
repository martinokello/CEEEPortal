using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class GraduateEmployerTO
    {
        public string JobTitle { get; set; }
        public JobType? JobType { get; set; }
        public string JobType2 { get; set; }
        public ContractType? ContractType { get; set; }
        public string ContractTypeOther { get; set; }
        public int? NumberOfOpportunities { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public decimal? Salary { get; set; }
        public string SalaryPeriod { get; set; }
        public string OpportunityDescription { get; set; }
        public string HowEmployeeApproaches { get; set; }
        public EmploymentStudentType? EmploymentStudentType { get; set; }
        public string OrganisationName { get; set; }
    }

    public enum JobType{ FullTime, PartTime}
    public enum ContractType { Permanent, Fixed, CasualZeroHour, SelfEmployed, Other}
    public enum EmploymentStudentType{ Graduate, Student}
   

}