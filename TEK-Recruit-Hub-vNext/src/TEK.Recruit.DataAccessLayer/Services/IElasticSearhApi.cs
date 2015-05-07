using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.ElasticSearch;
using TEK.Recruit.Commons.Entities.Interview;
using TEK.Recruit.Framework.Http;

namespace TEK.Recruit.DataAccessLayer.Services
{
    public interface IElasticSearhApi
    {
        Task<RestApiResponse<ElasticSearchResponse>> UpdateRecuiterReport(string elasticSearchRecordId,RecruiterReport elasticSearchReport, string cleanCustomerName);
        Task<RestApiResponse<ElasticSearchResponse>> CreateRecuiterReport(RecruiterReport elasticSearchReport, string cleanedCustomerName);
        Task<RestApiResponse<ElasticSearchResponse>> UpdateCandidateEvaluation(string elasticSearchRecordId, Interview interview);
        Task<RestApiResponse<ElasticSearchResponse>> CreateCandidateEvaluation(Interview interview);
        Task<ElasticSearchQueryResponse<Interview>> GetCandidateInterview(string projectId, string candidateId);
        Task<ElasticSearchQueryResponse<RecruiterReport>> GetRecuiterReport(string eLindex, string projectId,string candidateId);
        Task<bool> DeleteRecruiterReportIndex(string elIndex);
        Task<bool> DeleteCandidateInterviewIndex(string candidatesIndexName);
        Task<RestApiResponse<ElasticSearchResponse>> CreateRecruiterReportMapping(string eLindex);
        Task<RestApiResponse<ElasticSearchResponse>> CreateCustomer(Customer customer);
        Task<Customer[]> GetAllCustomers();
        Task<Interview[]> GetAllInterviews();
        Task<Customer> GetCustomerbyId(string customerId);
        Task<bool> MappingExists(string eLindex);
        string GenerateCustomerRecruiterReportIndex(string cleanCustomerName, string elasticSearchRecruiterReportIndex);
    }
}
