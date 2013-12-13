using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CEEEDataAccess.DataAccessTOs;
using CEEEDataAccess.Repository.Interfaces;
using CEEEPortal.DataAccessTOs;
using CEEEPortal.Models;
using CEEEDataAccess.Repository;
using OpportunityCategoryType = CEEEPortal.Models.OpportunityCategoryType;

namespace CEEEPortal.Business
{
    public class CeeePortalBusiness
    {
        private static IRepository ceeeRepository = new EmployerPortalRepository();

        public static bool CheckUserNotExists(string email)
        {
            return ceeeRepository.CheckUserNotExists(email);
        }

        public static int GetJobTypeById(int jobId)
        {
            return (int)ceeeRepository.GetJobTypeById(jobId);
        }
        public static bool SaveClientDetails(OrgRegFormViewModel orgDetails)
        {
            var orgRegFormTO = new OrgRegFormTO();
            orgRegFormTO.Address = new AddressTO
                {
                    AddressLine1 = orgDetails.Address.AddressLine1,
                    AddressLine2 = orgDetails.Address.AddressLine2,
                    City = orgDetails.Address.City,
                    Country = orgDetails.Address.Country,
                    PostCode = orgDetails.Address.PostCode
                };
            orgRegFormTO.BusinessRegNumber = orgDetails.BusinessRegNumber;
            orgRegFormTO.CharityRegNumber = orgDetails.CompanySize;
            orgRegFormTO.CompanySize = orgDetails.CompanySize;
            orgRegFormTO.Email = orgDetails.Email;
            orgRegFormTO.FirstName = orgDetails.FirstName;
            orgRegFormTO.LastName = orgDetails.LastName;
            orgRegFormTO.HowHeard = orgDetails.HowHeard;
            orgRegFormTO.JobTitle = orgDetails.JobTitle;
            orgRegFormTO.LandLineNo = orgDetails.LandLineNo;
            orgRegFormTO.MobileNo = orgDetails.MobileNo;
            orgRegFormTO.UserId = orgDetails.UserId;
            orgRegFormTO.OpportunityType = new OpportunityTypeTO
                {
                    AirLineAndAirport = orgDetails.OpportunityType.AirLineAndAirport,
                    Education = orgDetails.OpportunityType.Education,
                    Hospitality = orgDetails.OpportunityType.Hospitality,
                    Other = orgDetails.OpportunityType.Other,
                    RecruitmentAgency = orgDetails.OpportunityType.RecruitmentAgency,
                    SportAndLeisure = orgDetails.OpportunityType.SportAndLeisure
                };

            orgRegFormTO.OrgWebSite = orgDetails.OrgWebSite;
            orgRegFormTO.OrganisationDoes = orgDetails.OrganisationDoes;
            orgRegFormTO.OrganisationName = orgDetails.OrganisationName;
            orgRegFormTO.Title =
                (DataAccessTOs.Title)Enum.Parse(typeof(DataAccessTOs.Title), orgDetails.Title.ToString());
            return ceeeRepository.SaveClientDetails(orgRegFormTO);

        }

        public static bool UpdateClientDetails(UpdateDetailsViewModel orgDetails)
        {
            var orgRegFormTO = new OrgRegFormTO();
            orgRegFormTO.Address = new AddressTO
                {
                    AddressLine1 = orgDetails.Address.AddressLine1,
                    AddressLine2 = orgDetails.Address.AddressLine2,
                    City = orgDetails.Address.City,
                    Country = orgDetails.Address.Country,
                    PostCode = orgDetails.Address.PostCode
                };
            orgRegFormTO.BusinessRegNumber = orgDetails.BusinessRegNumber;
            orgRegFormTO.CharityRegNumber = orgDetails.CompanySize;
            orgRegFormTO.CompanySize = orgDetails.CompanySize;
            orgRegFormTO.Email = orgDetails.Email;
            orgRegFormTO.FirstName = orgDetails.FirstName;
            orgRegFormTO.LastName = orgDetails.LastName;
            orgRegFormTO.HowHeard = orgDetails.HowHeard;
            orgRegFormTO.JobTitle = orgDetails.JobTitle;
            orgRegFormTO.LandLineNo = orgDetails.LandLineNo;
            orgRegFormTO.MobileNo = orgDetails.MobileNo;
            orgRegFormTO.OpportunityType = new OpportunityTypeTO
                {
                    AirLineAndAirport = orgDetails.OpportunityType.AirLineAndAirport,
                    Education = orgDetails.OpportunityType.Education,
                    Hospitality = orgDetails.OpportunityType.Hospitality,
                    Other = orgDetails.OpportunityType.Other,
                    RecruitmentAgency = orgDetails.OpportunityType.RecruitmentAgency,
                    SportAndLeisure = orgDetails.OpportunityType.SportAndLeisure
                };

            orgRegFormTO.OrgWebSite = orgDetails.OrgWebSite;
            orgRegFormTO.OrganisationDoes = orgDetails.OrganisationDoes;
            orgRegFormTO.OrganisationName = orgDetails.OrganisationName;
            orgRegFormTO.UserId = orgDetails.UserId;
            orgRegFormTO.Title =
                (DataAccessTOs.Title)Enum.Parse(typeof(DataAccessTOs.Title), orgDetails.Title.ToString());
            return ceeeRepository.UpdateClientDetails(orgRegFormTO);
        }

        public static IEnumerable<JobUpdateTO> GetClientJobsByClientId(int clientID)
        {
            return ceeeRepository.GetClientJobsByClientId(clientID);
        }

