using System.Threading.Tasks;
using Commons.Dtos;
using Commons.Entities;
using Commons.Entities.ElasticSearch;
using Commons.Entities.GitLab;

namespace Facade.Contracts
{
    public interface ICodingExcerciseEnvironmentFacade
    {
        Task<EnvironmentSetUpResult> CreateNewCodingExcerciseEnvironment(string name, string email, string username, string devEnv);
        Task<GitLabProject[]> GetAllProjects();
        Task<bool> DeleteTestEnv(string projectid, string candidateid);
        Task<bool> RemoveUserAccess(string projectid, string candidateid);
        Task<bool> GenerateReport(string projectid, string candidateid);
        Task<bool> SaveCandidateEvaluation(CandidateEvaluationDto dto);
        Task<CandidateEvaluation> GetCandidateEvaluation(string projectid, string candidateid);
    }
}