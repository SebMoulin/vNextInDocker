using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class MoveForkedRepoToGroupTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IGitLabApi _gitLabApi;

        public MoveForkedRepoToGroupTask(IGitLabApi gitLabApi)
        {
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            _gitLabApi = gitLabApi;
            CanRunInParallel = false;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var response = await _gitLabApi.MoveForkedRepoToGroup(token.Group.Id, token.ProjectId, token.AdminToken);
            token.Message = response.SuccessfullyCreated ? "Moved forked project into default group" : "An error occured while moving the candidate's project into the default group.";
            token.Success = response.SuccessfullyCreated;
            return token;
        }
        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.Success;
        }
    }
}