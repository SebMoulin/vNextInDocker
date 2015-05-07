using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class AddJenkinsUserToForkedProjectTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IGitLabApi _gitLabApi;

        public AddJenkinsUserToForkedProjectTask(IGitLabApi gitLabApi)
        {
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            _gitLabApi = gitLabApi;
            CanRunInParallel = true;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            if (token.DevEnv == DevEnv.Java
                || token.DevEnv == DevEnv.Android)
            {
                var response = await _gitLabApi.AddJenkinsUserToForkedProject(token.ProjectId, token.AdminToken);
                token.Message = response.SuccessfullyCreated
                    ? "Jenkins user added to project team project"
                    : "An error occured while adding jenkins to the forked project";
                token.Success = response.SuccessfullyCreated;
            }
            return token;
        }

        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.Success;
        }
    }
}