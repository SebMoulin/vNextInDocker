using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TEK.Recruit.Framework.Configuration.Services;
using TEK.Recruit.Framework.Http.Services;

namespace TEK.Recruit.SetUpCandidateRuntime.Installers
{
    public class FrameworkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<IHandleHttpRequest>()
                .ImplementedBy(typeof(HttpRequestHandler))
                .LifestyleTransient());
            container.Register(Component
                .For<IProvideConfig>()
                .ImplementedBy(typeof(ConfigProvider))
                .LifestyleSingleton());
        }
    }
}
