using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.Commons.Entities.GitLab;
using TEK.Recruit.Commons.Entities.Interview;
using TEK.Recruit.DataAccessLayer.Services;
using TEK.Recruit.Framework.Configuration.Services;

namespace TEK.Recruit.BusinessServices.Services
{
    public class CodingExcerciseEnvironmentService :  IManageCodingExcerciseEnvironment
    {
        private readonly IProvideConfig _configProvider;
        private readonly IGitLabApi _gitLabApi;
        private readonly IElasticSearhApi _elasticSearhApi;
        private readonly ISlackApi _slackApi;
        
        public CodingExcerciseEnvironmentService(IProvideConfig configProvider, IGitLabApi gitLabApi, IElasticSearhApi elasticSearhApi, ISlackApi slackApi)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            if (elasticSearhApi == null) throw new ArgumentNullException("elasticSearhApi");
            if (slackApi == null) throw new ArgumentNullException("slackApi");
            _configProvider = configProvider;
            _gitLabApi = gitLabApi;
            _elasticSearhApi = elasticSearhApi;
            _slackApi = slackApi;
        }

        public async Task<CandidateEnvironment[]> GetAllCandidateEnvironments()
        {
            var adminToken = await _gitLabApi.GetAdminToken();

            var tasks = new Task[]
            {
                _gitLabApi.GetAllProjects(adminToken),
                _elasticSearhApi.GetAllInterviews()
            };
            await Task.WhenAll(tasks);
            var allProjects = ((Task<GitLabProject[]>)tasks[0]).Result;
            var allInterview = ((Task<Interview[]>)tasks[1]).Result;

            var projects = allProjects.Where(p => p.PatWithNamespace.Contains(_configProvider.GetCodingExerciseGroupName())).ToArray();
            if (!projects.Any()) return new CandidateEnvironment[0];

            //TODO: getPorjectMenber to check is user access has been removed

            var environments = new List<CandidateEnvironment>();
            foreach (var project in projects)
            {
                var interview = allInterview
                    .FirstOrDefault(i => i.CandidateProfile.ProjectId == project.Id);
                if (interview == null)
                {
                    continue;
                }
                var environment = new CandidateEnvironment
                {
                    CandidateId = interview.CandidateProfile.CandidateId,
                    CandidateName = interview.CandidateProfile.Name,
                    Username = interview.CandidateProfile.Username,
                    CreatedAt = project.CreatedAt,
                    Position = interview.CandidateProfile.Position,
                    LastActivityAt = project.LastActivityAt,
                    CustomerId = interview.CustomerId,
                    CustomerName = interview.CustomerName,
                    DevEnv = interview.CandidateProfile.DevEnv,
                    Email = interview.CandidateProfile.Email,
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                    City = interview.CandidateProfile.Address.City,
                    Country = interview.CandidateProfile.Address.Country,
                    PostalCode = interview.CandidateProfile.Address.PostalCode,
                    State = interview.CandidateProfile.Address.State,
                    TekCenter = interview.CandidateProfile.TEKCenter
                };
                environments.Add(environment);
            }
            return environments.ToArray();
        }

        public async Task<bool> DeleteCandidateProject(string projectId, string candidateId)
        {
            var adminToken = await _gitLabApi.GetAdminToken();
            var success = await _gitLabApi.DeleteProject(projectId, adminToken);
            if (success)
                success = await _gitLabApi.DeleteUser(candidateId, adminToken);
            return success;
        }

        public async Task<bool> RemoveUserAccess(string projectId, string candidateId)
        {
            var adminToken = await _gitLabApi.GetAdminToken();
            var accessRemoved = await _gitLabApi.RemoveUserAccess(projectId, candidateId, adminToken);
            var message = string.Format("Recruitment Hub Notification: coding excercise for candidate {0} is ready to be review.", candidateId);
            return await _slackApi.SendToSlack(message);
        }
    }
}