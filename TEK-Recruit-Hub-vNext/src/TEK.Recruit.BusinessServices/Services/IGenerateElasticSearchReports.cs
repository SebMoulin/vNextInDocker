using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.GitLab;
using TEK.Recruit.Commons.Entities.Interview;
using TEK.Recruit.Commons.Entities.Sonar;

namespace TEK.Recruit.BusinessServices.Services
{
    public interface IGenerateElasticSearchReports
    {
        Task<RecruiterReport> GenerateRecruiterReport(GitLabProject project, GitLabCommit[] commits, SonarMetrics sonarMetrics, Interview candidateInterview, string location);
    }
}
