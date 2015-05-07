using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class UpdateProjectVisibilityAndNameTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IGitLabApi _gitLabApi;

        public UpdateProjectVisibilityAndNameTask(IGitLabApi gitLabApi)
        {
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            _gitLabApi = gitLabApi;
            CanRunInParallel = false;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var newName = string.Format("{0}-coding-excercise-{1}", token.DevEnv, token.User.Username);

            var response = await _gitLabApi.UpdateProjectVisibilityAndName(token.ProjectId, newName, token.AdminToken);
            token.ProjectId = response.ReturnedObject.Id;
            token.HttpUrlToRepo = new Uri(response.ReturnedObject.HttpUrlToRepo);
            token.SshUrlToRepo = response.ReturnedObject.SshUrlToRepo;
            token.WebUrl = new Uri(response.ReturnedObject.WebUrl);

            token.Message = response.SuccessfulRequest ? "Project visibility level set to private, renamed with username concat" : "An error occured while updating the project visibility and name";
            token.Success = response.SuccessfulRequest;
            return token;
        }

        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.Success;
        }
    }
}