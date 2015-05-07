using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class SetCandidateAsDevolperOfForkedProjectTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IGitLabApi _gitLabApi;

        public SetCandidateAsDevolperOfForkedProjectTask(IGitLabApi gitLabApi)
        {
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            _gitLabApi = gitLabApi;
            CanRunInParallel = true;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var response = await _gitLabApi.SetCandidateAsDevolperOfForkedProject(token.ProjectId, token.User.Id, token.AdminToken);
            token.Message = response.SuccessfulRequest ? "Candidate set as developer of the forked project" : "An error occured while setting the candidate as developer of the forked project";
            token.Success = response.SuccessfulRequest;
            return token;
        }

        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.Success;
        }
    }
}