using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Commons.Entities.ElasticSearch;
using DataAccessLayer.Contracts;
using Framework.Configuration.Contracts;
using Framework.Http;
using Framework.Http.Contracts;

namespace DataAccessLayer.Services
{
    [ExcludeFromCodeCoverage]
    public class ElasticSearhApi : IElasticSearhApi
    {
        private readonly IHandleHttpRequest _httpRequestHandler;
        private readonly IProvideConfig _configProvider;

        public ElasticSearhApi(IProvideConfig configProvider, IHandleHttpRequest httpRequestHandler)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (httpRequestHandler == null) throw new ArgumentNullException("httpRequestHandler");
            _httpRequestHandler = httpRequestHandler;
            _configProvider = configProvider;
            _httpRequestHandler.Init(_configProvider.GetElasticSearchBaseUrl(), _configProvider.GetElasticSearchBaseUrlApiVersion());
        }

        public async Task<RestApiResponse<ElasticSearchResponse>> UpdateRecuiterReport(string elasticSearchRecordId, RecruiterReport elasticSearchReport)
        {
            var elasticSearchRepoPath = _configProvider.GetElasticSearchReportSchemaPath();
            return await _httpRequestHandler.PutHttpRequest<ElasticSearchResponse>(string.Format("{0}/{1}", elasticSearchRepoPath, elasticSearchRecordId), elasticSearchReport, null);
        }

        public async Task<RestApiResponse<ElasticSearchResponse>> CreateRecuiterReport(RecruiterReport elasticSearchReport)
        {
            //Put or Push a new document
            //Doc: http://www.elastic.co/guide/en/elasticsearch/reference/master/_modifying_your_data.html

            //Search / query
            //doc: http://www.elastic.co/guide/en/elasticsearch/reference/master/docs-index_.html

            //Post without id for insert with Post
            var elasticSearchRepoPath = _configProvider.GetElasticSearchReportSchemaPath();
            return await _httpRequestHandler.PostHttpRequest<ElasticSearchResponse>(elasticSearchRepoPath, elasticSearchReport, null);
        }

        public async Task<RestApiResponse<ElasticSearchResponse>> UpdateCandidateEvaluation(string elasticSearchRecordId, CandidateEvaluation candidateEval)
        {
            var candidateEvalRepoSchemaPath = _configProvider.GetElasticSearchCandidatesSchemaPath();
            return await _httpRequestHandler.PutHttpRequest<ElasticSearchResponse>(string.Format("{0}/{1}", candidateEvalRepoSchemaPath, elasticSearchRecordId), candidateEval, null);
        }

        public async Task<RestApiResponse<ElasticSearchResponse>> CreateCandidateEvaluation(CandidateEvaluation candidateEval)
        {
            var candidateEvalRepoSchemaPath = _configProvider.GetElasticSearchCandidatesSchemaPath();
            return await _httpRequestHandler.PostHttpRequest<ElasticSearchResponse>(candidateEvalRepoSchemaPath, candidateEval, null);
        }

        public async Task<RestApiResponse<ElasticSearchQueryResponse<CandidateEvaluation>>> GetCandidateEvaluation(string projectId, string candidateid)
        {
            var candidateEvalRepoSchemaPath = _configProvider.GetElasticSearchCandidatesSchemaPath();
            var query = new ElasticSearchQuery()
            {
                Query = new Query()
                {
                    QueryString = new QueryString()
                    {
                        Query = string.Format("(projectId:{0} AND candidateId:{1})",projectId, candidateid)
                    }
                }
            };
            return await _httpRequestHandler.PostHttpRequest<ElasticSearchQueryResponse<CandidateEvaluation>>(string.Format("{0}/_search", candidateEvalRepoSchemaPath), query,  null);
        }

        public async Task<RestApiResponse<ElasticSearchQueryResponse<RecruiterReport>>> GetRecuiterReport(string projectId, string candidateid)
        {
            var candidateEvalRepoSchemaPath = _configProvider.GetElasticSearchReportSchemaPath();
            var query = new ElasticSearchQuery()
            {
                Query = new Query()
                {
                    QueryString = new QueryString()
                    {
                        Query = string.Format("(projectId:{0} AND candidateId:{1})", projectId, candidateid)
                    }
                }
            };
            return await _httpRequestHandler.PostHttpRequest<ElasticSearchQueryResponse<RecruiterReport>>(string.Format("{0}/_search", candidateEvalRepoSchemaPath), query, null);
        }
    }
}