        public static OrgRegFormViewModel GetClientDetailsByID(int clientID)
        {
            var orgRegDetails = new OrgRegFormViewModel();

            var result = ceeeRepository.GetClientDetailsByID(clientID);

            var address = new AddressModel
                {
                    AddressLine1 = result.Address.AddressLine1,
                    AddressLine2 = result.Address.AddressLine2,
                    City = result.Address.City,
                    Country = result.Address.Country,
                    PostCode = result.Address.PostCode
                };
            orgRegDetails.Address = address;

            orgRegDetails.BusinessRegNumber = result.BusinessRegNumber;
            orgRegDetails.CharityRegNumber = result.CharityRegNumber;
            orgRegDetails.CompanySize = result.CompanySize;
            orgRegDetails.Email = result.Email;
            orgRegDetails.FirstName = result.FirstName;
            orgRegDetails.LastName = result.LastName;
            orgRegDetails.LandLineNo = result.LandLineNo;
            orgRegDetails.MobileNo = result.MobileNo;
            orgRegDetails.HowHeard = result.HowHeard;
            orgRegDetails.JobTitle = result.JobTitle;
            orgRegDetails.OrgWebSite = result.OrgWebSite;
            orgRegDetails.OrganisationDoes = result.OrganisationDoes;
            orgRegDetails.OrganisationName = result.OrganisationName;
            orgRegDetails.UserId = clientID;
            orgRegDetails.Title = (CEEEPortal.Models.Title)Enum.Parse(typeof(CEEEPortal.Models.Title), result.Title.ToString());
            orgRegDetails.HowHeard = result.HowHeard;
            orgRegDetails.OpportunityType = new OpportunityType
                {
                    AirLineAndAirport = result.OpportunityType.AirLineAndAirport,
                    Education = result.OpportunityType.Education,
                    Hospitality = result.OpportunityType.Hospitality,
                    Other = result.OpportunityType.Other,
                    RecruitmentAgency = result.OpportunityType.RecruitmentAgency
                };
            orgRegDetails.Title =
                (CEEEPortal.Models.Title)Enum.Parse(typeof(CEEEPortal.Models.Title), result.Title.ToString());
            return orgRegDetails;

        }

        public static bool SaveGraduateOrStudentJob(GraduateEmployerModel graduateOrStudentJob, int clientID)
        {
            var gradJob = new GraduateEmployerTO();
            gradJob.ClosingDate = graduateOrStudentJob.ClosingDate;
            gradJob.ContractType =
                (CEEEPortal.DataAccessTOs.ContractType)
                Enum.Parse(typeof(CEEEPortal.DataAccessTOs.ContractType), graduateOrStudentJob.ContractType.ToString());
            gradJob.ContractTypeOther = graduateOrStudentJob.ContractTypeOther;
            gradJob.EmploymentStudentType =
                (CEEEPortal.DataAccessTOs.EmploymentStudentType)
                Enum.Parse(typeof(CEEEPortal.DataAccessTOs.EmploymentStudentType),
                           graduateOrStudentJob.EmploymentStudentType.ToString());
            gradJob.EndDate = graduateOrStudentJob.EndDate;
            gradJob.HowEmployeeApproaches = graduateOrStudentJob.HowEmployeeApproaches;
            gradJob.JobTitle = graduateOrStudentJob.JobTitle;
            gradJob.JobType =
                (CEEEPortal.DataAccessTOs.JobType)
                Enum.Parse(typeof(CEEEPortal.DataAccessTOs.JobType), graduateOrStudentJob.JobType.ToString());
            gradJob.NumberOfOpportunities = graduateOrStudentJob.NumberOfOpportunities;
            gradJob.OpportunityDescription = graduateOrStudentJob.OpportunityDescription;
            gradJob.OrganisationName = graduateOrStudentJob.OrganisationName;
            gradJob.Salary = graduateOrStudentJob.Salary;
            gradJob.SalaryPeriod = graduateOrStudentJob.SalaryPeriod;
            gradJob.StartDate = graduateOrStudentJob.StartDate;
            return ceeeRepository.SaveGraduateOrStudentJob(gradJob, clientID);
        }

        public static bool UpdateGraduateOrStudentJob(GraduateEmployerModel graduateOrStudentJob, int jobId)
        {
            var gradJob = new GraduateEmployerTO();
            gradJob.ClosingDate = graduateOrStudentJob.ClosingDate;
            gradJob.ContractType =
                (CEEEPortal.DataAccessTOs.ContractType)
                Enum.Parse(typeof(CEEEPortal.DataAccessTOs.ContractType), graduateOrStudentJob.ContractType.ToString());
            gradJob.ContractTypeOther = graduateOrStudentJob.ContractTypeOther;
            gradJob.EndDate = graduateOrStudentJob.EndDate;
            gradJob.HowEmployeeApproaches = graduateOrStudentJob.HowEmployeeApproaches;
            gradJob.JobTitle = graduateOrStudentJob.JobTitle;
            gradJob.JobType =
                (CEEEPortal.DataAccessTOs.JobType)
                Enum.Parse(typeof(CEEEPortal.DataAccessTOs.JobType), graduateOrStudentJob.JobType.ToString());
            gradJob.NumberOfOpportunities = graduateOrStudentJob.NumberOfOpportunities;
            gradJob.OpportunityDescription = graduateOrStudentJob.OpportunityDescription;
            gradJob.OrganisationName = graduateOrStudentJob.OrganisationName;
            gradJob.Salary = graduateOrStudentJob.Salary;
            gradJob.SalaryPeriod = graduateOrStudentJob.SalaryPeriod;
            gradJob.StartDate = graduateOrStudentJob.StartDate;
            return ceeeRepository.UpdateGraduateOrStudentJob(gradJob, jobId);
        }
        public static bool SaveSandwichOrPlacementJob(SandwichPlacementViewModel sandwichPlacementJob, int clientID)
        {
            var job = new SandwichPlacementTO();
            job.Address = new AddressTO
                {
                    AddressLine1 = sandwichPlacementJob.Address.AddressLine1,
                    AddressLine2 = sandwichPlacementJob.Address.AddressLine2,
                    City = sandwichPlacementJob.Address.City,
                    Country = sandwichPlacementJob.Address.Country,
                    PostCode = sandwichPlacementJob.Address.PostCode
                };

            job.ClosingDate = sandwichPlacementJob.ClosingDate;
            job.EndDate = sandwichPlacementJob.EndDate;

            var files = sandwichPlacementJob.FileUploads;
            var filesInBytes = new List<byte[]>();
            var fileNames = new List<string>();
            if(files != null)
            foreach (var file in files)
            {
                if (file != null)
                {
                    fileNames.Add(file.FileName);
                    var stream = file.InputStream;
                    var bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);

                    filesInBytes.Add(bytes);
                }
            }
            job.FileUploads = filesInBytes;
            job.Filenames = fileNames;
            job.JobRoleTitle = sandwichPlacementJob.JobRoleTitle;
            job.NumberOfHoursWeekly = sandwichPlacementJob.NumberOfHoursWeekly;
            job.NumberOfPositions = sandwichPlacementJob.NumberOfPositions;
            job.OpportunityDescription = sandwichPlacementJob.OpportunityDescription;
            job.Position = sandwichPlacementJob.Position;
            job.Salary = sandwichPlacementJob.Salary;
            job.StartDate = sandwichPlacementJob.StartDate;

