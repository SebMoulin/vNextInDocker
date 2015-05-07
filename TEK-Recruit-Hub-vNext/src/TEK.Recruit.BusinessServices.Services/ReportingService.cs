using System;
using System.Linq;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Geolocation;
using TEK.Recruit.Commons.Entities.GitLab;
using TEK.Recruit.Commons.Entities.Interview;
using TEK.Recruit.Commons.Entities.Sonar;
using TEK.Recruit.Commons.Extensions;
using TEK.Recruit.DataAccessLayer.Services;
using TEK.Recruit.Framework.Configuration.Services;

namespace TEK.Recruit.BusinessServices.Services
{
    public class ReportingService : IHandleReporting
    {
        private readonly IProvideConfig _configProvider;
        private readonly IGitLabApi _gitLabApi;
        private readonly IElasticSearhApi _elasticSearhApi;
        private readonly ISonarApi _sonarApi;
        private readonly IHandleCandidateInterview _candidateInterviewService;
        private readonly IGeolocator _geolocator;
        private readonly IGeoHash _geoHash;
        private readonly IGenerateElasticSearchReports _elasticSearchReportGenerator;

        public ReportingService(IProvideConfig configProvider, IGitLabApi gitLabApi, IElasticSearhApi elasticSearhApi, ISonarApi sonarApi, IHandleCandidateInterview candidateInterviewService, IGeolocator geolocator, IGeoHash geoHash, IGenerateElasticSearchReports elasticSearchReportGenerator)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            if (elasticSearhApi == null) throw new ArgumentNullException("elasticSearhApi");
            if (sonarApi == null) throw new ArgumentNullException("sonarApi");
            if (candidateInterviewService == null) throw new ArgumentNullException("candidateInterviewService");
            if (geolocator == null) throw new ArgumentNullException("geolocator");
            if (elasticSearchReportGenerator == null) throw new ArgumentNullException("elasticSearchReportGenerator");
            _configProvider = configProvider;
            _gitLabApi = gitLabApi;
            _elasticSearhApi = elasticSearhApi;
            _sonarApi = sonarApi;
            _candidateInterviewService = candidateInterviewService;
            _geolocator = geolocator;
            _geoHash = geoHash;
            _elasticSearchReportGenerator = elasticSearchReportGenerator;
        }

        public async Task<bool> GenerateReport(string customerId, string projectId, string candidateid)
        {
            var adminToken = await _gitLabApi.GetAdminToken();

            var firstBatchTasks = new Task[]
            {
                _gitLabApi.GetProjectById(projectId, adminToken),
                _gitLabApi.GetProjectCommits(projectId, adminToken),
                _candidateInterviewService.GetCandidateInterviewById(projectId, candidateid),
                GetRecuiterReportElasticSearchRecordId(customerId, projectId, candidateid),
                _gitLabApi.GetUserById(candidateid, adminToken)
            };
            await Task.WhenAll(firstBatchTasks);

            var project = ((Task<GitLabProject>)firstBatchTasks[0]).Result;
            var commits = ((Task<GitLabCommit[]>)firstBatchTasks[1]).Result;
            var candidateInterview = ((Task<Interview>)firstBatchTasks[2]).Result;
            var elasticSearchRecordId = ((Task<string>)firstBatchTasks[3]).Result;
            var candidate = ((Task<GitLabUser>)firstBatchTasks[4]).Result;
            var candidateCommits = commits.Where(c => c.AuthorName == candidate.Name).ToArray();

            var secondBatchTasks = new Task[]
            {
                GetSonarMetrics(project.Name),
                _geolocator
                    .GetGeoCoordonateByCity(candidateInterview.CandidateProfile.Address.City)
                    .ContinueWith( previous => GetGeoHash(previous.Result).Result)
            };
            await Task.WhenAll(secondBatchTasks);

            var sonarMetrics = ((Task<SonarMetrics>)secondBatchTasks[0]).Result;
            var geoHash = ((Task<string>)secondBatchTasks[1]).Result;
            var newRecuiterReport = await _elasticSearchReportGenerator.GenerateRecruiterReport(project, candidateCommits, sonarMetrics, candidateInterview, geoHash);

            var cleanedCustomerName = candidateInterview.CustomerName.RemoveAllSpacesAnLower();

            var response = elasticSearchRecordId != null
                ? await _elasticSearhApi.UpdateRecuiterReport(elasticSearchRecordId, newRecuiterReport, cleanedCustomerName)
                : await _elasticSearhApi.CreateRecuiterReport(newRecuiterReport, cleanedCustomerName);
            return response.SuccessfullyCreated;
        }


        private async Task<SonarMetrics> GetSonarMetrics(string projectName)
        {
            var allSonarMetrics = await _sonarApi.GetAllSonarMetrics();
            var sonarMetrics = allSonarMetrics.FirstOrDefault(sm => sm.Name == projectName);
            return sonarMetrics;
        }
        private async Task<string> GetRecuiterReportElasticSearchRecordId(string customerId, string projectId, string candidateId)
        {
            string elasticSearchRecordId = null;
            var reportIndex = _configProvider.GetElasticSearchRecruiterReportIndex();
            var customer = await _elasticSearhApi.GetCustomerbyId(customerId);
            var elIndex = _elasticSearhApi.GenerateCustomerRecruiterReportIndex(customer.Name.RemoveAllSpacesAnLower(), reportIndex);

            var getReportResponse = await _elasticSearhApi.GetRecuiterReport(elIndex, projectId, candidateId);

            if (getReportResponse != null
                && getReportResponse.HitsHeader.Total.Equals(1))
            {
                elasticSearchRecordId = getReportResponse.HitsHeader.Hits.First().Id;
            }

            return elasticSearchRecordId;
        }
        private async Task<string> GetGeoHash(GeoLocation geolocation)
        {
            string location = null;
            if (geolocation != null
                && geolocation.Response != null
                && geolocation.Response.Docs.Any())
            {
                var firstResult = geolocation.Response.Docs.FirstOrDefault();
                if (firstResult != null)
                {
                    location = await _geoHash.GetGeoLocationHash(firstResult.Lat, firstResult.Lng);
                }
            }
            return location;
        }
    }
}
