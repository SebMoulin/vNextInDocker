using System.Threading.Tasks;
using TEK.Recruit.Commons.Dtos;

namespace TEK.Recruit.Facade.Services
{
    public interface ICodingExcerciseEnvironmentFacade
    {
        Task<EnvironmentSetUpResultDto> CreateCodingExcerciseEnvironment(CandidateEnvironmentDto candidateEnvironmentDto);
        Task<CandidateEnvironmentDto[]> GetAllCandidateEnvironments();
        Task<bool> DeleteTestEnv(string projectid, string candidateid);
        Task<bool> RemoveUserAccess(string projectid, string candidateid);
        Task<bool> GenerateReport(string customerId, string projectId, string candidateId);
        Task<bool> SaveCandidateInterview(InterviewDto dto);
        Task<InterviewDto> GetCandidateInterview(string projectid, string candidateid);
        Task<bool> DeleteRecruiterReportIndex(string customerId);
        Task<bool> CreateRecruiterReportMapping(string customerId);
        Task<bool> CreateCustomer(string customerName);
        Task<CustomerDto[]> GetAllCustomers();
        Task<bool> CreateDefaultCustomer();
    }
}
