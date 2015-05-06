using System.Threading.Tasks;
using Commons.Entities.ElasticSearch;
using Framework.Http;

namespace DataAccessLayer.Contracts
{
    public interface IElasticSearhApi
    {
        Task<RestApiResponse<ElasticSearchResponse>> UpdateRecuiterReport(string elasticSearchRecordId, RecruiterReport elasticSearchReport);
        Task<RestApiResponse<ElasticSearchResponse>> CreateRecuiterReport(RecruiterReport elasticSearchReport);
        Task<RestApiResponse<ElasticSearchResponse>> UpdateCandidateEvaluation(string elasticSearchRecordId, CandidateEvaluation candidateEval);
        Task<RestApiResponse<ElasticSearchResponse>> CreateCandidateEvaluation(CandidateEvaluation candidateEval);
        Task<RestApiResponse<ElasticSearchQueryResponse<CandidateEvaluation>>> GetCandidateEvaluation(string projectId, string candidateid);
        Task<RestApiResponse<ElasticSearchQueryResponse<RecruiterReport>>> GetRecuiterReport(string projectId, string candidateid);
    }
}
