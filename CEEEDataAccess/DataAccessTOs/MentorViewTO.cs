using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CEEEPortal.DataAccessTOs
{
    public class MentorViewTO
    {
        public int StudentId { get; set; }
        public string CourseCode { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public int? YearOfStudy { get; set; }
        public string UWLEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string PhoneNo { get; set; }
        public PrefContactMethod PrefContactMethod { get; set; }
        public string IsRegWithVolunteering { get; set; }
        public string IsMentor { get; set; }
        public string IsMentee { get; set; }
        public string IsDropInUser { get; set; }
        public string RequestMentor { get; set; }
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