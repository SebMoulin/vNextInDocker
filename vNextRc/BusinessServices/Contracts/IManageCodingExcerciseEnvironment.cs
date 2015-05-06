using System.Threading.Tasks;
using Commons.Dtos;
using Commons.Entities;
using Commons.Entities.ElasticSearch;
using Commons.Entities.GitLab;

namespace BusinessServices.Contracts
{
    public interface IManageCodingExcerciseEnvironment
    {
        Task<GitLabProject[]> GetAllCandiatesProjects();
        Task<bool> DeleteCandidateTestEnv(string projectid, string candidateid);
        Task<EnvironmentSetUpResult> StartEnvironmentCreation(string name, string email, string username, string devEnv);
        Task<bool> RemoveUserAccess(string projectid, string candidateid);
        Task<bool> GenerateReport(string projectId, string candidateid);
        Task<bool> SaveCandidateEvaluation(CandidateEvaluationDto dto);
        Task<CandidateEvaluation> GetCandidateEvaluation(string projectid, string candidateid);
    }
}
