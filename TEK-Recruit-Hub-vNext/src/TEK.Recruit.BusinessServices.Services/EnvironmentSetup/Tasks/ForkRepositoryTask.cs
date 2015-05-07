using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;
using TEK.Recruit.Framework.Configuration.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class ForkRepositoryTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IProvideConfig _configProvider;
        private readonly IGitLabApi _gitLabApi;
        private readonly string _devEnv;

        public ForkRepositoryTask(IProvideConfig configProvider, IGitLabApi gitLabApi, string devEnv)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            if (string.IsNullOrWhiteSpace(devEnv)) throw new ArgumentNullException("devEnv");
            _configProvider = configProvider;
            _gitLabApi = gitLabApi;
            _devEnv = devEnv;
            CanRunInParallel = false;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            token.DevEnv = (DevEnv)Enum.Parse(typeof(DevEnv), _devEnv);
            var devEnvRepoName = GetDevEnvRepoName(token.DevEnv);
            var response = await _gitLabApi.ForkRepository(token.User.Email, token.User.Password, devEnvRepoName, token.AdminToken);

            token.ProjectId = response.ReturnedObject.Id;
            token.Message = response.SuccessfullyCreated ? "Respository forked" : "An error occured while forking the repository";
            token.Success = response.SuccessfullyCreated;
            return token;
        }
        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.Success;
        }

        private string GetDevEnvRepoName(DevEnv devEnv)
        {
            string devEnvName;
            switch (devEnv)
            {
                case DevEnv.Android:
                    devEnvName = _configProvider.GetAndroisTemplateRepoName();
                    break;
                case DevEnv.iOs:
                    devEnvName = _configProvider.GetIosTemplateRepoName();
                    break;
                case DevEnv.Java:
                    devEnvName = _configProvider.GetJavaTemplateRepoName();
                    break;
                default:
                    devEnvName = _configProvider.GetDoNetTemplateRepoName();
                    break;
            }
            return devEnvName;
        }
    }
}