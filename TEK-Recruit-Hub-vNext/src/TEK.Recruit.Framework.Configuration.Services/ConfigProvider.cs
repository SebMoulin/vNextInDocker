using System;
using Microsoft.Framework.ConfigurationModel;

namespace TEK.Recruit.Framework.Configuration.Services
{
    public class ConfigProvider : IProvideConfig
    {
        private readonly IConfiguration _config;

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
        public string GetAndroisTemplateRepoName()
        {
            return _config.Get("android-coding-exercise-template-name");
        }
        public string GetIosTemplateRepoName()
        {
            return _config.Get("ios-coding-exercise-template-name");
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
        public string GetJenkinsEmail()
        {
            return _config.Get("jenkins-email");
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
            return _config.Get("email-from");
        }
        public string GetNotificationEmailSubject()
        {
            return _config.Get("email-subject");
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
        public string GetElasticSearchRecruiterReportIndex()
        {
            return _config.Get("elasticSearch-report-index");
        }
        public string GetElasticSearchRecruiterReportType()
        {
            return _config.Get("elasticSearch-report-type");
        }
        public string GetElasticSearchCandidatesIndex()
        {
            return _config.Get("elasticSearch-candidates-index");
        }
        public string GetElasticSearchCandidatesType()
        {
            return _config.Get("elasticSearch-candidates-type");
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
        public string GetSlackBaseApi()
        {
            return _config.Get("slack-base-url");
        }
        public string GetAuthorizedGroups()
        {
            return _config.Get("authorized-groups");
        }

        public string GetEmailYouTubeIntroVideoUrl()
        {
            return _config.Get("email-youtube-video-url");
        }

        public string GetEmailYouTubeIntroVideoUrlFr()
        {
            return _config.Get("email-youtube-video-url-fr");
        }

        public string GetElasticSearchCustomersIndex()
        {
            return _config.Get("elasticSearch-customer-index");
        }

        public string GetElasticSearchCustomersType()
        {
            return _config.Get("elasticSearch-customer-type");
        }

        public string GetElasticSearchDefaultRecordSetSize()
        {
            return _config.Get("elasticSearch-recordset-size");
        }
    }
}
