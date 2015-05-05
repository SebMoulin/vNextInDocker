using System;
using System.Threading.Tasks;
using BusinessServices.Contracts;
using Commons.Dtos;
using Commons.Entities;
using Commons.Entities.ElasticSearch;
using Commons.Entities.GitLab;
using Facade.Contracts;

namespace Facade.Services
{
    public class CodingExcerciseEnvironmentFacade : ICodingExcerciseEnvironmentFacade
    {
        private readonly IManageCodingExcerciseEnvironment _codingExcerciseEnvironmentManager;

        public CodingExcerciseEnvironmentFacade(IManageCodingExcerciseEnvironment codingExcerciseEnvironmentManager)
        {
            if (codingExcerciseEnvironmentManager == null) throw new ArgumentNullException("codingExcerciseEnvironmentManager");
            _codingExcerciseEnvironmentManager = codingExcerciseEnvironmentManager;
        }

        public async Task<EnvironmentSetUpResult> CreateNewCodingExcerciseEnvironment(string name, string email, string username, string devEnv)
        {
            return await _codingExcerciseEnvironmentManager.StartEnvironmentCreation(name, email, username, devEnv);
        }

        public async Task<GitLabProject[]> GetAllProjects()
        {
            return await _codingExcerciseEnvironmentManager.GetAllCandiatesProjects();
        }

        public async Task<bool> DeleteTestEnv(string projectid, string candidateid)
        {
            return await _codingExcerciseEnvironmentManager.DeleteCandidateTestEnv(projectid, candidateid);
        }

        public async Task<bool> RemoveUserAccess(string projectid, string candidateid)
        {
            return await _codingExcerciseEnvironmentManager.RemoveUserAccess(projectid, candidateid);
        }

        public async Task<bool> GenerateReport(string projectid, string candidateid)
        {
            return await _codingExcerciseEnvironmentManager.GenerateReport(projectid, candidateid);
        }

        public async Task<bool> SaveCandidateEvaluation(CandidateEvaluationDto dto)
        {
            return await _codingExcerciseEnvironmentManager.SaveCandidateEvaluation(dto);

        }

        public async Task<CandidateEvaluation> GetCandidateEvaluation(string projectid, string candidateid)
        {
            return await _codingExcerciseEnvironmentManager.GetCandidateEvaluation(projectid, candidateid);
        }
    }
}