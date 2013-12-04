using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CEEEPortal.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email should be in proper format")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "*")]
        //[Display(Name = "User name")]
        //public string Username { get; set; }
    }
}