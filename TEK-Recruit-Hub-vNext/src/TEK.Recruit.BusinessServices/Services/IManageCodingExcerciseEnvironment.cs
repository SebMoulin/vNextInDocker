using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;

namespace TEK.Recruit.BusinessServices.Services
{
    public interface IManageCodingExcerciseEnvironment
    {
        Task<CandidateEnvironment[]> GetAllCandidateEnvironments();
        Task<bool> DeleteCandidateProject(string projectid, string candidateid);
        Task<bool> RemoveUserAccess(string projectid, string candidateid);
    }
}
