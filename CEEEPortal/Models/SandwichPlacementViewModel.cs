using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEEEPortal.Models
{
    public class SandwichPlacementViewModel
    {
        [Required(ErrorMessage = "Job/Role title required")]
        public string JobRoleTitle { get; set; }
        [Required(ErrorMessage = "Position required")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Start Date required")]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "End Date required")]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Number of weekly hours required")]
        public decimal? NumberOfHoursWeekly { get; set; }
        [Required(ErrorMessage = "Number of positions required")]
        public int? NumberOfPositions { get; set; }
        public decimal? Salary { get; set; }
        public AddressModel Address { get; set; }
        [Required(ErrorMessage = "Opportunity description required")]
        public string OpportunityDescription { get; set; }
        [DataType(DataType.DateTime)]
        public HttpPostedFileBase[] FileUploads { get; set; }
        public DateTime? ClosingDate { get; set; }
    }
}