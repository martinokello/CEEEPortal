using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CEEEPortal.Models
{
    public class MentorViewModel
    {
        [Required(ErrorMessage = "StudentID required")]
        [RegularExpression("([0-9]+)",ErrorMessage = "Student ID should be digits")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Course Code required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Course code should be digits")]
        public string CourseCode { get; set; }
        [Required(ErrorMessage = "Student Name required")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Course Name required")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Year Of Study required")]
        public int? YearOfStudy { get; set; }
        [Required(ErrorMessage = "UWL email required")]
        [EmailAddress(ErrorMessage = "UWL email should be in correct format")]
        public string UWLEmail { get; set; }
        [Required(ErrorMessage = "Personal email required")]
        [EmailAddress(ErrorMessage = "Personal email should be in correct format")]
        public string PersonalEmail { get; set; }
        [Required(ErrorMessage = "PhoneNo required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Phone should be digits")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Preferred contact required")]
        public PrefContactMethod PrefContactMethod { get; set; }
        public string IsRegWithVolunteering { get; set; }
        public string IsMentor { get; set; }
        public string IsMentee { get; set; }
        public string IsDropInUser { get; set; }
        public string RequestMentor { get; set; }
        [Required(ErrorMessage = "Gender required")]
        public Gender Gender { get; set; }
        public string HasTrained { get; set; }

        public MentorType MentorType { get; set; }
        public string Mentees { get; set; }
        public string ContactMenteeHours { get; set; }
        public double? TrainingHours { get; set; }
        public double? AdditionalMentoringHours { get; set; }
        public int? VisitsToDropinCentre { get; set; }
        public string UsedDropInCentre { get; set; }
    }

    public enum PrefContactMethod{ UWLEmail, PersonalEmail}
    public enum Gender{Male, Female}
    public class MentorType
    {
        public string PeerMentor { get; set; }
        public string GraduateMentor { get; set; }
        public string DisabilityMentor { get; set; }
        public string InternationalMentor { get; set; }
        public string HigherEducationMentor { get; set; }
    }
}