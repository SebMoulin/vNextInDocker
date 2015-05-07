using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TEK.Recruit.BusinessServices.Services;
using TEK.Recruit.BusinessServices.Services.EnvironmentSetup;
using TEK.Recruit.Commons.Entities;

namespace TEK.Recruit.SetUpCandidateRuntime.Installers
{
    public class BusinessServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<IHandleAdmin>()
                .ImplementedBy(typeof(AdminService))
                .LifestyleTransient());
            container.Register(Component
                .For<ISetUpCodingExcerciseEnvironment>()
                .ImplementedBy(typeof(CodingExcerciseEnvironmentSetUpService))
                .LifestyleTransient());
            container.Register(Component
                .For<IManageCodingExcerciseEnvironment>()
                .ImplementedBy(typeof(CodingExcerciseEnvironmentService))
                .LifestyleTransient());
            container.Register(Component
                .For<IHandleReporting>()
                .ImplementedBy(typeof(ReportingService))
                .LifestyleSingleton());
            container.Register(Component
                .For<IGenerateElasticSearchReports>()
                .ImplementedBy(typeof(ElasticSearchReportsGenerator))
                .LifestyleSingleton());
            container.Register(Component
                .For<IHandleCandidateInterview>()
                .ImplementedBy(typeof(CandidateInterviewService))
                .LifestyleSingleton());
            container.Register(Component
                .For<IEmailService>()
                .ImplementedBy(typeof(EmailService))
                .LifestyleSingleton());
            container.Register(Component
                .For<ICoordonateTasks<EnvironmentSetUpResult>>()
                .ImplementedBy(typeof(EnvironmentSetupCoordinator))
                .LifestyleTransient());
        }
    }
}