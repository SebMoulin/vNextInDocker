namespace TEK.Recruit.Framework.Configuration.Services
{
    public interface IProvideConfig
    {
        string GetDoNetTemplateRepoName();
        string GetJavaTemplateRepoName();
        string GetAndroisTemplateRepoName();
        string GetIosTemplateRepoName();
        string GetGitLabBaseUrl();
        string GetGitLabApiVersion();
        string GetAdminName();
        string GetAdminUsername();
        string GetAdminEmail();
        string GetJenkinsEmail();
        string GetAdminPassword();
        string GetSmtpServer();
        string GetSmtpServerPort();
        string GetNotificationEmailFrom();
        string GetNotificationEmailSubject();
        string GetJenkinsUsername();
        string GetCodingExerciseGroupName();
        string GetElasticSearchBaseUrl();
        string GetElasticSearchBaseUrlApiVersion();
        string GetElasticSearchRecruiterReportIndex();
        string GetElasticSearchRecruiterReportType();
        string GetElasticSearchCandidatesIndex();
        string GetElasticSearchCandidatesType();
        string GetSonarBaseUrl();
        string GetSonarApiVersion();
        string GetSonarMetricsList();
        string GetGeolocatorBaseUrl();
        string GetGeoHashBaseUrl();
        string GetSlackBaseApi();
        string GetAuthorizedGroups();
        string GetEmailYouTubeIntroVideoUrl();
        string GetEmailYouTubeIntroVideoUrlFr();
        string GetElasticSearchCustomersIndex();
        string GetElasticSearchCustomersType();
        string GetElasticSearchDefaultRecordSetSize();
    }
}
