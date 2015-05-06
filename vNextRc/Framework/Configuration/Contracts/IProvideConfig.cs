using System;

namespace Framework.Configuration.Contracts
{
    public interface IProvideConfig
    {
        string GetDoNetTemplateRepoName();
        string GetJavaTemplateRepoName();
        string GetGitLabBaseUrl();
        string GetGitLabApiVersion();
        string GetAdminName();
        string GetAdminUsername();
        string GetAdminEmail();
        string GetAdminPassword();
        string GetSmtpServer();
        string GetSmtpServerPort();
        string GetNotificationEmailFrom();
        string GetNotificationEmailSubject();
        string GetJenkinsUsername();
        string GetCodingExerciseGroupName();
        string GetElasticSearchBaseUrl();
        string GetElasticSearchBaseUrlApiVersion();
        string GetElasticSearchReportSchemaPath();
        string GetElasticSearchCandidatesSchemaPath();
        string GetSonarBaseUrl();
        string GetSonarApiVersion();
        string GetSonarMetricsList();
        string GetGeolocatorBaseUrl();
        string GetGeoHashBaseUrl();
    }
}