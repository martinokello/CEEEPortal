//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CEEEDataAccess.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class CeeeUser
    {
        public CeeeUser()
        {
            this.CeeeOpportunityTypes = new HashSet<CeeeOpportunityType>();
            this.webpages_Roles = new HashSet<webpages_Roles>();
        }
    
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Phone { get; set; }
        public string JobTitle { get; set; }
        public string OrgWebsite { get; set; }
        public string CompanySize { get; set; }
        public string HowHeard { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
    
        public virtual CeeeClientAttribute CeeeClientAttribute { get; set; }
        public virtual ICollection<CeeeOpportunityType> CeeeOpportunityTypes { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}