            return ceeeRepository.SaveSandwichOrPlacementJob(job, clientID);

        }

        public static bool UpdateSandwichOrPlacementJob(SandwichPlacementViewModel sandwichPlacementJob, int jobId)
        {
            var job = new SandwichPlacementTO();
            job.Address = new AddressTO
            {
                AddressLine1 = sandwichPlacementJob.Address.AddressLine1,
                AddressLine2 = sandwichPlacementJob.Address.AddressLine2,
                City = sandwichPlacementJob.Address.City,
                Country = sandwichPlacementJob.Address.Country,
                PostCode = sandwichPlacementJob.Address.PostCode
            };
            var files = sandwichPlacementJob.FileUploads;
            var filesInBytes = new List<byte[]>();
            var fileNames = new List<string>();

            if (files != null)
            foreach (var file in files)
            {
                if (file != null)
                {
                    fileNames.Add(file.FileName);
                    var stream = file.InputStream;
                    var bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);

                    filesInBytes.Add(bytes);
                }
            }
            job.FileUploads = filesInBytes;
            job.Filenames = fileNames;

            job.ClosingDate = sandwichPlacementJob.ClosingDate;
            job.EndDate = sandwichPlacementJob.EndDate;
            job.JobRoleTitle = sandwichPlacementJob.JobRoleTitle;
            job.NumberOfHoursWeekly = sandwichPlacementJob.NumberOfHoursWeekly;
            job.NumberOfPositions = sandwichPlacementJob.NumberOfPositions;
            job.OpportunityDescription = sandwichPlacementJob.OpportunityDescription;
            job.Position = sandwichPlacementJob.Position;
            job.Salary = sandwichPlacementJob.Salary;
            job.StartDate = sandwichPlacementJob.StartDate;

