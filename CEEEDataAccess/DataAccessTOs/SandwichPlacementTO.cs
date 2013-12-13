using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class SandwichPlacementTO
    {
        public string JobRoleTitle { get; set; }
        public string JobType2 { get; set; }
        public string Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? NumberOfHoursWeekly { get; set; }
        public int? NumberOfPositions { get; set; }
        public decimal? Salary { get; set; }
        public AddressTO Address { get; set; }
        public string OpportunityDescription { get; set; }
        public List<byte[]> FileUploads { get; set; }
        public DateTime? ClosingDate { get; set; }
        public List<string> Filenames { get; set; } 
    }
}