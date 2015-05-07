using System;
using System.Linq;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.GitLab;
using TEK.Recruit.Commons.Entities.Interview;
using TEK.Recruit.Commons.Entities.Sonar;
using TEK.Recruit.Commons.Extensions;

namespace TEK.Recruit.BusinessServices.Services
{
    public class ElasticSearchReportsGenerator : IGenerateElasticSearchReports
    {
        public Task<RecruiterReport> GenerateRecruiterReport(GitLabProject project, 
            GitLabCommit[] commits, 
            SonarMetrics sonarMetrics, 
            Interview candidateInterview, string hash)
        {
            var report = new RecruiterReport { Location = hash };
            SetSonarValues(sonarMetrics, report);
            SetCandidateInterviewValues(candidateInterview, report);
            SetGitLabProjectValues(project, report);
            SetGitLabCommitsValues(commits, project, report);
            return Task.FromResult(report);
        }
        
        private void SetGitLabCommitsValues(GitLabCommit[] commits, GitLabProject project, RecruiterReport report)
        {
            if (commits != null
                && commits.Any())
            {
                report.CommitsCount = commits.Count();
                report.FirstCommitDate = commits.FirstCommitDate();
                if (commits.Length >= 1)
                {
                    report.LastCommitDate = commits.LastCommitDate();
                }
                if (project.CreatedAt != null)
                    report.HoursTakenToCompleteCodingExcercise = commits.HoursTakenToCompleteCodingExcercise(project.CreatedAt.Value);
            }
        }
        private void SetGitLabProjectValues(GitLabProject project, RecruiterReport report)
        {
            if (project == null) return;
            report.ProjectId = project.Id;
            report.ProjectName = project.Name;
            report.ProjectWebUrl = project.WebUrl;
            report.ProjectCreatedAt = project.CreatedAt ?? DateTime.Now;
            report.ProjectLastActivityAt = project.LastActivityAt ?? DateTime.Now;
        }
        private void SetCandidateInterviewValues(Interview candidateInterview, RecruiterReport report)
        {
            if (candidateInterview == null) return;
            report.CustomerId = candidateInterview.CustomerId;
            report.CustomerName = candidateInterview.CustomerName;
            if (candidateInterview.CandidateProfile != null)
            {
                report.Email = candidateInterview.CandidateProfile.Email;
                report.CandidateName = candidateInterview.CandidateProfile.Name;
                report.Username = candidateInterview.CandidateProfile.Username;
                report.CandidateId = candidateInterview.CandidateProfile.CandidateId;
                report.DevEnv = candidateInterview.CandidateProfile.DevEnv;
                report.Position = candidateInterview.CandidateProfile.Position;
                report.TekCenter = candidateInterview.CandidateProfile.TEKCenter;
                if (candidateInterview.CandidateProfile.Address != null)
                {
                    report.City = candidateInterview.CandidateProfile.Address.City;
                    report.PostalCode = candidateInterview.CandidateProfile.Address.PostalCode;
                    report.State = candidateInterview.CandidateProfile.Address.State;
                    report.Country = candidateInterview.CandidateProfile.Address.Country;
                }
            }

            if (candidateInterview.InterviewEyeball != null)
            {
                report.CodeQuality = candidateInterview.InterviewEyeball.CodeQuality;
                report.Agenda = candidateInterview.InterviewEyeball.Agenda;
                report.PassedFirstScreening = candidateInterview.InterviewEyeball.PassedTheSelection;
                report.PassedFirstScreeningDate = candidateInterview.InterviewEyeball.PassedTheSelectionDate;
            }
            if (candidateInterview.InterviewFeedback != null)
            {
                report.CulturalFit = candidateInterview.InterviewFeedback.CulturalFit;
                report.TechnicalInterview = candidateInterview.InterviewFeedback.TechnicalInterview;
            }
            if (candidateInterview.FinalAssement == null) return;
            report.FinalFeedback = candidateInterview.FinalAssement.FinalFeedback;
            report.PassedTheSelection = candidateInterview.FinalAssement.PassedTheSelection;
            report.PassedTheSelectionDate = candidateInterview.FinalAssement.PassedTheSelectionDate;
        }
        private void SetSonarValues(SonarMetrics sonarMetrics, RecruiterReport report)
        {
            if (sonarMetrics == null) return;
            report.CommentLinesDensity = sonarMetrics.Msr.CommentLinesDensity();
            report.Complexity = sonarMetrics.Msr.Complexity();
            report.Coverage = sonarMetrics.Msr.Coverage();
            report.DuplicatedBlocks = sonarMetrics.Msr.DuplicatedBlocks();
            report.Ncloc = sonarMetrics.Msr.Ncloc();
            report.SqaleIndex = sonarMetrics.Msr.SqaleIndex();
            report.Tests = sonarMetrics.Msr.Tests();
        }
    }
}