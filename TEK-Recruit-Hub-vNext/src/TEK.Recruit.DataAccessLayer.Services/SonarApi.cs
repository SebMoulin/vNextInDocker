using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Sonar;
using TEK.Recruit.Framework.Configuration.Services;
using TEK.Recruit.Framework.Http.Services;

namespace TEK.Recruit.DataAccessLayer.Services
{
    [ExcludeFromCodeCoverage]
    public class SonarApi : ISonarApi
    {
        private readonly IProvideConfig _configProvider;
        private readonly IHandleHttpRequest _httpRequestHandler;

        public SonarApi(IProvideConfig configProvider, IHandleHttpRequest httpRequestHandler)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (httpRequestHandler == null) throw new ArgumentNullException("httpRequestHandler");
            _configProvider = configProvider;
            _httpRequestHandler = httpRequestHandler;
            _httpRequestHandler.Init(configProvider.GetSonarBaseUrl(), _configProvider.GetSonarApiVersion());
        }

        public async Task<SonarMetrics> GetSonarMetricsForProject(string projectName)
        {
            var metrics = _configProvider.GetSonarMetricsList();
            return await _httpRequestHandler.GetHttpRequest<SonarMetrics>(string.Format("resources?resource={0}&metrics={1}", projectName, metrics), null);
        }

        public async Task<SonarMetrics[]> GetAllSonarMetrics()
        {
            var metrics = _configProvider.GetSonarMetricsList();
            return await _httpRequestHandler.GetHttpRequest<SonarMetrics[]>(string.Format("resources?metrics={0}", metrics), null);
        }
    }
}