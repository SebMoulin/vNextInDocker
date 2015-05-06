using System.Threading.Tasks;
using Commons.Entities.ElasticSearch;
using Commons.Entities.GitLab;
using Commons.Entities.Sonar;

namespace BusinessServices.Contracts
{
    public interface IGenerateElasticSearchReports
    {
        Task<RecruiterReport> GenerateRecruiterReport(GitLabProject project, GitLabCommit[] commits, SonarMetrics sonarMetrics, CandidateEvaluation candidateEvaluation, string location);
    }
}
