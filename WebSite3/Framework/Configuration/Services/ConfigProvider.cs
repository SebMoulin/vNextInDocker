using System;
using Framework.Configuration.Contracts;
using Microsoft.Framework.ConfigurationModel;

namespace Framework.Configuration.Services
{
    public class ConfigProvider : IProvideConfig
    {
        private IConfiguration _config;

        public ConfigProvider(IConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");
            _config = config;
        }

        public string GetDoNetTemplateRepoName()
        {
            return _config.Get("dotnet-coding-exercise-template-name");
        }
        public string GetJavaTemplateRepoName()
        {
            return _config.Get("java-coding-exercise-template-name");
        }
        public string GetGitLabBaseUrl()
        {
            return _config.Get("gitLab-base-url");
        }
        public string GetGitLabApiVersion()
        {
            return _config.Get("gitLab-api-version");
        }
        public string GetAdminName()
        {
            return _config.Get("admin-name");
        }
        public string GetAdminUsername()
        {
            return _config.Get("admin-username");
        }
        public string GetAdminEmail()
        {
            return _config.Get("admin-email");
        }
        public string GetAdminPassword()
        {
            return _config.Get("admin-password");
        }
        public string GetSmtpServer()
        {
            return _config.Get("smtp-server");
        }
        public string GetSmtpServerPort()
        {
            return _config.Get("smtp-server-port");
        }
        public string GetNotificationEmailFrom()
        {
            return _config.Get("notification-email-from");
        }
        public string GetNotificationEmailSubject()
        {
            return _config.Get("notification-email-subject");
        }
        public string GetJenkinsUsername()
        {
            return _config.Get("jenkins-username");
        }
        public string GetCodingExerciseGroupName()
        {
            return _config.Get("coding-exercise-group-name");
        }
        public string GetElasticSearchBaseUrl()
        {
            return _config.Get("elasticSearch-base-url");
        }
        public string GetElasticSearchBaseUrlApiVersion()
        {
            return _config.Get("elasticSearch-api-version");
        }
        public string GetElasticSearchReportSchemaPath()
        {
            return _config.Get("elasticSearch-report-schema-path");
        }
        public string GetElasticSearchCandidatesSchemaPath()
        {
            return _config.Get("elasticSearch-candidates-schema-path");
        }
        public string GetSonarBaseUrl()
        {
            return _config.Get("sonar-base-url");
        }
        public string GetSonarApiVersion()
        {
            return _config.Get("sonar-api-version");
        }
        public string GetSonarMetricsList()
        {
            return _config.Get("sonar-metrics");
        }
        public string GetGeolocatorBaseUrl()
        {
            return _config.Get("geolocation-base-url");
        }
        public string GetGeoHashBaseUrl()
        {
            return _config.Get("geohash-base-url");
        }
    }
}