using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class SetAdminAsOwnerOfForkedProjectTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IGitLabApi _gitLabApi;

        public SetAdminAsOwnerOfForkedProjectTask(IGitLabApi gitLabApi)
        {
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            _gitLabApi = gitLabApi;
            CanRunInParallel = true;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var response = await _gitLabApi.SetAdminAsOwnerOfForkedProject(token.ProjectId, token.AdminToken);
            token.Message = response.SuccessfullyCreated ? "Adminstrator set as owner of the forked project" : "An error occured while setting the Adminstrator as owner of the forked project";
            token.Success = response.SuccessfullyCreated;
            return token;
        }

        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.Success;
        }
    }
}