            return ceeeRepository.UpdateSandwichOrPlacementJob(job, jobId);

        }
        public static bool SaveOnedDayChallengeOrCharityVolunteeringJob(
            OneDayChallengeCharityVolunteerViewModel oneDayChallengeCharityJob,
            int clientID)
        {
            var job = new OneDayChallengeCharityVolunteerTO();
            job.AlternateEmail = oneDayChallengeCharityJob.AlternateEmail;
            job.ContactEmail = oneDayChallengeCharityJob.ContactEmail;
            job.ContactName = oneDayChallengeCharityJob.ContactName;
            job.When = oneDayChallengeCharityJob.When;
            job.WhatWillBeDoing = oneDayChallengeCharityJob.WhatWillBeDoing;
            job.ExperienceNeeded = oneDayChallengeCharityJob.ExperienceNeeded;
            job.MeetingPoint = oneDayChallengeCharityJob.MeetingPoint;
            job.Location = oneDayChallengeCharityJob.Location;
            job.MeetingTime = oneDayChallengeCharityJob.MeetingTime;
            job.AlternateEmail = oneDayChallengeCharityJob.AlternateEmail;
            job.IsCordinatedByUniversity = oneDayChallengeCharityJob.IsCordinatedByUniversity;
            job.ProjectEventTitle = oneDayChallengeCharityJob.ProjectEventTitle;
            job.FinishingTime = oneDayChallengeCharityJob.FinishingTime;
            job.HasCarriedOutRiskAssesment = oneDayChallengeCharityJob.HasCarriedOutRiskAssesment;
            job.HasInsuranceLiability = oneDayChallengeCharityJob.HasInsuranceLiability;
            job.HasNoInsuranceLiability = oneDayChallengeCharityJob.HasNoInsuranceLiability;
            job.HasNotCarriedOutRiskAssesment = oneDayChallengeCharityJob.HasNotCarriedOutRiskAssesment;
        
            job.OpportunityCategoryType = new DataAccessTOs.OpportunityCategoryType
                {
                    Administration = oneDayChallengeCharityJob.OpportunityCategoryType.Administration,
                    Animals = oneDayChallengeCharityJob.OpportunityCategoryType.Animals,
                    ChildrenYouth = oneDayChallengeCharityJob.OpportunityCategoryType.ChildrenYouth,
                    CultureHeritage = oneDayChallengeCharityJob.OpportunityCategoryType.CultureHeritage,
                    Education = oneDayChallengeCharityJob.OpportunityCategoryType.Education,
                    Environment = oneDayChallengeCharityJob.OpportunityCategoryType.Environment,
                    EthnicityReligion = oneDayChallengeCharityJob.OpportunityCategoryType.EthnicityReligion,
                    EventProjectManagement = oneDayChallengeCharityJob.OpportunityCategoryType.EventProjectManagement,
                    GovernanceFinance = oneDayChallengeCharityJob.OpportunityCategoryType.GovernanceFinance,
                    HealthSocialCare = oneDayChallengeCharityJob.OpportunityCategoryType.HealthSocialCare,
                    Politics = oneDayChallengeCharityJob.OpportunityCategoryType.Politics,
                    MediaCreative = oneDayChallengeCharityJob.OpportunityCategoryType.MediaCreative,
                    MentoringAdvice = oneDayChallengeCharityJob.OpportunityCategoryType.MentoringAdvice,
                    IT = oneDayChallengeCharityJob.OpportunityCategoryType.IT,
                    InternationalDevelopment = oneDayChallengeCharityJob.OpportunityCategoryType.InternationalDevelopment,
                    SocialEnterprise = oneDayChallengeCharityJob.OpportunityCategoryType.SocialEnterprise,
                    SportsOutdoor = oneDayChallengeCharityJob.OpportunityCategoryType.SportsOutdoor,
                    VulnerableAdults = oneDayChallengeCharityJob.OpportunityCategoryType.VulnerableAdults

                };
            job.OpportunityType =
                (DataAccessTOs.VolunteeringTypes)
                Enum.Parse(typeof(DataAccessTOs.VolunteeringTypes),
                           oneDayChallengeCharityJob.OpportunityType.ToString());
            job.OrganisationType = oneDayChallengeCharityJob.OrganisationType;

            if (oneDayChallengeCharityJob.BudgetFormFile != null)
            {
                var bytesBudgetFile = new byte[oneDayChallengeCharityJob.BudgetFormFile.ContentLength];
                oneDayChallengeCharityJob.BudgetFormFile.InputStream.Read(bytesBudgetFile, 0, bytesBudgetFile.Length);
                job.BudgetFormFile = bytesBudgetFile;
                job.BudgetFormFileName = oneDayChallengeCharityJob.BudgetFormFile.FileName;
            }
            if (oneDayChallengeCharityJob.RiskAssesmentFormFile != null)
            {
                var bytesRiskFile = new byte[oneDayChallengeCharityJob.RiskAssesmentFormFile.ContentLength];
                oneDayChallengeCharityJob.RiskAssesmentFormFile.InputStream.Read(bytesRiskFile, 0,
                                                                                 bytesRiskFile.Length);
                job.RiskAssesmentFormFile = bytesRiskFile;
                job.RiskAssesmentFormFileName = oneDayChallengeCharityJob.RiskAssesmentFormFile.FileName;
            }
            if (oneDayChallengeCharityJob.SessionPlanFile != null)
            {
                var bytesSessionFile = new byte[oneDayChallengeCharityJob.SessionPlanFile.ContentLength];
                oneDayChallengeCharityJob.SessionPlanFile.InputStream.Read(bytesSessionFile, 0, bytesSessionFile.Length);
                job.SessionPlanFile = bytesSessionFile;
                job.SessionPlanFileName = oneDayChallengeCharityJob.SessionPlanFile.FileName;
            }
            return ceeeRepository.SaveOnedDayChallengeOrCharityVolunteeringJob(job, clientID);
        }

        public static bool UpdateOnedDayChallengeOrCharityVolunteeringJob(
    OneDayChallengeCharityVolunteerViewModel oneDayChallengeCharityJob,
    int jobId)
        {
            var job = new OneDayChallengeCharityVolunteerTO();
            job.AlternateEmail = oneDayChallengeCharityJob.AlternateEmail;
            job.ContactEmail = oneDayChallengeCharityJob.ContactEmail;
            job.ContactName = oneDayChallengeCharityJob.ContactName;
            job.ExperienceNeeded = oneDayChallengeCharityJob.ExperienceNeeded;
            job.MeetingPoint = oneDayChallengeCharityJob.MeetingPoint;
            job.Location = oneDayChallengeCharityJob.Location;
            job.MeetingTime = oneDayChallengeCharityJob.MeetingTime;
            job.AlternateEmail = oneDayChallengeCharityJob.AlternateEmail;
            job.IsCordinatedByUniversity = oneDayChallengeCharityJob.IsCordinatedByUniversity;
            job.ProjectEventTitle = oneDayChallengeCharityJob.ProjectEventTitle;
            job.FinishingTime = oneDayChallengeCharityJob.FinishingTime;
            job.HasCarriedOutRiskAssesment = oneDayChallengeCharityJob.HasCarriedOutRiskAssesment;
            job.HasInsuranceLiability = oneDayChallengeCharityJob.HasInsuranceLiability;
            job.HasNoInsuranceLiability = oneDayChallengeCharityJob.HasNoInsuranceLiability;
            job.HasNotCarriedOutRiskAssesment = oneDayChallengeCharityJob.HasNotCarriedOutRiskAssesment;
            job.When = oneDayChallengeCharityJob.When;
            job.WhatWillBeDoing = oneDayChallengeCharityJob.WhatWillBeDoing;
            if (oneDayChallengeCharityJob.BudgetFormFile != null)
            {
                var bytesBudgetFile = new byte[oneDayChallengeCharityJob.BudgetFormFile.ContentLength];
                oneDayChallengeCharityJob.BudgetFormFile.InputStream.Read(bytesBudgetFile, 0, bytesBudgetFile.Length);
                job.BudgetFormFile = bytesBudgetFile;
                job.BudgetFormFileName = oneDayChallengeCharityJob.BudgetFormFile.FileName;
            }
            if (oneDayChallengeCharityJob.RiskAssesmentFormFile != null)
            {
                var bytesRiskFile = new byte[oneDayChallengeCharityJob.RiskAssesmentFormFile.ContentLength];
                oneDayChallengeCharityJob.RiskAssesmentFormFile.InputStream.Read(bytesRiskFile, 0,
                                                                                 bytesRiskFile.Length);
                job.RiskAssesmentFormFile = bytesRiskFile;
                job.RiskAssesmentFormFileName = oneDayChallengeCharityJob.RiskAssesmentFormFile.FileName;
            }
            if (oneDayChallengeCharityJob.SessionPlanFile != null)
            {
                var bytesSessionFile = new byte[oneDayChallengeCharityJob.SessionPlanFile.ContentLength];
                oneDayChallengeCharityJob.SessionPlanFile.InputStream.Read(bytesSessionFile, 0, bytesSessionFile.Length);
                job.SessionPlanFile = bytesSessionFile;
                job.SessionPlanFileName = oneDayChallengeCharityJob.SessionPlanFile.FileName;
            }
            job.OpportunityCategoryType = new DataAccessTOs.OpportunityCategoryType
            {
                Administration = oneDayChallengeCharityJob.OpportunityCategoryType.Administration,
                Animals = oneDayChallengeCharityJob.OpportunityCategoryType.Animals,
                ChildrenYouth = oneDayChallengeCharityJob.OpportunityCategoryType.ChildrenYouth,
                CultureHeritage = oneDayChallengeCharityJob.OpportunityCategoryType.CultureHeritage,
                Education = oneDayChallengeCharityJob.OpportunityCategoryType.Education,
                Environment = oneDayChallengeCharityJob.OpportunityCategoryType.Environment,
                EthnicityReligion = oneDayChallengeCharityJob.OpportunityCategoryType.EthnicityReligion,
                EventProjectManagement = oneDayChallengeCharityJob.OpportunityCategoryType.EventProjectManagement,
                GovernanceFinance = oneDayChallengeCharityJob.OpportunityCategoryType.GovernanceFinance,
                HealthSocialCare = oneDayChallengeCharityJob.OpportunityCategoryType.HealthSocialCare,
                Politics = oneDayChallengeCharityJob.OpportunityCategoryType.Politics,
                MediaCreative = oneDayChallengeCharityJob.OpportunityCategoryType.MediaCreative,
                MentoringAdvice = oneDayChallengeCharityJob.OpportunityCategoryType.MentoringAdvice,
                IT = oneDayChallengeCharityJob.OpportunityCategoryType.IT,
                InternationalDevelopment = oneDayChallengeCharityJob.OpportunityCategoryType.InternationalDevelopment,
                SocialEnterprise = oneDayChallengeCharityJob.OpportunityCategoryType.SocialEnterprise,
                SportsOutdoor = oneDayChallengeCharityJob.OpportunityCategoryType.SportsOutdoor,
                VulnerableAdults = oneDayChallengeCharityJob.OpportunityCategoryType.VulnerableAdults

            };
            job.OpportunityType =
                (DataAccessTOs.VolunteeringTypes)
                Enum.Parse(typeof(DataAccessTOs.VolunteeringTypes),
                           oneDayChallengeCharityJob.OpportunityType.ToString());
            job.OrganisationType = oneDayChallengeCharityJob.OrganisationType;
            return ceeeRepository.UpdateOnedDayChallengeOrCharityVolunteeringJob(job, jobId);
        }
        public static bool SavePlacementOrInternationalVolunteeringJob(
            PlacementsInternationalVolunteerViewModel placementInternationalJob,
            int clientID)
        {
            var job = new PlacementsInternationalVolunteerTO();
            job.AboutOrganisation = placementInternationalJob.AboutOrganisation;
            job.AlternateEmail = placementInternationalJob.AlternateEmail;
            job.ApplicationMethodCoverLetter = placementInternationalJob.ApplicationMethodCoverLetter;
            job.ApplicationMethodCv = placementInternationalJob.ApplicationMethodCv;
            job.ApplicationMethodForm = placementInternationalJob.ApplicationMethodForm;
            job.ApplicationReceiveEmail = placementInternationalJob.ApplicationReceiveEmail;
            job.ApplicationReceiveWebsite = placementInternationalJob.ApplicationReceiveWebsite;
            job.ContactEmail = placementInternationalJob.ContactEmail;
            job.ContactName = placementInternationalJob.ContactName;
            job.DayTimeRequired = placementInternationalJob.DayTimeRequired;
            job.DurationNeeded = placementInternationalJob.DurationNeeded;
            job.EmailReceipt = placementInternationalJob.EmailReceipt;
            job.WebSiteReciept = placementInternationalJob.WebSiteReciept;
            job.HasInsuranceLiability = placementInternationalJob.HasInsuranceLiability;
            job.HasNoInsuranceLiability = placementInternationalJob.HasNoInsuranceLiability;
            job.HasNotCarriedOutRiskAssesment = placementInternationalJob.HasNotCarriedOutRiskAssesment;
            job.HasCarriedOutRiskAssesment = placementInternationalJob.HasCarriedOutRiskAssesment;
            job.Location = placementInternationalJob.Location;
            job.NumberOfVacancies = placementInternationalJob.NumberOfVacancies;
            job.OpportunityType =
    (DataAccessTOs.VolunteeringTypes)
    Enum.Parse(typeof(DataAccessTOs.VolunteeringTypes),
               placementInternationalJob.OpportunityType.ToString());
            job.OpportunityCategoryType = new DataAccessTOs.OpportunityCategoryType
                {
                    Administration = placementInternationalJob.OpportunityCategoryType.Administration,
                    Animals = placementInternationalJob.OpportunityCategoryType.Animals,
                    ChildrenYouth = placementInternationalJob.OpportunityCategoryType.ChildrenYouth,
                    CultureHeritage = placementInternationalJob.OpportunityCategoryType.CultureHeritage,
                    Education = placementInternationalJob.OpportunityCategoryType.Education,
                    Environment = placementInternationalJob.OpportunityCategoryType.Environment,
                    EthnicityReligion = placementInternationalJob.OpportunityCategoryType.EthnicityReligion,
                    EventProjectManagement = placementInternationalJob.OpportunityCategoryType.EventProjectManagement,
                    GovernanceFinance = placementInternationalJob.OpportunityCategoryType.GovernanceFinance,
                    HealthSocialCare = placementInternationalJob.OpportunityCategoryType.HealthSocialCare,
                    Politics = placementInternationalJob.OpportunityCategoryType.Politics,
                    MediaCreative = placementInternationalJob.OpportunityCategoryType.MediaCreative,
                    MentoringAdvice = placementInternationalJob.OpportunityCategoryType.MentoringAdvice,
                    IT = placementInternationalJob.OpportunityCategoryType.IT,
                    InternationalDevelopment = placementInternationalJob.OpportunityCategoryType.InternationalDevelopment,
                    SocialEnterprise = placementInternationalJob.OpportunityCategoryType.SocialEnterprise,
                    SportsOutdoor = placementInternationalJob.OpportunityCategoryType.SportsOutdoor,
                    VulnerableAdults = placementInternationalJob.OpportunityCategoryType.VulnerableAdults

                };
            job.OpportunityType =
                (DataAccessTOs.VolunteeringTypes)
                Enum.Parse(typeof(DataAccessTOs.VolunteeringTypes),
                           placementInternationalJob.OpportunityType.ToString());
            job.OrganisationType = placementInternationalJob.OrganisationType;
            job.RoleTitle = placementInternationalJob.RoleTitle;
            job.SkillsRequired = placementInternationalJob.SkillsRequired;
            job.TrainingDetails = placementInternationalJob.TrainingDetails;

            if (placementInternationalJob.ApplicationFormFile != null)
            {
                var bytesApplicationFile = new byte[placementInternationalJob.ApplicationFormFile.ContentLength];
                placementInternationalJob.ApplicationFormFile.InputStream.Read(bytesApplicationFile, 0, bytesApplicationFile.Length);
                job.ApplicationFormFile = bytesApplicationFile;
                job.ApplicationFormFileName = placementInternationalJob.ApplicationFormFile.FileName;
            }
            if (placementInternationalJob.RoleDescriptionFile != null)
            {
                var bytesRoleFile = new byte[placementInternationalJob.RoleDescriptionFile.ContentLength];
                placementInternationalJob.RoleDescriptionFile.InputStream.Read(bytesRoleFile, 0, bytesRoleFile.Length);
                job.RoleDescriptionFile = bytesRoleFile;
                job.RoleDescriptionFileName = placementInternationalJob.RoleDescriptionFile.FileName;
            }

            return ceeeRepository.SavePlacementOrInternationalVolunteeringJob(job, clientID);
        }


        public static bool UpdatePlacementOrInternationalVolunteeringJob(
    PlacementsInternationalVolunteerViewModel placementInternationalJob,
    int jobId)
        {
            var job = new PlacementsInternationalVolunteerTO();
            job.AboutOrganisation = placementInternationalJob.AboutOrganisation;
            job.AlternateEmail = placementInternationalJob.AlternateEmail;
            job.ApplicationMethodCoverLetter = placementInternationalJob.ApplicationMethodCoverLetter;
            job.ApplicationMethodCv = placementInternationalJob.ApplicationMethodCv;
            job.ApplicationMethodForm = placementInternationalJob.ApplicationMethodForm;
            job.ApplicationReceiveEmail = placementInternationalJob.ApplicationReceiveEmail;
            job.ApplicationReceiveWebsite = placementInternationalJob.ApplicationReceiveWebsite;
            job.ContactEmail = placementInternationalJob.ContactEmail;
            job.ContactName = placementInternationalJob.ContactName;
            job.DayTimeRequired = placementInternationalJob.DayTimeRequired;
            job.DurationNeeded = placementInternationalJob.DurationNeeded;
            job.EmailReceipt = placementInternationalJob.EmailReceipt;
            job.WebSiteReciept = placementInternationalJob.WebSiteReciept;
            job.HasCarriedOutRiskAssesment = placementInternationalJob.HasCarriedOutRiskAssesment;
            job.HasInsuranceLiability = placementInternationalJob.HasInsuranceLiability;
            job.HasNoInsuranceLiability = placementInternationalJob.HasNoInsuranceLiability;
            job.HasNotCarriedOutRiskAssesment = placementInternationalJob.HasNotCarriedOutRiskAssesment;
            job.HasCarriedOutRiskAssesment = placementInternationalJob.HasCarriedOutRiskAssesment;
            job.Location = placementInternationalJob.Location;
            job.NumberOfVacancies = placementInternationalJob.NumberOfVacancies;
            job.OpportunityType =
    (DataAccessTOs.VolunteeringTypes)
    Enum.Parse(typeof(DataAccessTOs.VolunteeringTypes),
               placementInternationalJob.OpportunityType.ToString());
            job.OpportunityCategoryType = new DataAccessTOs.OpportunityCategoryType
            {
                Administration = placementInternationalJob.OpportunityCategoryType.Administration,
                Animals = placementInternationalJob.OpportunityCategoryType.Animals,
                ChildrenYouth = placementInternationalJob.OpportunityCategoryType.ChildrenYouth,
                CultureHeritage = placementInternationalJob.OpportunityCategoryType.CultureHeritage,
                Education = placementInternationalJob.OpportunityCategoryType.Education,
                Environment = placementInternationalJob.OpportunityCategoryType.Environment,
                EthnicityReligion = placementInternationalJob.OpportunityCategoryType.EthnicityReligion,
                EventProjectManagement = placementInternationalJob.OpportunityCategoryType.EventProjectManagement,
                GovernanceFinance = placementInternationalJob.OpportunityCategoryType.GovernanceFinance,
                HealthSocialCare = placementInternationalJob.OpportunityCategoryType.HealthSocialCare,
                Politics = placementInternationalJob.OpportunityCategoryType.Politics,
                MediaCreative = placementInternationalJob.OpportunityCategoryType.MediaCreative,
                MentoringAdvice = placementInternationalJob.OpportunityCategoryType.MentoringAdvice,
                IT = placementInternationalJob.OpportunityCategoryType.IT,
                InternationalDevelopment = placementInternationalJob.OpportunityCategoryType.InternationalDevelopment,
                SocialEnterprise = placementInternationalJob.OpportunityCategoryType.SocialEnterprise,
                SportsOutdoor = placementInternationalJob.OpportunityCategoryType.SportsOutdoor,
                VulnerableAdults = placementInternationalJob.OpportunityCategoryType.VulnerableAdults

            };
            job.OpportunityType =
                (DataAccessTOs.VolunteeringTypes)
                Enum.Parse(typeof(DataAccessTOs.VolunteeringTypes),
                           placementInternationalJob.OpportunityType.ToString());
            job.OrganisationType = placementInternationalJob.OrganisationType;
            //job.RoleDescriptionFile = placementInternationalJob.RoleDescriptionFile;
            job.RoleTitle = placementInternationalJob.RoleTitle;
            job.SkillsRequired = placementInternationalJob.SkillsRequired;
            job.TrainingDetails = placementInternationalJob.TrainingDetails;
            if (placementInternationalJob.ApplicationFormFile != null)
            {
                var bytesApplicationFile = new byte[placementInternationalJob.ApplicationFormFile.ContentLength];
                placementInternationalJob.ApplicationFormFile.InputStream.Read(bytesApplicationFile, 0, bytesApplicationFile.Length);
                job.ApplicationFormFile = bytesApplicationFile;
                job.ApplicationFormFileName = placementInternationalJob.ApplicationFormFile.FileName;
            }
            if (placementInternationalJob.RoleDescriptionFile != null)
            {
                var bytesRoleFile = new byte[placementInternationalJob.RoleDescriptionFile.ContentLength];
                placementInternationalJob.RoleDescriptionFile.InputStream.Read(bytesRoleFile, 0, bytesRoleFile.Length);
                job.RoleDescriptionFile = bytesRoleFile;
                job.RoleDescriptionFileName = placementInternationalJob.RoleDescriptionFile.FileName;
            }
            return ceeeRepository.UpdatePlacementOrInternationalVolunteeringJob(job, jobId);
        }
        public static int GetClientIdByName(string name)
        {
            return ceeeRepository.GetClientIdByName(name);
        }

        public static int GetUserIdByUsername(string name)
        {
            return ceeeRepository.GetUserIdByUsername(name);
        }

        public static GraduateEmployerModel GetGraduateOrStudentJob(int jobId)
        {
            var graduateViewModel = new GraduateEmployerModel();

            var result = ceeeRepository.GetGraduateOrStudentJob(jobId);
            graduateViewModel.ClosingDate = result.ClosingDate;
            graduateViewModel.ContractType = (CEEEPortal.Models.ContractType)Enum.Parse(typeof(CEEEPortal.Models.ContractType),result.ContractType.ToString());
            graduateViewModel.EndDate = result.EndDate;
            graduateViewModel.HowEmployeeApproaches = result.HowEmployeeApproaches;
            graduateViewModel.JobTitle = result.JobTitle;
            graduateViewModel.JobType =
                (CEEEPortal.Models.JobType) Enum.Parse(typeof (CEEEPortal.Models.JobType), result.JobType.ToString());
            graduateViewModel.NumberOfOpportunities = result.NumberOfOpportunities;
            graduateViewModel.OpportunityDescription = result.OpportunityDescription;
            graduateViewModel.OrganisationName = result.OrganisationName;
            graduateViewModel.Salary = result.Salary;
            graduateViewModel.SalaryPeriod = result.SalaryPeriod;
            graduateViewModel.StartDate = result.StartDate;
            graduateViewModel.ContractTypeOther = result.ContractTypeOther;
            return graduateViewModel;
        }
        public static SandwichPlacementViewModel GetSandwichOrPlacementJob(int jobId)
        {
            var sandwichPlacementModel = new SandwichPlacementViewModel();

            var result = ceeeRepository.GetSandwichOrPlacementJob(jobId);

            sandwichPlacementModel.Address = new AddressModel();
            sandwichPlacementModel.Address.AddressLine1 = result.Address.AddressLine1;
            sandwichPlacementModel.Address.AddressLine2 = result.Address.AddressLine2;
            sandwichPlacementModel.Address.City = result.Address.City;
            sandwichPlacementModel.Address.Country = result.Address.Country;
            sandwichPlacementModel.Address.PostCode = result.Address.PostCode;
            sandwichPlacementModel.ClosingDate = result.ClosingDate;
            sandwichPlacementModel.EndDate = result.EndDate;
            sandwichPlacementModel.JobRoleTitle = result.JobRoleTitle;
            sandwichPlacementModel.NumberOfHoursWeekly = result.NumberOfHoursWeekly;
            sandwichPlacementModel.NumberOfPositions = result.NumberOfPositions;
            sandwichPlacementModel.OpportunityDescription = result.OpportunityDescription;
            sandwichPlacementModel.Position = result.Position;
            sandwichPlacementModel.Salary = result.Salary;
            sandwichPlacementModel.StartDate = result.StartDate;
            sandwichPlacementModel.EndDate = result.EndDate;
            return sandwichPlacementModel;
        }
        public static OneDayChallengeCharityVolunteerViewModel GetOnedDayChallengeOrCharityVolunteeringJob(int jobId)
        {
            var oneDayChallengeViewModel = new OneDayChallengeCharityVolunteerViewModel();

            var result = ceeeRepository.GetOnedDayChallengeOrCharityVolunteeringJob(jobId);

            oneDayChallengeViewModel.AlternateEmail = result.AlternateEmail;
            oneDayChallengeViewModel.ContactEmail = result.ContactEmail;
            oneDayChallengeViewModel.ContactName = result.ContactName;
            oneDayChallengeViewModel.ExperienceNeeded = result.ExperienceNeeded;
            oneDayChallengeViewModel.FinishingTime = result.FinishingTime;
            oneDayChallengeViewModel.HasCarriedOutRiskAssesment = result.HasCarriedOutRiskAssesment;
            oneDayChallengeViewModel.HasInsuranceLiability = result.HasInsuranceLiability;
            oneDayChallengeViewModel.IsCordinatedByUniversity = result.IsCordinatedByUniversity;
            oneDayChallengeViewModel.Location = result.Location;
            oneDayChallengeViewModel.MeetingPoint = result.MeetingPoint;
            oneDayChallengeViewModel.MeetingTime = result.MeetingTime;
            oneDayChallengeViewModel.OpportunityCategoryType = new OpportunityCategoryType();
            oneDayChallengeViewModel.OpportunityCategoryType.Administration = result.OpportunityCategoryType.Administration;
            oneDayChallengeViewModel.OpportunityCategoryType.Animals = result.OpportunityCategoryType.Animals;
            oneDayChallengeViewModel.OpportunityCategoryType.ChildrenYouth = result.OpportunityCategoryType.ChildrenYouth;
            oneDayChallengeViewModel.OpportunityCategoryType.CultureHeritage = result.OpportunityCategoryType.CultureHeritage;
            oneDayChallengeViewModel.OpportunityCategoryType.Education = result.OpportunityCategoryType.Education;
            oneDayChallengeViewModel.OpportunityCategoryType.Environment = result.OpportunityCategoryType.Environment;
            oneDayChallengeViewModel.OpportunityCategoryType.EthnicityReligion = result.OpportunityCategoryType.EthnicityReligion;
            oneDayChallengeViewModel.OpportunityCategoryType.EventProjectManagement = result.OpportunityCategoryType.EventProjectManagement;
            oneDayChallengeViewModel.OpportunityCategoryType.GovernanceFinance = result.OpportunityCategoryType.GovernanceFinance;
            oneDayChallengeViewModel.OpportunityCategoryType.HealthSocialCare = result.OpportunityCategoryType.HealthSocialCare;
            oneDayChallengeViewModel.OpportunityCategoryType.IT = result.OpportunityCategoryType.IT;
            oneDayChallengeViewModel.OpportunityCategoryType.InternationalDevelopment = result.OpportunityCategoryType.InternationalDevelopment;
            oneDayChallengeViewModel.OpportunityCategoryType.LawCriminalJustice = result.OpportunityCategoryType.LawCriminalJustice;
            oneDayChallengeViewModel.OpportunityCategoryType.MediaCreative = result.OpportunityCategoryType.MediaCreative;
            oneDayChallengeViewModel.OpportunityCategoryType.MentoringAdvice = result.OpportunityCategoryType.MentoringAdvice;
            oneDayChallengeViewModel.OpportunityCategoryType.Politics = result.OpportunityCategoryType.Politics;
            oneDayChallengeViewModel.OpportunityCategoryType.SocialEnterprise = result.OpportunityCategoryType.SocialEnterprise;
            oneDayChallengeViewModel.OpportunityCategoryType.SportsOutdoor = result.OpportunityCategoryType.SportsOutdoor;
            oneDayChallengeViewModel.OpportunityCategoryType.VulnerableAdults = result.OpportunityCategoryType.VulnerableAdults;
            oneDayChallengeViewModel.OpportunityType = (CEEEPortal.Models.VolunteeringTypes)Enum.Parse(typeof(CEEEPortal.Models.VolunteeringTypes), result.OpportunityType.ToString());
            oneDayChallengeViewModel.OrganisationType = result.OrganisationType;
            oneDayChallengeViewModel.ProjectEventTitle = result.ProjectEventTitle;
            oneDayChallengeViewModel.WhatWillBeDoing = result.WhatWillBeDoing;
            oneDayChallengeViewModel.When = result.When;

            return oneDayChallengeViewModel;
        }
        public static PlacementsInternationalVolunteerViewModel GetPlacementOrInternationalVolunteeringJob(int jobId)
        {
            var placementViewModel = new PlacementsInternationalVolunteerViewModel();

            var result = ceeeRepository.GetPlacementOrInternationalVolunteeringJob(jobId);
            placementViewModel.OpportunityType = (Models.VolunteeringTypes)Enum.Parse(typeof(Models.VolunteeringTypes), result.OpportunityType.ToString());
            placementViewModel.OrganisationType = result.OrganisationType;
            placementViewModel.AboutOrganisation = result.AboutOrganisation;
            placementViewModel.AlternateEmail = result.AlternateEmail;
            placementViewModel.ApplicationMethodCoverLetter = result.ApplicationMethodCoverLetter;
            placementViewModel.ApplicationReceiveEmail = result.ApplicationReceiveEmail;
            placementViewModel.ApplicationMethodCv = result.ApplicationMethodCv;
            placementViewModel.ApplicationMethodForm = result.ApplicationMethodForm;
            placementViewModel.ApplicationReceiveWebsite = result.ApplicationReceiveWebsite;
            placementViewModel.ContactEmail = result.ContactEmail;
            placementViewModel.ContactName = result.ContactName;
            placementViewModel.DayTimeRequired = result.DayTimeRequired;
            placementViewModel.DurationNeeded = result.DurationNeeded;
            placementViewModel.HasCarriedOutRiskAssesment = result.HasCarriedOutRiskAssesment;
            placementViewModel.HasInsuranceLiability = result.HasInsuranceLiability;
            placementViewModel.HasNotCarriedOutRiskAssesment = result.HasCarriedOutRiskAssesment;
            placementViewModel.HasNoInsuranceLiability = result.HasNoInsuranceLiability;
            placementViewModel.Location = result.Location;
            placementViewModel.NumberOfVacancies = result.NumberOfVacancies;
            placementViewModel.OpportunityCategoryType = new OpportunityCategoryType();
            placementViewModel.OpportunityCategoryType.Administration = result.OpportunityCategoryType.Administration;
            placementViewModel.OpportunityCategoryType.Animals = result.OpportunityCategoryType.Animals;
            placementViewModel.OpportunityCategoryType.ChildrenYouth = result.OpportunityCategoryType.ChildrenYouth;
            placementViewModel.OpportunityCategoryType.CultureHeritage = result.OpportunityCategoryType.CultureHeritage;
            placementViewModel.OpportunityCategoryType.Education = result.OpportunityCategoryType.Education;
            placementViewModel.OpportunityCategoryType.Environment = result.OpportunityCategoryType.Environment;
            placementViewModel.OpportunityCategoryType.EthnicityReligion = result.OpportunityCategoryType.EthnicityReligion;
            placementViewModel.OpportunityCategoryType.EventProjectManagement = result.OpportunityCategoryType.EventProjectManagement;
            placementViewModel.OpportunityCategoryType.GovernanceFinance = result.OpportunityCategoryType.GovernanceFinance;
            placementViewModel.OpportunityCategoryType.HealthSocialCare = result.OpportunityCategoryType.HealthSocialCare;
            placementViewModel.OpportunityCategoryType.IT = result.OpportunityCategoryType.IT;
            placementViewModel.OpportunityCategoryType.InternationalDevelopment = result.OpportunityCategoryType.InternationalDevelopment;
            placementViewModel.OpportunityCategoryType.LawCriminalJustice = result.OpportunityCategoryType.LawCriminalJustice;
            placementViewModel.OpportunityCategoryType.MediaCreative = result.OpportunityCategoryType.MediaCreative;
            placementViewModel.OpportunityCategoryType.MentoringAdvice = result.OpportunityCategoryType.MentoringAdvice;
            placementViewModel.OpportunityCategoryType.Politics = result.OpportunityCategoryType.Politics;
            placementViewModel.OpportunityCategoryType.SocialEnterprise = result.OpportunityCategoryType.SocialEnterprise;
            placementViewModel.OpportunityCategoryType.SportsOutdoor = result.OpportunityCategoryType.SportsOutdoor;
            placementViewModel.OpportunityCategoryType.VulnerableAdults = result.OpportunityCategoryType.VulnerableAdults;
            placementViewModel.OpportunityType = (CEEEPortal.Models.VolunteeringTypes)Enum.Parse(typeof(CEEEPortal.Models.VolunteeringTypes), result.OpportunityType.ToString());
            placementViewModel.OrganisationType = placementViewModel.OrganisationType;
            placementViewModel.RoleTitle = result.RoleTitle;
            placementViewModel.SkillsRequired = result.SkillsRequired;
            placementViewModel.TrainingDetails = result.TrainingDetails;
            placementViewModel.EmailReceipt = result.EmailReceipt;
            placementViewModel.WebSiteReciept = result.WebSiteReciept;

            return placementViewModel;
        }
    }
}
