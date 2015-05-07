using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class GetAdminTokenTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IGitLabApi _gitLabApi;
        public bool CanRunInParallel { get; private set; }

        public GetAdminTokenTask(IGitLabApi gitLabApi)
        {
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            _gitLabApi = gitLabApi;
            CanRunInParallel = false;
        }

        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var adminToken = await _gitLabApi.GetAdminToken();
            token.AdminToken = adminToken;
            return token;
        }

        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return !string.IsNullOrWhiteSpace(token.AdminToken);
        }
    }
}
