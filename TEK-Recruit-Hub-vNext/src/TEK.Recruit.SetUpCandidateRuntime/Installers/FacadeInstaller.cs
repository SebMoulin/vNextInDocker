using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TEK.Recruit.Facade.Services;

namespace TEK.Recruit.SetUpCandidateRuntime.Installers
{
    public class FacadeInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<ICodingExcerciseEnvironmentFacade>()
                .ImplementedBy(typeof(CodingExcerciseEnvironmentFacade))
                .LifestyleTransient());
            container.Register(Component
                .For<IHandleMapping>()
                .ImplementedBy(typeof(MappingService))
                .LifestyleTransient());
        }
    }
}
