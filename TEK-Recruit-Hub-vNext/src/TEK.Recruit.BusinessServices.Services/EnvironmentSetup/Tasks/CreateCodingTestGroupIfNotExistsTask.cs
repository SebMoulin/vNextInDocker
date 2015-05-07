using System;
using System.Linq;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;
using TEK.Recruit.Framework.Configuration.Services;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class CreateCodingTestGroupIfNotExistsTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IProvideConfig _configProvider;
        private readonly IGitLabApi _gitLabApi;

        public CreateCodingTestGroupIfNotExistsTask(IProvideConfig configProvider, IGitLabApi gitLabApi)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            _configProvider = configProvider;
            _gitLabApi = gitLabApi;
            CanRunInParallel = false;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var search = await _gitLabApi.SearchCodingExerciseGroup(token.AdminToken);
            var groupName = _configProvider.GetCodingExerciseGroupName();
            if (search.ToList().Any(g => g.Name == groupName))
            {
                token.Group = search.ToList().First(g => g.Name == groupName);
                token.Message = "Default Coding Exercise Group of projects found";
                token.Success = true;
            }
            else
            {
                var response = await _gitLabApi.CreateCodingExerciseGroup(token.AdminToken);
                token.Group = response.ReturnedObject;
                token.Message = response.SuccessfullyCreated ? "Created default Coding Exercise Group" : "Not able to create default Coding Exercise Group of projects";
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