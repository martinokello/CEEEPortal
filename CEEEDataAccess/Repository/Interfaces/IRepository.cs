using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEEEDataAccess.DataAccess;
using CEEEDataAccess.DataAccessTOs;
using CEEEPortal.DataAccessTOs;

namespace CEEEDataAccess.Repository.Interfaces
{
    public interface IRepository
    {
        bool SaveClientDetails(OrgRegFormTO orgDetails);
        bool UpdateClientDetails(OrgRegFormTO orgDetails);
        OrgRegFormTO GetClientDetailsByID(int clientID);
        bool SaveGraduateOrStudentJob(GraduateEmployerTO graduateOrStudentJob, int clientID);
        bool SaveSandwichOrPlacementJob(SandwichPlacementTO sandwichPlacementJob, int clientID);
        bool SaveOnedDayChallengeOrCharityVolunteeringJob(OneDayChallengeCharityVolunteerTO oneDayChallengeCharityJob, int clientID);
        bool SavePlacementOrInternationalVolunteeringJob(PlacementsInternationalVolunteerTO volunteeringPlacementJob, int clientID);
        AddressTO GetClientAddressByID(int clientID);
        bool UpdateGraduateOrStudentJob(GraduateEmployerTO graduateOrStudentJob, int clientID);
        bool UpdateSandwichOrPlacementJob(SandwichPlacementTO sandwichPlacementJob, int clientID);
        bool UpdateOnedDayChallengeOrCharityVolunteeringJob(OneDayChallengeCharityVolunteerTO oneDayChallengeCharityJob, int clientID);
        bool UpdatePlacementOrInternationalVolunteeringJob(PlacementsInternationalVolunteerTO volunteeringPlacementJob, int clientID);
        Client GetClientByID(int clientID);
        int GetClientIdByName(string name);
        int GetUserIdByUsername(string name);
        bool CheckUserNotExists(string username);
        IEnumerable<JobUpdateTO> GetClientJobsByClientId(int clientID);
        int? GetJobTypeById(int jobId);
        GraduateEmployerTO GetGraduateOrStudentJob(int jobId);
        SandwichPlacementTO GetSandwichOrPlacementJob(int jobId);
        OneDayChallengeCharityVolunteerTO GetOnedDayChallengeOrCharityVolunteeringJob(int jobId);
        PlacementsInternationalVolunteerTO GetPlacementOrInternationalVolunteeringJob(int jobId);
 
    }
}
