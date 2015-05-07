using System;
using System.Threading.Tasks;
using TEK.Recruit.BusinessServices.Services.EnvironmentSetup;
using TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks;
using TEK.Recruit.Commons.Entities;
using TEK.Recruit.DataAccessLayer.Services;
using TEK.Recruit.Framework.Configuration.Services;

namespace TEK.Recruit.BusinessServices.Services
{
    public class CodingExcerciseEnvironmentSetUpService : ISetUpCodingExcerciseEnvironment
    {
        private readonly IProvideConfig _configProvider;
        private readonly IGitLabApi _gitLabApi;
        private readonly IHandleCandidateInterview _candidateInterviewService;
        private readonly IEmailService _emailService;
        private readonly ICoordonateTasks<EnvironmentSetUpResult> _environmentSetupCoordinator;

        public CodingExcerciseEnvironmentSetUpService(IProvideConfig configProvider, IGitLabApi gitLabApi, IHandleCandidateInterview candidateInterviewService, IEmailService emailService, ICoordonateTasks<EnvironmentSetUpResult> environmentSetupCoordinator)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (gitLabApi == null) throw new ArgumentNullException("gitLabApi");
            if (candidateInterviewService == null) throw new ArgumentNullException("candidateInterviewService");
            if (emailService == null) throw new ArgumentNullException("emailService");
            if (environmentSetupCoordinator == null) throw new ArgumentNullException("environmentSetupCoordinator");
            _configProvider = configProvider;
            _gitLabApi = gitLabApi;
            _candidateInterviewService = candidateInterviewService;
            _emailService = emailService;
            _environmentSetupCoordinator = environmentSetupCoordinator;
        }

        public async Task<EnvironmentSetUpResult> CreateCodingExcerciseEnvironment(string name, string email, string username, string customerId, string customerName, string devEnv, string city, string postalCode, string state, string country, string position, string tekCenter, string recruiterEmail)
        {
            _environmentSetupCoordinator.RegisterTask(100, new GetAdminTokenTask(_gitLabApi));
            _environmentSetupCoordinator.RegisterTask(200, new CreateUserIfNotExistsTask(_gitLabApi, customerId, customerName, email, name, username));
            _environmentSetupCoordinator.RegisterTask(300, new CreateCodingTestGroupIfNotExistsTask(_configProvider, _gitLabApi));
            _environmentSetupCoordinator.RegisterTask(400, new ForkRepositoryTask(_configProvider, _gitLabApi, devEnv));
            _environmentSetupCoordinator.RegisterTask(500, new MoveForkedRepoToGroupTask(_gitLabApi));
            _environmentSetupCoordinator.RegisterTask(600, new UpdateProjectVisibilityAndNameTask(_gitLabApi));
            _environmentSetupCoordinator.RegisterTask(700, new SetAdminAsOwnerOfForkedProjectTask(_gitLabApi));
            _environmentSetupCoordinator.RegisterTask(710, new AddJenkinsUserToForkedProjectTask(_gitLabApi));
            _environmentSetupCoordinator.RegisterTask(720, new SetCandidateAsDevolperOfForkedProjectTask(_gitLabApi));
            _environmentSetupCoordinator.RegisterTask(730, new SaveInitialCandiateInterviewTask(_candidateInterviewService, city, state, postalCode, country, position, tekCenter));
            _environmentSetupCoordinator.RegisterTask(740, new SendEmailNotificationsTask(_emailService, recruiterEmail));

            return await _environmentSetupCoordinator.StartProcess();
        }
    }
}
