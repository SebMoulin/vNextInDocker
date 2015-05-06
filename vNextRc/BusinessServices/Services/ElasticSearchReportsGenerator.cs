using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.Contracts;
using Commons.Entities.ElasticSearch;
using Commons.Entities.GitLab;
using Commons.Entities.Sonar;
using Commons.Extensions;

namespace BusinessServices.Services
{
    public class ElasticSearchReportsGenerator : IGenerateElasticSearchReports
    {
        public Task<RecruiterReport> GenerateRecruiterReport(GitLabProject project, GitLabCommit[] commits, SonarMetrics sonarMetrics, CandidateEvaluation candidateEvaluation, string hash)
        {
            var report = new RecruiterReport();

            report.Location = hash;

            if (sonarMetrics != null)
            {
                report.CommentLinesDensity = sonarMetrics.Msr.CommentLinesDensity();
                report.Complexity = sonarMetrics.Msr.Complexity();
                report.Coverage = sonarMetrics.Msr.Coverage();
                report.DuplicatedBlocks = sonarMetrics.Msr.DuplicatedBlocks();
                report.Ncloc = sonarMetrics.Msr.Ncloc();
                report.SqaleIndex = sonarMetrics.Msr.SqaleIndex();
                report.Tests = sonarMetrics.Msr.Tests();
                report.Id = sonarMetrics.Id;
                report.Date = sonarMetrics.Date;
                report.CreationDate = sonarMetrics.CreationDate;
                report.Key = sonarMetrics.Key;
                report.Lname = sonarMetrics.Lname;
                report.Name = sonarMetrics.Name;
                report.Description = sonarMetrics.Description;
                report.Qualifier = sonarMetrics.Qualifier;
                report.Scope = sonarMetrics.Scope;
                report.Version = sonarMetrics.Version;
            }
            if (candidateEvaluation != null)
            {
                report.CandidateId = candidateEvaluation.CandidateId;
                report.CodeQuality = candidateEvaluation.CodeQuality;
                report.CulturalFit = candidateEvaluation.CulturalFit;
                report.TechnicalInterview = candidateEvaluation.TechnicalInterview;
                report.City = candidateEvaluation.City;
                report.PostalCode = candidateEvaluation.PostalCode;
                report.State = candidateEvaluation.State;
                report.Country = candidateEvaluation.Country;
                report.Position = candidateEvaluation.Position;
                report.TekLocation = candidateEvaluation.TekLocation;
            }
            if (project != null)
            {
                report.ProjectId = project.Id;
                report.ProjectName = project.Name;
                report.ProjectWebUrl = project.WebUrl;
                report.ProjectCreatedAt = project.CreatedAt.HasValue
                    ? project.CreatedAt.Value.ToString("F")
                    : DateTime.Now.ToString("F");
                report.ProjectLastActivityAt = project.LastActivityAt.HasValue
                    ? project.LastActivityAt.Value.ToString("F")
                    : DateTime.Now.ToString("F");
                //Assuming the project name starts by java- or dotnet-
                report.DevEnv = project.Name.Substring(0, project.Name.IndexOf('-'));
            }
            if (commits != null
                && commits.Any())
            {
                report.CommitsCount = commits.Count();
                report.FirstCommitDate = commits.FirstCommitDate();
                report.LastCommitDate = commits.LastCommitDate();
                report.CommitTimeBetweenFirstAndLast = commits.CommitTimeBetweenFirstAndLastInHours();
            }
            return Task.FromResult(report);
        }
    }
}