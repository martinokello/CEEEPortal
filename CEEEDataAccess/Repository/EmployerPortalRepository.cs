using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEEEDataAccess.DataAccess;
using CEEEDataAccess.DataAccessTOs;
using CEEEDataAccess.Repository.Interfaces;
using CEEEPortal.DataAccessTOs;

namespace CEEEDataAccess.Repository
{
    public class EmployerPortalRepository : IRepository
    {
        public bool CheckUserNotExists(string username)
        {
            using (var dbContext = new ProNetTestEntities())
            {
                try
                {
                    var user = dbContext.CeeeUsers.SingleOrDefault(p => p.Email == username);
                    return user == null;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public int? GetJobTypeById(int jobId)
        {
            using (var dbContext = new ProNetTestEntities())
            {
                var jobDescription = dbContext.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == jobId);
                if (jobDescription == null) return 1;
                else return jobDescription.JobTypeID;
            }
        }
        public bool SaveClientDetails(OrgRegFormTO orgDetails)
        {
            try
            {
                var location = new Location();
                var client = new Client();
                var ceeeClientAttributes = new CeeeClientAttribute();

                CeeeUser ceeeUser = null;
                using (var userCtx = new ProNetTestEntities())
                {
                    ceeeUser = userCtx.CeeeUsers.SingleOrDefault(p => p.UserID == orgDetails.UserId);
                    ceeeUser.FirstName = orgDetails.FirstName;
                    ceeeUser.LastName = orgDetails.LastName;
                    ceeeUser.Email = orgDetails.Email;
                    ceeeUser.Phone = orgDetails.MobileNo;
                    ceeeUser.HowHeard = orgDetails.HowHeard;
                    ceeeUser.CompanySize = orgDetails.CompanySize;
                    ceeeUser.JobTitle = orgDetails.JobTitle;
                    ceeeUser.Client = client;
                    ceeeUser.OrgWebsite = orgDetails.OrgWebSite;
                    ceeeUser.Title = orgDetails.Title.ToString();
                    ceeeClientAttributes.CeeeUser = ceeeUser;
                    ceeeClientAttributes.Client = client;
                    ceeeClientAttributes.CompanySize = ceeeUser.CompanySize;
                    ceeeClientAttributes.LandlineNumber = orgDetails.LandLineNo;
                    ceeeClientAttributes.JobTitle = orgDetails.JobTitle;
                    ceeeClientAttributes.MobileNumber = orgDetails.MobileNo;
                    ceeeClientAttributes.OrganisationWebsite = orgDetails.OrgWebSite;
                    ceeeClientAttributes.Title = orgDetails.Title.ToString();

                    //Deal with OpportunityTypes:
                    var oppTypes = orgDetails.OpportunityType;
                    var airline = oppTypes.AirLineAndAirport ?? null;
                    var education = oppTypes.Education ?? null;
                    var hospitality = oppTypes.Hospitality ?? null;
                    var other = oppTypes.Other ?? null;
                    var recruitment = oppTypes.RecruitmentAgency ?? null;
                    var sport = oppTypes.SportAndLeisure ?? null;

                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = airline });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = education });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = hospitality });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = other });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = recruitment });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = sport });

                    location.Code = orgDetails.Address.PostCode;
                    location.Description = orgDetails.Address.AddressLine1 + "," + orgDetails.Address.AddressLine2 + "," +
                                           orgDetails.Address.City + "," +
                                           orgDetails.Address.Country;
                    location.CreatedOn = DateTime.Now;
                    //
                    client.Company = orgDetails.OrganisationName;
                    client.Notes = orgDetails.OrganisationDoes;
                    client.RegNo = !string.IsNullOrEmpty(orgDetails.BusinessRegNumber)
                                       ? orgDetails.BusinessRegNumber
                                       : orgDetails.CharityRegNumber;
                    client.CreatedOn = DateTime.Now;
                    client.UpdatedOn = DateTime.Now;

                    var obj = new CEEEDataAccess.DataAccess.Object();
                    var user = new CEEEDataAccess.DataAccess.User();
                    user.LoginName = (ceeeUser.FirstName + ceeeUser.LastName + Guid.NewGuid()).Substring(0, 20);
                    user.Inactive = "N";
                    user.SecurityOption = "N";
                    user.CreatedOn = DateTime.Now;
                    user.CreatedUserId = 1;
                    obj.ObjectTypeId = 2; //client = 2;
                    obj.Client = client;
                    obj.FileAs = "Not File";
                    obj.CreatedOn = DateTime.Now;

                    obj.CreatedUserId = 1;

                    location.CreatedUserId = 1;
                    var locationExist = userCtx.Locations.SingleOrDefault(p => p.Code == location.Code);
                    if (locationExist == null)
                    {
                        location.Objects.Add(obj);
                    }
                    else
                    {
                        client.Location = locationExist;
                        obj.Location = locationExist;
                    }
                    client.Object = obj;
                    client.User = user;
                    userCtx.Clients.Add(client);
                    userCtx.SaveChanges();
                    return true;
                }


            }
            catch (DbEntityValidationException e)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateClientDetails(OrgRegFormTO orgDetails)
        {
            try
            {
                Client client = null;

                using (var dbContext = new ProNetTestEntities())
                {
                    var ceeeUser = dbContext.CeeeUsers.SingleOrDefault(p => p.UserID == orgDetails.UserId);
                    client = ceeeUser.Client;
                    if (client == null) return false;

                    ceeeUser.FirstName = orgDetails.FirstName;
                    ceeeUser.LastName = orgDetails.LastName;
                    ceeeUser.Email = orgDetails.Email;
                    ceeeUser.Phone = orgDetails.MobileNo;
                    ceeeUser.HowHeard = orgDetails.HowHeard;
                    ceeeUser.CompanySize = orgDetails.CompanySize;
                    var ceeeClientAttributes = ceeeUser.CeeeClientAttribute;
                    if (ceeeClientAttributes == null) ceeeClientAttributes = new CeeeClientAttribute();
                    ceeeUser.CeeeOpportunityTypes = new Collection<CeeeOpportunityType>();

                    //Deal with OpportunityTypes:
                    var oppTypes = orgDetails.OpportunityType;
                    var airline = oppTypes.AirLineAndAirport ?? null;
                    var education = oppTypes.Education ?? null;
                    var hospitality = oppTypes.Hospitality ?? null;
                    var other = oppTypes.Other ?? null;
                    var recruitment = oppTypes.RecruitmentAgency ?? null;
                    var sport = oppTypes.SportAndLeisure ?? null;

                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = airline });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = education });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = hospitality });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = other });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = recruitment });
                    ceeeUser.CeeeOpportunityTypes.Add(new CeeeOpportunityType { OpportunityName = sport });
                    ceeeClientAttributes.CeeeUser = ceeeUser;
                    ceeeClientAttributes.Client = client;
                    ceeeClientAttributes.CompanySize = ceeeUser.CompanySize;
                    ceeeClientAttributes.LandlineNumber = orgDetails.LandLineNo;
                    ceeeClientAttributes.JobTitle = orgDetails.JobTitle;
                    ceeeClientAttributes.MobileNumber = orgDetails.MobileNo;
                    ceeeClientAttributes.OrganisationWebsite = orgDetails.OrgWebSite;
                    ceeeClientAttributes.Title = orgDetails.Title.ToString();

                    var location = client.Location;

                    location.Code = orgDetails.Address.PostCode;
                    location.Description = orgDetails.Address.AddressLine1 + "," + orgDetails.Address.AddressLine2 + "," +
                                           orgDetails.Address.City + "," +
                                           orgDetails.Address.Country;

                    client.Company = orgDetails.OrganisationName;
                    client.Notes = orgDetails.OrganisationDoes;
                    client.RegNo = !string.IsNullOrEmpty(orgDetails.BusinessRegNumber)
                                       ? orgDetails.BusinessRegNumber
                                       : orgDetails.CharityRegNumber;


                    dbContext.SaveChanges();
                    return true;
                }


            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public GraduateEmployerTO GetGraduateOrStudentJob(int jobId)
        {
            var graduateOrStudentJob = new GraduateEmployerTO();

            using (var dbContext = new ProNetTestEntities())
            {
                var jobDescription = dbContext.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == jobId);
                if (jobDescription == null) return null;
                var job = jobDescription.Job;

                graduateOrStudentJob.StartDate = job.StartDate;
                graduateOrStudentJob.EndDate = jobDescription.EndDate;
                graduateOrStudentJob.JobType =
                    (JobType)Enum.Parse(typeof(JobType), dbContext.CeeeJobContractTypes.SingleOrDefault(p => p.JobContractTypeId == jobDescription.JobContractTypeID).Name);
                graduateOrStudentJob.OpportunityDescription = job.Notes;
                graduateOrStudentJob.NumberOfOpportunities = job.NoOfPlaces;
                graduateOrStudentJob.JobTitle = job.JobTitle;
                graduateOrStudentJob.ContractType = job.EmploymentTypeId == 4
                                                        ? ContractType.Permanent
                                                        : job.EmploymentTypeId == 5
                                                              ? ContractType.Fixed
                                                              : job.EmploymentTypeId == 9
                                                                    ? ContractType.CasualZeroHour
                                                                    : ContractType.Other;

                graduateOrStudentJob.HowEmployeeApproaches = jobDescription.HowEmployeeApproaches;
                graduateOrStudentJob.ClosingDate = jobDescription.ClosingDate;
                graduateOrStudentJob.Salary = jobDescription.Salary;
                graduateOrStudentJob.SalaryPeriod = jobDescription.SalaryPeriod;
                graduateOrStudentJob.OpportunityDescription = jobDescription.JobDescription;
            }

            return graduateOrStudentJob;
        }

        public bool SaveGraduateOrStudentJob(GraduateEmployerTO graduateOrStudentJob, int clientID)
        {
            try
            {
                var job = new Job();
                var jobDescription = new CeeeJobDescription();

                var client = GetClientByID(clientID);

                if (client == null)
                {
                    using (var dbContext = new ProNetTestEntities())
                    {
                        client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                        if (client == null) return false;
                    }
                }
                job.ClientId = client.ClientID;

                job.StartDate = graduateOrStudentJob.StartDate;
                
                job.Notes = graduateOrStudentJob.OpportunityDescription;
                job.JobRefNo = ("J0" + client.ClientID + Guid.NewGuid().ToString()).Substring(0, 10);
                job.Published = "N";
                job.Archived = "N";
                job.CreatedOn = DateTime.Now;
                job.UpdatedOn = DateTime.Now;
                job.StatusDate = DateTime.Now;
                job.NoOfPlaces = graduateOrStudentJob.NumberOfOpportunities;
                job.JobTitle = graduateOrStudentJob.JobTitle;
                job.EmploymentTypeId = graduateOrStudentJob.ContractType == ContractType.Permanent
                                           ? 4
                                           : graduateOrStudentJob.ContractType == ContractType.Fixed
                                                 ? 5
                                                 : graduateOrStudentJob.ContractType == ContractType.CasualZeroHour
                                                       ? 9
                                                       : 10;
                jobDescription.EndDate = graduateOrStudentJob.EndDate;
                jobDescription.IsVolunteering = false;
                jobDescription.JobTypeID = 1;
                jobDescription.HowEmployeeApproaches = graduateOrStudentJob.HowEmployeeApproaches;
                jobDescription.ClosingDate = graduateOrStudentJob.ClosingDate;
                jobDescription.Salary = graduateOrStudentJob.Salary;
                jobDescription.SalaryPeriod = graduateOrStudentJob.SalaryPeriod;
                jobDescription.JobDescription = graduateOrStudentJob.OpportunityDescription;
                using (var dbContext = new ProNetTestEntities())
                {
                    var jobTypeValue = graduateOrStudentJob.JobType.ToString();
                    jobDescription.JobContractTypeID =
                        dbContext.CeeeJobContractTypes.SingleOrDefault(
                            p => p.Name == jobTypeValue).JobContractTypeId;
                    job.CeeeJobDescriptions = new Collection<CeeeJobDescription>();
                    job.CeeeJobDescriptions.Add(jobDescription);
                    dbContext.Jobs.Add(job);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateGraduateOrStudentJob(GraduateEmployerTO graduateOrStudentJob, int jobId)
        {
            try
            {
                using (var dbContext = new ProNetTestEntities())
                {
                    var job = dbContext.Jobs.SingleOrDefault(p => p.JobId == jobId);

                    var jobDescription = job.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == job.JobId);
                    var client = job.Client;

                    if (client == null)
                    {
                            client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                        if (client == null) return false;
                    }
                    var jobTypeValue = graduateOrStudentJob.JobType.ToString();
                    jobDescription.JobContractTypeID =
                        dbContext.CeeeJobContractTypes.SingleOrDefault(
                            p => p.Name == jobTypeValue).JobContractTypeId;
                    job.StartDate = graduateOrStudentJob.StartDate;
                    jobDescription.EndDate = graduateOrStudentJob.EndDate;
                    job.Notes = graduateOrStudentJob.OpportunityDescription;
                    job.JobRefNo = ("J0" + client.ClientID + Guid.NewGuid().ToString()).Substring(0, 10);
                    job.Published = "N";
                    job.Archived = "N";
                    job.CreatedOn = DateTime.Now;
                    job.UpdatedOn = DateTime.Now;
                    job.StatusDate = DateTime.Now;
                    job.NoOfPlaces = graduateOrStudentJob.NumberOfOpportunities;
                    job.JobTitle = graduateOrStudentJob.JobTitle;
                    job.EmploymentTypeId = graduateOrStudentJob.ContractType == ContractType.Permanent
                                               ? 4
                                               : graduateOrStudentJob.ContractType == ContractType.Fixed
                                                     ? 5
                                                     : graduateOrStudentJob.ContractType == ContractType.CasualZeroHour
                                                           ? 9
                                                           : 10;
                    jobDescription.IsVolunteering = false;
                    jobDescription.HowEmployeeApproaches = graduateOrStudentJob.HowEmployeeApproaches;
                    jobDescription.ClosingDate = graduateOrStudentJob.ClosingDate;
                    jobDescription.Salary = graduateOrStudentJob.Salary;
                    jobDescription.SalaryPeriod = graduateOrStudentJob.SalaryPeriod;
                    jobDescription.JobDescription = graduateOrStudentJob.OpportunityDescription;

                    dbContext.SaveChanges();
                    return true;
                }


            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SandwichPlacementTO GetSandwichOrPlacementJob(int jobId)
        {
            var sandwichPlacementJob = new SandwichPlacementTO();

            using (var dbContext = new ProNetTestEntities())
            {
                var job = dbContext.Jobs.SingleOrDefault(p => p.JobId == jobId);

                var jobDescription = job.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == job.JobId);

                var client = job.Client;

                if (client == null)
                {
                    client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                    if (client == null) return new SandwichPlacementTO();
                }
                sandwichPlacementJob.OpportunityDescription = job.Notes;
                sandwichPlacementJob.StartDate = job.StartDate;
                sandwichPlacementJob.EndDate = jobDescription.EndDate;

                sandwichPlacementJob.NumberOfPositions = job.NoOfPlaces;
                sandwichPlacementJob.JobRoleTitle = job.JobTitle;
                sandwichPlacementJob.Address = new AddressTO();

                var workAddressComponents = job.WorkAddress.Split(new char[] { ',' },
                                                                  StringSplitOptions.RemoveEmptyEntries);
                sandwichPlacementJob.Address.AddressLine1 = workAddressComponents[0];
                sandwichPlacementJob.Address.AddressLine2 = workAddressComponents[1];
                sandwichPlacementJob.Address.City = workAddressComponents[2];
                sandwichPlacementJob.Address.PostCode = workAddressComponents[3];

                sandwichPlacementJob.ClosingDate = (DateTime)jobDescription.ClosingDate;
                sandwichPlacementJob.Salary = jobDescription.Salary;
                sandwichPlacementJob.NumberOfHoursWeekly = jobDescription.NoOfHoursPerWeek;
                sandwichPlacementJob.OpportunityDescription = jobDescription.JobDescription;
            }
            return sandwichPlacementJob;
        }

        public bool SaveSandwichOrPlacementJob(SandwichPlacementTO sandwichPlacementJob, int clientID)
        {
            try
            {
                var job = new Job();
                var jobDescription = new CeeeJobDescription();

                var client = GetClientByID(clientID);
                
                if (client == null)
                {
                    using (var dbContext = new ProNetTestEntities())
                    {
                        client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                        if (client == null) return false;
                    }
                }

                job.ClientId = client.ClientID;
                job.Notes = sandwichPlacementJob.OpportunityDescription;
                job.StartDate = sandwichPlacementJob.StartDate;
                jobDescription.EndDate = sandwichPlacementJob.EndDate;
                job.JobRefNo = ("J0" + client.ClientID + Guid.NewGuid().ToString()).Substring(0, 10);
                job.Published = "N";
                job.Archived = "N";
                job.CreatedOn = DateTime.Now;
                job.UpdatedOn = DateTime.Now;
                job.StatusDate = DateTime.Now;
                job.NoOfPlaces = sandwichPlacementJob.NumberOfPositions;
                job.JobTitle = sandwichPlacementJob.JobRoleTitle;
                job.WorkAddress = sandwichPlacementJob.Address.AddressLine1 + "," +
                                  sandwichPlacementJob.Address.AddressLine2 + "," + sandwichPlacementJob.Address.City +
                                  "," + sandwichPlacementJob.Address.Country + "," +
                                  sandwichPlacementJob.Address.PostCode;

                jobDescription.IsVolunteering = false;

                jobDescription.ClosingDate = sandwichPlacementJob.ClosingDate;
                jobDescription.Salary = sandwichPlacementJob.Salary;
                jobDescription.NoOfHoursPerWeek = sandwichPlacementJob.NumberOfHoursWeekly;
                jobDescription.JobDescription = sandwichPlacementJob.OpportunityDescription;
                jobDescription.JobTypeID = 2;

                using (var dbContext = new ProNetTestEntities())
                {
                    job.CeeeJobDescriptions = new Collection<CeeeJobDescription>();
                    job.CeeeJobDescriptions.Add(jobDescription);
                    dbContext.Jobs.Add(job);
                    dbContext.SaveChanges();
                }
                var fileNames = sandwichPlacementJob.Filenames;
                var fileBytes = sandwichPlacementJob.FileUploads;
                //SaveFiles:
                if(fileNames.Count > 0)
                for(int i=0;i<fileNames.Count; i++){

                    SaveJobDocumentByJobId(job.JobId, fileBytes[i], fileNames[i].Substring(fileNames[i].LastIndexOf(".")),fileNames[i]);
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdateSandwichOrPlacementJob(SandwichPlacementTO sandwichPlacementJob, int jobId)
        {
            try
            {
                using (var dbContext = new ProNetTestEntities())
                {
                    var job = dbContext.Jobs.SingleOrDefault(p => p.JobId == jobId);

                    var jobDescription = job.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == job.JobId);


                    var client = job.Client;


                    if (client == null)
                    {
                            client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                            if (client == null) return false;
                    }

                    job.ClientId = client.ClientID;
                    job.Notes = sandwichPlacementJob.OpportunityDescription;
                    job.StartDate = sandwichPlacementJob.StartDate;
                    job.JobRefNo = ("J0" + client.ClientID + Guid.NewGuid().ToString()).Substring(0, 10);
                    job.Published = "N";
                    job.Archived = "N";
                    job.CreatedOn = DateTime.Now;
                    job.UpdatedOn = DateTime.Now;
                    job.StatusDate = DateTime.Now;
                    job.NoOfPlaces = sandwichPlacementJob.NumberOfPositions;
                    job.JobTitle = sandwichPlacementJob.JobRoleTitle;
                    job.WorkAddress = sandwichPlacementJob.Address.AddressLine1 + "," +
                                      sandwichPlacementJob.Address.AddressLine2 + "," +
                                      sandwichPlacementJob.Address.City +
                                      "," + sandwichPlacementJob.Address.Country + "," +
                                      sandwichPlacementJob.Address.PostCode;

                    jobDescription.IsVolunteering = false;

                    jobDescription.ClosingDate = sandwichPlacementJob.ClosingDate;
                    jobDescription.EndDate = sandwichPlacementJob.EndDate;
                    jobDescription.Salary = sandwichPlacementJob.Salary;
                    jobDescription.NoOfHoursPerWeek = sandwichPlacementJob.NumberOfHoursWeekly;
                    jobDescription.JobDescription = sandwichPlacementJob.OpportunityDescription;
                    dbContext.SaveChanges();

                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public OneDayChallengeCharityVolunteerTO GetOnedDayChallengeOrCharityVolunteeringJob(int jobId)
        {
            var oneDayChallengeCharityJob = new OneDayChallengeCharityVolunteerTO();
            using (var dbContext = new ProNetTestEntities())
            {
                var job = dbContext.Jobs.SingleOrDefault(p => p.JobId == jobId);

                var jobDescription = job.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == job.JobId);


                var client = job.Client;

                if (client == null)
                {
                        client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                        if (client == null) return new OneDayChallengeCharityVolunteerTO();
                }
                var oneDayChallenge = jobDescription.CeeeVolunteeringOneDayChallenge;

                oneDayChallengeCharityJob.ContactName = oneDayChallenge.ContactName;
                oneDayChallengeCharityJob.ContactEmail = oneDayChallenge.ContactEmail;
                oneDayChallengeCharityJob.AlternateEmail = oneDayChallenge.AlternateEmail;
                oneDayChallengeCharityJob.OrganisationType = oneDayChallenge.OrganisationType;
                var volunteeringTypeId = oneDayChallenge.VolunteeringTypeId;
                oneDayChallengeCharityJob.OpportunityType =
    (VolunteeringTypes)Enum.Parse(typeof(VolunteeringTypes), dbContext.CeeeVolunteeringTypes.SingleOrDefault(p => p.VolunteeringTypeID == volunteeringTypeId)
             .Name);
                oneDayChallengeCharityJob.ProjectEventTitle = job.JobTitle;
                oneDayChallengeCharityJob.WhatWillBeDoing = jobDescription.JobDescription;

                oneDayChallengeCharityJob.ExperienceNeeded = oneDayChallenge.ExperienceNeeded;
                oneDayChallengeCharityJob.FinishingTime = oneDayChallenge.FinishingTime;
                oneDayChallengeCharityJob.BudgetFormFile = oneDayChallenge.BudgetPlanFile;
                oneDayChallengeCharityJob.HasCarriedOutRiskAssesment = oneDayChallenge.HasCarriedOutRiskAssesment.ToString();
                oneDayChallengeCharityJob.HasInsuranceLiability = oneDayChallenge.HasInsuranceLiability.ToString();
                oneDayChallengeCharityJob.IsCordinatedByUniversity = oneDayChallenge.IsCordinatedByUniversity.ToString();

                oneDayChallengeCharityJob.Location = oneDayChallenge.Location;
                oneDayChallengeCharityJob.MeetingPoint = oneDayChallenge.MeetingPoint;
                oneDayChallengeCharityJob.MeetingTime = oneDayChallenge.MeetingTime;
                oneDayChallengeCharityJob.ProjectEventTitle = oneDayChallenge.ProjectEventTitle;
                oneDayChallengeCharityJob.RiskAssesmentFormFile = oneDayChallenge.RiskAssesmentFile;
                oneDayChallengeCharityJob.SessionPlanFile = oneDayChallenge.SessionPlanFile;
                oneDayChallengeCharityJob.WhatWillBeDoing = oneDayChallenge.WhatWillBeDoing;
                oneDayChallengeCharityJob.When = oneDayChallenge.When;

                oneDayChallengeCharityJob.OpportunityCategoryType = new OpportunityCategoryType();

                var opportunityTypes = oneDayChallenge.OpportunityTypes.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var opportunityType in opportunityTypes)
                {
                    if (opportunityType == "Administration")
                        oneDayChallengeCharityJob.OpportunityCategoryType.Administration = opportunityType;
                    if (opportunityType == "Animals")
                        oneDayChallengeCharityJob.OpportunityCategoryType.Animals = opportunityType;
                    if (opportunityType == "ChildrenYouth")
                        oneDayChallengeCharityJob.OpportunityCategoryType.ChildrenYouth = opportunityType;
                    if (opportunityType == "CultureHeritage")
                        oneDayChallengeCharityJob.OpportunityCategoryType.CultureHeritage = opportunityType;
                    if (opportunityType == "EventProjectManagement")
                        oneDayChallengeCharityJob.OpportunityCategoryType.EventProjectManagement = opportunityType;
                    if (opportunityType == "Education")
                        oneDayChallengeCharityJob.OpportunityCategoryType.Education = opportunityType;
                    if (opportunityType == "Environment")
                        oneDayChallengeCharityJob.OpportunityCategoryType.Environment = opportunityType;
                    if (opportunityType == "EthnicityReligion")
                        oneDayChallengeCharityJob.OpportunityCategoryType.EthnicityReligion = opportunityType;
                    if (opportunityType == "GovernanceFinance")
                        oneDayChallengeCharityJob.OpportunityCategoryType.GovernanceFinance = opportunityType;
                    if (opportunityType == "HealthSocialCare")
                        oneDayChallengeCharityJob.OpportunityCategoryType.HealthSocialCare = opportunityType;
                    if (opportunityType == "IT")
                        oneDayChallengeCharityJob.OpportunityCategoryType.IT = opportunityType;
                    if (opportunityType == "InternationalDevelopment")
                        oneDayChallengeCharityJob.OpportunityCategoryType.InternationalDevelopment = opportunityType;
                    if (opportunityType == "LawCriminalJustice")
                        oneDayChallengeCharityJob.OpportunityCategoryType.LawCriminalJustice = opportunityType;
                    if (opportunityType == "MediaCreative")
                        oneDayChallengeCharityJob.OpportunityCategoryType.MediaCreative = opportunityType;
                    if (opportunityType == "MentoringAdvice")
                        oneDayChallengeCharityJob.OpportunityCategoryType.MentoringAdvice = opportunityType;
                    if (opportunityType == "Politics")
                        oneDayChallengeCharityJob.OpportunityCategoryType.Politics = opportunityType;
                    if (opportunityType == "SocialEnterprise")
                    oneDayChallengeCharityJob.OpportunityCategoryType.SocialEnterprise = opportunityType;
                    if (opportunityType == "SportsOutdoor")
                        oneDayChallengeCharityJob.OpportunityCategoryType.SportsOutdoor = opportunityType;
                    if (opportunityType == "VulnerableAdults")
                        oneDayChallengeCharityJob.OpportunityCategoryType.VulnerableAdults = opportunityType;
                }
            }

            return oneDayChallengeCharityJob;
        }

        public bool SaveOnedDayChallengeOrCharityVolunteeringJob(
            OneDayChallengeCharityVolunteerTO oneDayChallengeCharityJob, int clientID)
        {
            try
            {
                var job = new Job();
                var jobDescription = new CeeeJobDescription();
                var oneDayChallenge = new CeeeVolunteeringOneDayChallenge();
                var volunteeringType = oneDayChallengeCharityJob.OpportunityType.ToString();
                var client = GetClientByID(clientID);

                if (client == null)
                {
                    using (var dbContext = new ProNetTestEntities())
                    {
                        client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                        if (client == null) return false;
                    }
                }

                oneDayChallenge.OrganisationType = oneDayChallengeCharityJob.OrganisationType;
                job.ClientId = client.ClientID;
                job.JobRefNo = ("J0" + client.ClientID + Guid.NewGuid().ToString()).Substring(0, 10);
                job.Notes = oneDayChallengeCharityJob.ExperienceNeeded + "; When: " + oneDayChallengeCharityJob.When + "; Finishing Time: " +
                            oneDayChallengeCharityJob.FinishingTime;
                job.Published = "N";
                job.Archived = "N";
                job.CreatedOn = DateTime.Now;
                job.UpdatedOn = DateTime.Now;
                job.StatusDate = DateTime.Now;
                job.JobTitle = oneDayChallengeCharityJob.ProjectEventTitle;
                jobDescription.IsVolunteering = true;
                jobDescription.JobDescription = oneDayChallengeCharityJob.WhatWillBeDoing;
                jobDescription.JobTypeID = 3;

                oneDayChallenge.ContactName = oneDayChallengeCharityJob.ContactName;
                oneDayChallenge.ContactEmail = oneDayChallengeCharityJob.ContactEmail;
                oneDayChallenge.AlternateEmail = oneDayChallengeCharityJob.AlternateEmail;

                var opportunityTypes = string.Empty;
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Administration + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Animals + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.ChildrenYouth + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.CultureHeritage + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Education + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Environment + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.EthnicityReligion + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.EventProjectManagement + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.GovernanceFinance + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.HealthSocialCare + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.IT + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.InternationalDevelopment + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.LawCriminalJustice + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.MediaCreative + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.MentoringAdvice + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Politics + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.SocialEnterprise + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.SportsOutdoor + ",";
                opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.VulnerableAdults;

                oneDayChallenge.OpportunityTypes = opportunityTypes;
                oneDayChallenge.ExperienceNeeded = oneDayChallengeCharityJob.ExperienceNeeded;
                oneDayChallenge.FinishingTime = oneDayChallengeCharityJob.FinishingTime;
                oneDayChallenge.BudgetPlanFile = oneDayChallengeCharityJob.BudgetFormFile;
                oneDayChallenge.HasCarriedOutRiskAssesment =
                    !string.IsNullOrEmpty(oneDayChallengeCharityJob.HasCarriedOutRiskAssesment) ? true : false;
                oneDayChallenge.HasInsuranceLiability =
                    !string.IsNullOrEmpty(oneDayChallengeCharityJob.HasInsuranceLiability) ? true : false;

                oneDayChallenge.IsCordinatedByUniversity =
                    !string.IsNullOrEmpty(oneDayChallengeCharityJob.IsCordinatedByUniversity) ? true : false;
                oneDayChallenge.Location = oneDayChallengeCharityJob.Location;
                oneDayChallenge.MeetingPoint = oneDayChallengeCharityJob.MeetingPoint;
                oneDayChallenge.MeetingTime = oneDayChallengeCharityJob.MeetingTime;
                oneDayChallenge.ProjectEventTitle = oneDayChallengeCharityJob.ProjectEventTitle;
                oneDayChallenge.RiskAssesmentFile = oneDayChallengeCharityJob.RiskAssesmentFormFile;
                oneDayChallenge.SessionPlanFile = oneDayChallengeCharityJob.SessionPlanFile;
                oneDayChallenge.WhatWillBeDoing = oneDayChallengeCharityJob.WhatWillBeDoing;
                oneDayChallenge.When = oneDayChallengeCharityJob.When;

                using (var dbContext = new ProNetTestEntities())
                {
                    var volunteeringTypes =
                        dbContext.CeeeVolunteeringTypes.SingleOrDefault(
                            p => p.Name == volunteeringType);

                    job.CeeeJobDescriptions = new Collection<CeeeJobDescription>();
                    oneDayChallenge.VolunteeringTypeId = volunteeringTypes.VolunteeringTypeID;
                    jobDescription.VolunteeringTypeID = volunteeringTypes.VolunteeringTypeID;
                    jobDescription.CeeeVolunteeringOneDayChallenge = oneDayChallenge;
                    job.CeeeJobDescriptions.Add(jobDescription);
                    dbContext.Jobs.Add(job);
                    dbContext.SaveChanges();
                }


                //SaveFiles:
                if(!string.IsNullOrEmpty(oneDayChallengeCharityJob.BudgetFormFileName))
                SaveJobDocumentByJobId(job.JobId, oneDayChallengeCharityJob.BudgetFormFile, oneDayChallengeCharityJob.BudgetFormFileName.Substring(oneDayChallengeCharityJob.BudgetFormFileName.LastIndexOf(".")), oneDayChallengeCharityJob.BudgetFormFileName);
                //SaveFiles:
                if(!string.IsNullOrEmpty(oneDayChallengeCharityJob.RiskAssesmentFormFileName))
                SaveJobDocumentByJobId(job.JobId, oneDayChallengeCharityJob.RiskAssesmentFormFile, oneDayChallengeCharityJob.RiskAssesmentFormFileName.Substring(oneDayChallengeCharityJob.RiskAssesmentFormFileName.LastIndexOf(".")), oneDayChallengeCharityJob.RiskAssesmentFormFileName);
                //SaveFiles:
                if(!string.IsNullOrEmpty(oneDayChallengeCharityJob.SessionPlanFileName))
                    SaveJobDocumentByJobId(job.JobId, oneDayChallengeCharityJob.SessionPlanFile, oneDayChallengeCharityJob.SessionPlanFileName.Substring(oneDayChallengeCharityJob.SessionPlanFileName.LastIndexOf(".")), oneDayChallengeCharityJob.SessionPlanFileName);
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateOnedDayChallengeOrCharityVolunteeringJob(
                    OneDayChallengeCharityVolunteerTO oneDayChallengeCharityJob, int jobId)
        {
            try
            {
                using (var dbContext = new ProNetTestEntities())
                {
                    var job = dbContext.Jobs.SingleOrDefault(p => p.JobId == jobId);

                    var jobDescription = job.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == job.JobId);


                    var client = job.Client;

                    var oneDayChallenge = jobDescription.CeeeVolunteeringOneDayChallenge;

                    var volunteeringType = oneDayChallengeCharityJob.OpportunityType.ToString();

                    if (client == null)
                    {
                        client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                        if (client == null) return false;
                    }

                    oneDayChallenge.OrganisationType = oneDayChallengeCharityJob.OrganisationType;
                    oneDayChallenge.ContactName = oneDayChallengeCharityJob.ContactName;
                    oneDayChallenge.ContactEmail = oneDayChallengeCharityJob.ContactEmail;
                    oneDayChallenge.AlternateEmail = oneDayChallengeCharityJob.AlternateEmail;

                    job.ClientId = client.ClientID;
                    job.JobRefNo = ("J0" + client.ClientID + Guid.NewGuid().ToString()).Substring(0, 10);
                    job.Notes = oneDayChallengeCharityJob.ExperienceNeeded + "; When: " + oneDayChallengeCharityJob.When +
                                "; Finishing Time: " +
                                oneDayChallengeCharityJob.FinishingTime;
                    job.Published = "N";
                    job.Archived = "N";
                    job.CreatedOn = DateTime.Now;
                    job.UpdatedOn = DateTime.Now;
                    job.StatusDate = DateTime.Now;
                    job.JobTitle = oneDayChallengeCharityJob.ProjectEventTitle;
                    jobDescription.IsVolunteering = true;
                    jobDescription.JobDescription = oneDayChallengeCharityJob.WhatWillBeDoing;

                    oneDayChallenge.ExperienceNeeded = oneDayChallengeCharityJob.ExperienceNeeded;
                    oneDayChallenge.FinishingTime = oneDayChallengeCharityJob.FinishingTime;
                    oneDayChallenge.BudgetPlanFile = oneDayChallengeCharityJob.BudgetFormFile;
                    oneDayChallenge.HasCarriedOutRiskAssesment =
                        !string.IsNullOrEmpty(oneDayChallengeCharityJob.HasCarriedOutRiskAssesment) ? true : false;
                    oneDayChallenge.HasInsuranceLiability =
                        !string.IsNullOrEmpty(oneDayChallengeCharityJob.HasInsuranceLiability) ? true : false;
                    oneDayChallenge.IsCordinatedByUniversity =
                        !string.IsNullOrEmpty(oneDayChallengeCharityJob.IsCordinatedByUniversity) ? true : false;
                    oneDayChallenge.Location = oneDayChallengeCharityJob.Location;
                    oneDayChallenge.MeetingPoint = oneDayChallengeCharityJob.MeetingPoint;
                    oneDayChallenge.MeetingTime = oneDayChallengeCharityJob.MeetingTime;
                    oneDayChallenge.ProjectEventTitle = oneDayChallengeCharityJob.ProjectEventTitle;
                    oneDayChallenge.RiskAssesmentFile = oneDayChallengeCharityJob.RiskAssesmentFormFile;
                    oneDayChallenge.SessionPlanFile = oneDayChallengeCharityJob.SessionPlanFile;
                    oneDayChallenge.WhatWillBeDoing = oneDayChallengeCharityJob.WhatWillBeDoing;
                    oneDayChallenge.When = oneDayChallengeCharityJob.When;
                    var opportunityTypes = string.Empty;
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Administration + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Animals + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.ChildrenYouth + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.CultureHeritage  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Education  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Environment  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.EthnicityReligion  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.EventProjectManagement  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.GovernanceFinance  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.HealthSocialCare  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.IT  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.InternationalDevelopment  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.LawCriminalJustice  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.MediaCreative  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.MentoringAdvice  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.Politics  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.SocialEnterprise  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.SportsOutdoor  + ",";
                    opportunityTypes += oneDayChallengeCharityJob.OpportunityCategoryType.VulnerableAdults;

                    oneDayChallenge.OpportunityTypes = opportunityTypes;
                    var volunteeringTypes = dbContext.CeeeVolunteeringTypes.
                        SingleOrDefault(p => p.Name == volunteeringType);

                    oneDayChallenge.VolunteeringTypeId = volunteeringTypes.VolunteeringTypeID;
                    jobDescription.CeeeVolunteeringOneDayChallenge = oneDayChallenge;
                    dbContext.SaveChanges();

                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PlacementsInternationalVolunteerTO GetPlacementOrInternationalVolunteeringJob(int jobId)
        {
            var volunteeringPlacementJob = new PlacementsInternationalVolunteerTO();

            using (var dbContext = new ProNetTestEntities())
            {
                var job = dbContext.Jobs.SingleOrDefault(p => p.JobId == jobId);

                var jobDescription = job.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == job.JobId);


                var placementInternationalVol = jobDescription.CeeeVolunteeringPlacement;
                var volunteeringTypeId = jobDescription.VolunteeringTypeID;
                volunteeringPlacementJob.OpportunityType =
                    (VolunteeringTypes)Enum.Parse(typeof(VolunteeringTypes), dbContext.CeeeVolunteeringTypes.SingleOrDefault(p => p.VolunteeringTypeID == volunteeringTypeId)
                             .Name);
                volunteeringPlacementJob.ContactName = placementInternationalVol.ContactName;
                volunteeringPlacementJob.ContactEmail = placementInternationalVol.ContactEmail;
                volunteeringPlacementJob.AlternateEmail = placementInternationalVol.AlternateEmail;
                volunteeringPlacementJob.AboutOrganisation = placementInternationalVol.AboutOrganisation;
                volunteeringPlacementJob.RoleTitle = job.JobTitle;
                volunteeringPlacementJob.TrainingDetails = jobDescription.JobDescription;
                volunteeringPlacementJob.DurationNeeded = placementInternationalVol.DurationNeeded;
                volunteeringPlacementJob.TrainingDetails = placementInternationalVol.TrainingDetails;
                volunteeringPlacementJob.DayTimeRequired = placementInternationalVol.DayTimeRequired;
                volunteeringPlacementJob.HasCarriedOutRiskAssesment = placementInternationalVol.HasCarriedOutRiskAssesment.ToString();
                volunteeringPlacementJob.HasInsuranceLiability = placementInternationalVol.HasLiabilityInsurance.ToString();
                volunteeringPlacementJob.HasNotCarriedOutRiskAssesment = (!placementInternationalVol.HasCarriedOutRiskAssesment).ToString();
                volunteeringPlacementJob.HasNoInsuranceLiability = (!placementInternationalVol.HasLiabilityInsurance).ToString();

                volunteeringPlacementJob.Location = placementInternationalVol.Location;
                volunteeringPlacementJob.DurationNeeded = placementInternationalVol.DurationNeeded;
                var HowToPost = !string.IsNullOrEmpty(placementInternationalVol.HowToPostApplication)?placementInternationalVol.HowToPostApplication.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries): new  string[]{""};
                foreach (var howPost in HowToPost)
                {
                    if (howPost.StartsWith("ApplicationReceiveWebsite"))
                        volunteeringPlacementJob.ApplicationReceiveWebsite = "ApplicationReceiveWebsite";
                    if (howPost.StartsWith("ApplicationReceiveEmail"))
                        volunteeringPlacementJob.ApplicationReceiveEmail = "ApplicationReceiveEmail";
                }
                volunteeringPlacementJob.NumberOfVacancies = placementInternationalVol.NumberOfPositions;
                volunteeringPlacementJob.RoleTitle = placementInternationalVol.RoleTitle;
                volunteeringPlacementJob.SkillsRequired = placementInternationalVol.SkillsRequired;
                volunteeringPlacementJob.OpportunityCategoryType = new OpportunityCategoryType();
                volunteeringPlacementJob.OrganisationType = placementInternationalVol.OrganisationType;

                var applicationMethod = !string.IsNullOrEmpty(placementInternationalVol.ApplicationMethod) ? placementInternationalVol.ApplicationMethod.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : new string[] { "" };

                foreach (var appMethod in applicationMethod)
                {
                    if (appMethod.StartsWith("ApplicationMethodCv"))
                        volunteeringPlacementJob.ApplicationMethodCv = "ApplicationMethodCv";
                    if (appMethod.StartsWith("ApplicationMethodCoverLetter"))
                        volunteeringPlacementJob.ApplicationMethodCoverLetter = "ApplicationMethodCoverLetter";
                    if (appMethod.StartsWith("ApplicationMethodForm"))
                        volunteeringPlacementJob.ApplicationMethodForm = "ApplicationMethodForm";
                }
                var opportunityTypes = placementInternationalVol.OpportunityTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var opportunityType in opportunityTypes)
                {
                    if (opportunityType == "Administration")
                        volunteeringPlacementJob.OpportunityCategoryType.Administration = opportunityType;
                    if (opportunityType == "Animals")
                        volunteeringPlacementJob.OpportunityCategoryType.Animals = opportunityType;
                    if (opportunityType == "ChildrenYouth")
                        volunteeringPlacementJob.OpportunityCategoryType.ChildrenYouth = opportunityType;
                    if (opportunityType == "CultureHeritage")
                        volunteeringPlacementJob.OpportunityCategoryType.CultureHeritage = opportunityType;
                    if (opportunityType == "EventProjectManagement")
                        volunteeringPlacementJob.OpportunityCategoryType.EventProjectManagement = opportunityType;
                    if (opportunityType == "Education")
                        volunteeringPlacementJob.OpportunityCategoryType.Education = opportunityType;
                    if (opportunityType == "Environment")
                        volunteeringPlacementJob.OpportunityCategoryType.Environment = opportunityType;
                    if (opportunityType == "EthnicityReligion")
                        volunteeringPlacementJob.OpportunityCategoryType.EthnicityReligion = opportunityType;
                    if (opportunityType == "GovernanceFinance")
                        volunteeringPlacementJob.OpportunityCategoryType.GovernanceFinance = opportunityType;
                    if (opportunityType == "HealthSocialCare")
                        volunteeringPlacementJob.OpportunityCategoryType.HealthSocialCare = opportunityType;
                    if (opportunityType == "IT")
                        volunteeringPlacementJob.OpportunityCategoryType.IT = opportunityType;
                    if (opportunityType == "InternationalDevelopment")
                        volunteeringPlacementJob.OpportunityCategoryType.InternationalDevelopment = opportunityType;
                    if (opportunityType == "LawCriminalJustice")
                        volunteeringPlacementJob.OpportunityCategoryType.LawCriminalJustice = opportunityType;
                    if (opportunityType == "MediaCreative")
                        volunteeringPlacementJob.OpportunityCategoryType.MediaCreative = opportunityType;
                    if (opportunityType == "MentoringAdvice")
                        volunteeringPlacementJob.OpportunityCategoryType.MentoringAdvice = opportunityType;
                    if (opportunityType == "Politics")
                        volunteeringPlacementJob.OpportunityCategoryType.Politics = opportunityType;
                    if (opportunityType == "SocialEnterprise")
                        volunteeringPlacementJob.OpportunityCategoryType.SocialEnterprise = opportunityType;
                    if (opportunityType == "SportsOutdoor")
                        volunteeringPlacementJob.OpportunityCategoryType.SportsOutdoor = opportunityType;
                    if (opportunityType == "VulnerableAdults")
                        volunteeringPlacementJob.OpportunityCategoryType.VulnerableAdults = opportunityType;
                }
            }

            return volunteeringPlacementJob;
        }

        public bool SavePlacementOrInternationalVolunteeringJob(
            PlacementsInternationalVolunteerTO volunteeringPlacementJob, int clientID)
        {
            try
            {
                var job = new Job();
                var jobDescription = new CeeeJobDescription();
                var placementInternationalVol = new CeeeVolunteeringPlacement();
                var volunteeringType = volunteeringPlacementJob.OpportunityType.ToString();

                var client = GetClientByID(clientID);
                if (client == null) return false;
                placementInternationalVol.AboutOrganisation = volunteeringPlacementJob.AboutOrganisation;
                placementInternationalVol.ContactName = volunteeringPlacementJob.ContactName;
                placementInternationalVol.ContactEmail = volunteeringPlacementJob.ContactEmail;
                placementInternationalVol.AlternateEmail = volunteeringPlacementJob.AlternateEmail;
                placementInternationalVol.OrganisationType = volunteeringPlacementJob.OrganisationType;
                job.ClientId = client.ClientID;
                job.Notes = volunteeringPlacementJob.RoleTitle + "; Skills required:" +
                            volunteeringPlacementJob.SkillsRequired + "; EmploymentOpportunityType: " +
                            volunteeringPlacementJob.OpportunityCategoryType.Administration ?? "" +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .ChildrenYouth +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .CultureHeritage +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .Education +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .Environment +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .SocialEnterprise +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .EthnicityReligion +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .EventProjectManagement +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .GovernanceFinance +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .HealthSocialCare +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .IT +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .InternationalDevelopment +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .LawCriminalJustice +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .MediaCreative +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .MentoringAdvice +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .Politics +
                                                                                               "," +
                                                                                               volunteeringPlacementJob
                                                                                                   .OpportunityCategoryType
                                                                                                   .SportsOutdoor +
                            "," + volunteeringPlacementJob.OpportunityCategoryType.VulnerableAdults;
                job.JobRefNo = ("J0" + client.ClientID + Guid.NewGuid().ToString()).Substring(0, 10);
                job.Published = "N";
                job.Archived = "N";
                job.CreatedOn = DateTime.Now;
                job.UpdatedOn = DateTime.Now;
                job.StatusDate = DateTime.Now;
                job.JobTitle = volunteeringPlacementJob.RoleTitle;

                jobDescription.IsVolunteering = true;
                jobDescription.JobDescription = volunteeringPlacementJob.TrainingDetails;
                jobDescription.JobTypeID = 4;

                placementInternationalVol.OrganisationType = volunteeringPlacementJob.OrganisationType;
                placementInternationalVol.TrainingDetails = volunteeringPlacementJob.TrainingDetails;
                placementInternationalVol.DayTimeRequired = volunteeringPlacementJob.DayTimeRequired;
                placementInternationalVol.HasCarriedOutRiskAssesment =
                    !string.IsNullOrEmpty(volunteeringPlacementJob.HasCarriedOutRiskAssesment) ? true : false;
                placementInternationalVol.HasLiabilityInsurance =
                    !string.IsNullOrEmpty(volunteeringPlacementJob.HasInsuranceLiability) ? true : false;

                placementInternationalVol.Location = volunteeringPlacementJob.Location;
                placementInternationalVol.DurationNeeded = volunteeringPlacementJob.DurationNeeded;
                placementInternationalVol.HowToPostApplication =volunteeringPlacementJob.ApplicationReceiveEmail+","+
                volunteeringPlacementJob.ApplicationReceiveWebsite + ",";
                placementInternationalVol.NumberOfPositions = volunteeringPlacementJob.NumberOfVacancies;
                placementInternationalVol.RoleTitle = volunteeringPlacementJob.RoleTitle;
                placementInternationalVol.SkillsRequired = volunteeringPlacementJob.SkillsRequired;
                placementInternationalVol.ApplicationMethod = volunteeringPlacementJob.ApplicationMethodCoverLetter + "," +
                                                              volunteeringPlacementJob.ApplicationMethodCv + "," +
                                                              volunteeringPlacementJob.ApplicationMethodForm;

                var opportunityTypes = string.Empty;
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Administration + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Animals + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.ChildrenYouth + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.CultureHeritage + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Education + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Environment + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.EthnicityReligion + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.EventProjectManagement + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.GovernanceFinance + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.HealthSocialCare + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.IT + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.InternationalDevelopment + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.LawCriminalJustice + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.MediaCreative + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.MentoringAdvice + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Politics + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.SocialEnterprise + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.SportsOutdoor + ",";
                opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.VulnerableAdults;

                placementInternationalVol.OpportunityTypes = opportunityTypes;

                using (var dbContext = new ProNetTestEntities())
                {
                    placementInternationalVol.VolunteeringTypeID = dbContext.CeeeVolunteeringTypes.SingleOrDefault(p => p.Name == volunteeringType).VolunteeringTypeID;

                    dbContext.CeeeVolunteeringPlacements.Add(placementInternationalVol);
                    dbContext.SaveChanges();
                    var volunteeringTypes = dbContext.CeeeVolunteeringTypes.
                        SingleOrDefault(p => p.Name == volunteeringType);
                    placementInternationalVol.VolunteeringTypeID = volunteeringTypes.VolunteeringTypeID;
                    jobDescription.VolunteeringPlacmentID = placementInternationalVol.VolunteeringJobID;
                    jobDescription.VolunteeringTypeID = placementInternationalVol.VolunteeringTypeID;
                    dbContext.CeeeJobDescriptions.Add(jobDescription);
                    job.CeeeJobDescriptions = new Collection<CeeeJobDescription>();
                    jobDescription.CeeeVolunteeringPlacement = placementInternationalVol;
                    job.CeeeJobDescriptions.Add(jobDescription);
                    dbContext.Jobs.Add(job);
                    dbContext.SaveChanges();
                }
                //SaveFiles:
                if (!string.IsNullOrEmpty(volunteeringPlacementJob.ApplicationFormFileName))
                    SaveJobDocumentByJobId(job.JobId, volunteeringPlacementJob.ApplicationFormFile, volunteeringPlacementJob.ApplicationFormFileName.Substring(volunteeringPlacementJob.ApplicationFormFileName.LastIndexOf(".")), volunteeringPlacementJob.ApplicationFormFileName);
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdatePlacementOrInternationalVolunteeringJob(
            PlacementsInternationalVolunteerTO volunteeringPlacementJob, int jobId)
        {
            try
            {
                using (var dbContext = new ProNetTestEntities())
                {
                    var job = dbContext.Jobs.SingleOrDefault(p => p.JobId == jobId);

                    var jobDescription = job.CeeeJobDescriptions.SingleOrDefault(p => p.JobID == job.JobId);


                    var client = job.Client;
                    var placementInternationalVol = jobDescription.CeeeVolunteeringPlacement;
                    var volunteeringType = volunteeringPlacementJob.OpportunityType.ToString();
                    placementInternationalVol.VolunteeringTypeID = dbContext.CeeeVolunteeringTypes.SingleOrDefault(p => p.Name == volunteeringType).VolunteeringTypeID;
                    placementInternationalVol.AboutOrganisation = volunteeringPlacementJob.AboutOrganisation;
                    placementInternationalVol.ContactName = volunteeringPlacementJob.ContactName;
                    placementInternationalVol.ContactEmail = volunteeringPlacementJob.ContactEmail;
                    placementInternationalVol.AlternateEmail = volunteeringPlacementJob.AlternateEmail;

                    if (client == null)
                    {
                        if (client == null)
                            client = dbContext.Clients.SingleOrDefault(p => p.ClientID == job.ClientId);
                        if(client == null)return false;
                    }

                    job.ClientId = client.ClientID;
                    job.Notes = volunteeringPlacementJob.RoleTitle + "; Skills required:" +
                                volunteeringPlacementJob.SkillsRequired + "; EmploymentOpportunityType: " +
                                volunteeringPlacementJob.OpportunityCategoryType.Administration ?? "" +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .ChildrenYouth +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .CultureHeritage +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .Education +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .Environment +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .SocialEnterprise +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .EthnicityReligion +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .EventProjectManagement +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .GovernanceFinance +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .HealthSocialCare +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .IT +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .InternationalDevelopment +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .LawCriminalJustice +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .MediaCreative +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .MentoringAdvice +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .Politics +
                                                                                                   "," +
                                                                                                   volunteeringPlacementJob
                                                                                                       .OpportunityCategoryType
                                                                                                       .SportsOutdoor +
                                "," + volunteeringPlacementJob.OpportunityCategoryType.VulnerableAdults;
                    job.JobRefNo = ("J0" + client.ClientID + Guid.NewGuid().ToString()).Substring(0, 10);
                    job.Published = "N";
                    job.Archived = "N";
                    job.CreatedOn = DateTime.Now;
                    job.UpdatedOn = DateTime.Now;
                    job.StatusDate = DateTime.Now;
                    job.JobTitle = volunteeringPlacementJob.RoleTitle;

                    jobDescription.IsVolunteering = true;
                    jobDescription.JobDescription = volunteeringPlacementJob.TrainingDetails;

                    placementInternationalVol.ApplicationMethod = volunteeringPlacementJob.ApplicationMethodCoverLetter + "," +
                                                                  volunteeringPlacementJob.ApplicationMethodCv + "," +
                                                                  volunteeringPlacementJob.ApplicationMethodForm;
                    placementInternationalVol.TrainingDetails = volunteeringPlacementJob.TrainingDetails;
                    placementInternationalVol.DayTimeRequired = volunteeringPlacementJob.DayTimeRequired;
                    placementInternationalVol.HasCarriedOutRiskAssesment =
                        !string.IsNullOrEmpty(volunteeringPlacementJob.HasCarriedOutRiskAssesment) ? true : false;
                    placementInternationalVol.HasLiabilityInsurance =
                        !string.IsNullOrEmpty(volunteeringPlacementJob.HasInsuranceLiability) ? true : false;

                    placementInternationalVol.Location = volunteeringPlacementJob.Location;
                    placementInternationalVol.DurationNeeded = volunteeringPlacementJob.DurationNeeded;
                    placementInternationalVol.HowToPostApplication = volunteeringPlacementJob.ApplicationReceiveEmail + "," +
                    volunteeringPlacementJob.ApplicationReceiveWebsite + ",";
                    placementInternationalVol.NumberOfPositions = volunteeringPlacementJob.NumberOfVacancies;
                    placementInternationalVol.RoleTitle = volunteeringPlacementJob.RoleTitle;
                    placementInternationalVol.SkillsRequired = volunteeringPlacementJob.SkillsRequired;
                    var opportunityTypes = string.Empty;
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Administration + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Animals + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.ChildrenYouth + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.CultureHeritage + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Education + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Environment + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.EthnicityReligion + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.EventProjectManagement + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.GovernanceFinance + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.HealthSocialCare + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.IT + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.InternationalDevelopment + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.LawCriminalJustice + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.MediaCreative + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.MentoringAdvice + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.Politics + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.SocialEnterprise + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.SportsOutdoor + ",";
                    opportunityTypes += volunteeringPlacementJob.OpportunityCategoryType.VulnerableAdults;

                    placementInternationalVol.OpportunityTypes = opportunityTypes;
                    var volunteeringTypes = dbContext.CeeeVolunteeringTypes.
                                                         SingleOrDefault(p => p.Name == volunteeringType);
                    placementInternationalVol.VolunteeringTypeID = volunteeringTypes.VolunteeringTypeID;
                    jobDescription.VolunteeringPlacmentID = placementInternationalVol.VolunteeringJobID;
                    dbContext.SaveChanges();

                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private bool SaveJobDocumentByJobId(int jobId, byte[] docContent,string fileExtension,string fileName)
        {
            using (var dbContext = new ProNetTestEntities())
            {
                try
                {
                    var jobDocument = new JobDocument();
                    var document = new Document();
                    var documentContent = new DocumentContent();

                    documentContent.CreatedOn = DateTime.Now;
                    documentContent.CreatedUserId = 1;
                    documentContent.Document = docContent;
                    documentContent.FileExtension = fileExtension;

                    document.CreatedOn = DateTime.Now;
                    document.CreatedUserId = 1;
                    document.Description = fileName;
                    dbContext.Documents.Add(document);
                    dbContext.SaveChanges();

                    documentContent.DocumentId = document.DocumentID;
                    document.DocumentContent = documentContent;
                    dbContext.DocumentContents.Add(documentContent);

                    dbContext.SaveChanges();

                    jobDocument.CreatedOn = DateTime.Now;
                    jobDocument.CreatedUserId = 1;
                    jobDocument.JobId = jobId;

                    dbContext.SaveChanges();

                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public IEnumerable<JobUpdateTO> GetClientJobsByClientId(int clientID)
        {
            IEnumerable<JobUpdateTO> jobDetails = null;
            IList<JobUpdateTO> list = null;
            using (var dbContext = new ProNetTestEntities())
            {
                jobDetails = from j in dbContext.Jobs
                       from jd in dbContext.CeeeJobDescriptions
                       where j.JobId == jd.JobID && j.ClientId == clientID
                       select new JobUpdateTO
                           {
                               JobId = j.JobId,
                               JobTitle = j.JobTitle,
                               JobDescription = jd.JobDescription,
                               IsVolunteering = jd.IsVolunteering,
                               DateCreated = j.CreatedOn,
                               DateUpdate = j.UpdatedOn
                           };

                list = new List<JobUpdateTO>();
                foreach (var jd in jobDetails)
                    list.Add(jd);
                return list;
            }
            
        }
        public AddressTO GetClientAddressByName(string clientName)
        {
            using (var dbContext = new ProNetTestEntities())
            {
                var client = dbContext.Clients.SingleOrDefault(p => p.Company == clientName);

                if (client != null)
                {
                    return new AddressTO
                        {
                            AddressLine1 = client.Company,
                            AddressLine2 = client.Location.Description.Split(new char[] { ',' })[0],
                            City = client.Location.Description.Split(new char[] { ',' })[1],
                            Country = client.Location.Description.Split(new char[] { ',' })[2],
                            PostCode = client.Location.Code
                        };
                }
                else return null;
            }

        }


        public OrgRegFormTO GetClientDetailsByID(int clientID)
        {
            var clientTO = new OrgRegFormTO();

            using (var dbContext = new ProNetTestEntities())
            {
                var client = dbContext.Clients.SingleOrDefault(p => p.ClientID == clientID);

                if (client != null)
                {
                    clientTO.Address = new AddressTO();
                    clientTO.Address.AddressLine1 = client.Location.Description.Split(new char[] { ',' })[0];
                    clientTO.Address.AddressLine2 = client.Location.Description.Split(new char[] { ',' })[1];
                    clientTO.Address.City = client.Location.Description.Split(new char[] { ',' })[2];
                    clientTO.Address.Country = client.Location.Description.Split(new char[] { ',' })[3];
                    clientTO.Address.PostCode = client.Location.Code;
                    clientTO.BusinessRegNumber = client.RegNo;

                    clientTO.OrganisationName = client.Company;
                    var user = client.CeeeUsers.SingleOrDefault();
                    clientTO.UserId = user.UserID;
                    //Deal with OpportunityTypes:
                    var orgDetails = user.CeeeOpportunityTypes;

                    var oppTypeTO = new OpportunityTypeTO();

                    foreach (var opp in orgDetails)
                    {
                        if (opp.OpportunityName == "OpportunityType.AirLineAndAirport")
                            oppTypeTO.AirLineAndAirport = "OpportunityType.AirLineAndAirport";
                        if (opp.OpportunityName == "OpportunityType.Education")
                            oppTypeTO.Education = "OpportunityType.Education";
                        if (opp.OpportunityName == "OpportunityType.Hospitality")
                            oppTypeTO.Hospitality = "OpportunityType.Hospitality";
                        if (opp.OpportunityName == "OpportunityType.RecruitmentAgency")
                            oppTypeTO.RecruitmentAgency = "OpportunityType.RecruitmentAgency";
                        if (opp.OpportunityName == "OpportunityType.SportAndLeisure")
                            oppTypeTO.SportAndLeisure = "OpportunityType.SportAndLeisure";
                        if (opp.OpportunityName == "OpportunityType.Other")
                            oppTypeTO.Other = "OpportunityType.Other";
                    }
                    clientTO.OpportunityType = oppTypeTO;
                    clientTO.CompanySize = user.CompanySize;
                    clientTO.Email = user.Email;
                    clientTO.HowHeard = user.HowHeard;
                    clientTO.JobTitle = user.JobTitle;
                    clientTO.LandLineNo = user.Phone;
                    clientTO.FirstName = user.FirstName;
                    clientTO.LastName = user.LastName;
                    clientTO.OrganisationDoes = client.Notes;
                    clientTO.OrgWebSite = user.OrgWebsite;
                    clientTO.Title = (Title)Enum.Parse(typeof(Title), user.Title);

                }
            }

            return clientTO;
        }

        public AddressTO GetClientAddressByID(int clientID)
        {
            using (var dbContext = new ProNetTestEntities())
            {
                var client = dbContext.Clients.SingleOrDefault(p => p.ClientID == clientID);

                if (client != null)
                {
                    return new AddressTO
                        {
                            AddressLine1 = client.Company,
                            AddressLine2 = client.Location.Description.Split(new char[] { ',' })[0],
                            City = client.Location.Description.Split(new char[] { ',' })[1],
                            Country = client.Location.Description.Split(new char[] { ',' })[2],
                            PostCode = client.Location.Code
                        };
                }
                else return null;
            }
        }

        public DataAccess.Client GetClientByID(int clientID)
        {
            using (var dbContext = new ProNetTestEntities())
            {
                return dbContext.Clients.SingleOrDefault(p => p.ClientID == clientID);
            }
        }

        public int GetClientIdByName(string name)
        {
            using (var dbContext = new ProNetTestEntities())
            {
                var user = dbContext.CeeeUsers.SingleOrDefault(p => p.Email == name && p.UserID > 0);

                if (user != null)
                {
                    return (int)user.ClientID;
                }
                else return -1;
            }
        }
        public int GetUserIdByUsername(string name)
        {
            using (var dbContext = new ProNetTestEntities())
            {
                var user = dbContext.CeeeUsers.SingleOrDefault(p => p.Email == name);
                if (user != null) return user.UserID;
                return -1;
            }
        }
    }
}
