using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEEEPortal.Models
{

    public class AddressModel
    {
        [Required(ErrorMessage = "Address Line 1 is required")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Post Code is required")]
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}