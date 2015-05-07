using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TEK.Recruit.Commons.Entities.ElasticSearch;
using TEK.Recruit.Commons.Entities.Interview;
using TEK.Recruit.Framework.Configuration.Services;
using TEK.Recruit.Framework.Http;
using TEK.Recruit.Framework.Http.Services;

namespace TEK.Recruit.DataAccessLayer.Services
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

        public Task<RestApiResponse<ElasticSearchResponse>> UpdateRecuiterReport(string elasticSearchRecordId, RecruiterReport elasticSearchReport, string cleanedCustomerName)
        {
            var elasticSearchRecruiterReportIndex = _configProvider.GetElasticSearchRecruiterReportIndex();
            var elasticSearchReportType = _configProvider.GetElasticSearchRecruiterReportType();
            var eLindex = GenerateCustomerRecruiterReportIndex(cleanedCustomerName, elasticSearchRecruiterReportIndex);

            return _httpRequestHandler.PutHttpRequest<ElasticSearchResponse>(string.Format("{0}/{1}/{2}", eLindex, elasticSearchReportType, elasticSearchRecordId), elasticSearchReport, null);
        }

        public async Task<RestApiResponse<ElasticSearchResponse>> CreateRecuiterReport(RecruiterReport elasticSearchReport, string cleanedCustomerName)
        {
            //Put or Push a new document
            //Doc: http://www.elastic.co/guide/en/elasticsearch/reference/master/_modifying_your_data.html

            //Search / query
            //doc: http://www.elastic.co/guide/en/elasticsearch/reference/master/docs-index_.html

            //Post without id for insert with Post
            var elasticSearchRecruiterReportIndex = _configProvider.GetElasticSearchRecruiterReportIndex();
            var elasticSearchReportType = _configProvider.GetElasticSearchRecruiterReportType();

            var eLindex = GenerateCustomerRecruiterReportIndex(cleanedCustomerName, elasticSearchRecruiterReportIndex);
            var mappingExists = await MappingExists(eLindex);
            if (!mappingExists)
            {
                var result = await CreateRecruiterReportMapping(eLindex);
            }

            return await _httpRequestHandler.PostHttpRequest<ElasticSearchResponse>(string.Format("{0}/{1}", eLindex, elasticSearchReportType), elasticSearchReport, null);
        }

        public Task<RestApiResponse<ElasticSearchResponse>> UpdateCandidateEvaluation(string elasticSearchRecordId, Interview interview)
        {
            var elasticSearchCandidatesIndex = _configProvider.GetElasticSearchCandidatesIndex();
            var elasticSearchCandidatesType = _configProvider.GetElasticSearchCandidatesType();

            return _httpRequestHandler.PutHttpRequest<ElasticSearchResponse>(string.Format("{0}/{1}/{2}", elasticSearchCandidatesIndex, elasticSearchCandidatesType, elasticSearchRecordId), interview, null);
        }

        public Task<RestApiResponse<ElasticSearchResponse>> CreateCandidateEvaluation(Interview interview)
        {
            var elasticSearchCandidatesIndex = _configProvider.GetElasticSearchCandidatesIndex();
            var elasticSearchCandidatesType = _configProvider.GetElasticSearchCandidatesType();

            return _httpRequestHandler.PostHttpRequest<ElasticSearchResponse>(string.Format("{0}/{1}", elasticSearchCandidatesIndex, elasticSearchCandidatesType), interview, null);
        }

        public Task<ElasticSearchQueryResponse<Interview>> GetCandidateInterview(string projectId, string candidateId)
        {
            var elasticSearchCandidatesIndex = _configProvider.GetElasticSearchCandidatesIndex();
            var elasticSearchRecruiterReportType = _configProvider.GetElasticSearchCandidatesType();

            return _httpRequestHandler.GetHttpRequest<ElasticSearchQueryResponse<Interview>>(
                string.Format("{0}/{1}/_search?q=projectId:{2}&candidateId:{3}",
                elasticSearchCandidatesIndex,
                elasticSearchRecruiterReportType,
                projectId,
                candidateId), null);
        }

        public Task<ElasticSearchQueryResponse<RecruiterReport>> GetRecuiterReport(string elIndex, string projectId, string candidateId)
        {
            var elasticSearchRecruiterReportType = _configProvider.GetElasticSearchRecruiterReportType();
            return _httpRequestHandler.GetHttpRequest<ElasticSearchQueryResponse<RecruiterReport>>(
                string.Format("{0}/{1}/_search?q=projectId:{2}&candidateId:{3}",
                elIndex,
                elasticSearchRecruiterReportType,
                projectId,
                candidateId), null);
        }

        public Task<bool> DeleteRecruiterReportIndex(string elIndex)
        {
            return _httpRequestHandler.DeleteHttpRequest(elIndex, null);
        }

        public Task<bool> DeleteCandidateInterviewIndex(string candidatesIndexName)
        {
            return _httpRequestHandler.DeleteHttpRequest(candidatesIndexName, null);
        }

        public Task<RestApiResponse<ElasticSearchResponse>> CreateRecruiterReportMapping(string eLindex)
        {
            var json = GetRecruiterReportJsonMapping();
            return _httpRequestHandler.PutHttpRequest<ElasticSearchResponse>(eLindex, json, null);
        }

        public Task<RestApiResponse<ElasticSearchResponse>> CreateCustomer(Customer customer)
        {
            var elasticSearchCustomersIndex = _configProvider.GetElasticSearchCustomersIndex();
            var elasticSearchCustomersType = _configProvider.GetElasticSearchCustomersType();

            return _httpRequestHandler.PostHttpRequest<ElasticSearchResponse>(string.Format("{0}/{1}", elasticSearchCustomersIndex, elasticSearchCustomersType), customer, null);
        }

        public async Task<Customer[]> GetAllCustomers()
        {
            var elasticSearchCustomersIndex = _configProvider.GetElasticSearchCustomersIndex();
            var elasticSearchCustomersType = _configProvider.GetElasticSearchCustomersType();
            var result = await _httpRequestHandler.GetHttpRequest<ElasticSearchQueryResponse<Customer>>(string.Format("{0}/{1}/_search", elasticSearchCustomersIndex, elasticSearchCustomersType), null);
            return result != null && result.HitsHeader.Hits.Any()
                ? result.HitsHeader.Hits.Select(h => h.Source).ToArray()
                : new Customer[0];
        }

        public async Task<Interview[]> GetAllInterviews()
        {
            var elasticSearchCandidatesIndex = _configProvider.GetElasticSearchCandidatesIndex();
            var elasticSearchCandidatesType = _configProvider.GetElasticSearchCandidatesType();
            var elasticSearchDefaultRecordSetSize = _configProvider.GetElasticSearchDefaultRecordSetSize();
            var result = await _httpRequestHandler.GetHttpRequest<ElasticSearchQueryResponse<Interview>>(string.Format("{0}/{1}/_search?pretty=true&size={2}", elasticSearchCandidatesIndex, elasticSearchCandidatesType, elasticSearchDefaultRecordSetSize), null);
            return result != null && result.HitsHeader.Hits.Any()
                ? result.HitsHeader.Hits.Select(h => h.Source).ToArray()
                : new Interview[0];
        }

        public async Task<Customer> GetCustomerbyId(string customerId)
        {
            var elasticSearchCustomersIndex = _configProvider.GetElasticSearchCustomersIndex();
            var elasticSearchCustomersType = _configProvider.GetElasticSearchCustomersType();

            var result = await _httpRequestHandler.GetHttpRequest<ElasticSearchQueryResponse<Customer>>(
                string.Format("{0}/{1}/_search?q=id:{2}",
                elasticSearchCustomersIndex,
                elasticSearchCustomersType,
                customerId), null);


            if (result == null) return default(Customer);

            return result.HitsHeader.Hits.Any() && result.HitsHeader.Total.Equals(1F)
                ? result.HitsHeader.Hits.First().Source
                : default(Customer);
        }

        public async Task<bool> MappingExists(string eLindex)
        {
            var result = await _httpRequestHandler.GetHttpRequest(string.Format("{0}/_mapping", eLindex));
            return result != null;
        }

        public string GenerateCustomerRecruiterReportIndex(string cleanCustomerName, string elasticSearchRecruiterReportIndex)
        {
            return string.Format("{0}-{1}", elasticSearchRecruiterReportIndex, cleanCustomerName);
        }

        private Object GetRecruiterReportJsonMapping()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(Constants.EmbeddedJsonMappingDefinitionFilePath))
            {
                if (stream == null) return null;
                using (var reader = new StreamReader(stream))
                {
                    using (var textReader = new JsonTextReader(reader))
                    {
                        return JToken.ReadFrom(textReader);
                    }
                }
            }
        }
    }
}
