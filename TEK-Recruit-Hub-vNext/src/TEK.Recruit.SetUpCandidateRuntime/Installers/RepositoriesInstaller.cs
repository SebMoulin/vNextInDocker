using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TEK.Recruit.DataAccessLayer.Services;

namespace TEK.Recruit.SetUpCandidateRuntime.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<IGeolocator>()
                .ImplementedBy(typeof(Geolocator))
                .LifestyleTransient());
            container.Register(Component
                .For<IGeoHash>()
                .ImplementedBy(typeof(GeoHasher))
                .LifestyleTransient());
            container.Register(Component
                .For<IElasticSearhApi>()
                .ImplementedBy(typeof(ElasticSearhApi))
                .LifestyleTransient());
            container.Register(Component
                .For<ISonarApi>()
                .ImplementedBy(typeof(SonarApi))
                .LifestyleTransient());
            container.Register(Component
                .For<ISlackApi>()
                .ImplementedBy(typeof(SlackApi))
                .LifestyleTransient());
            container.Register(Component
                .For<IGitLabApi>()
                .ImplementedBy(typeof(GitLabApi))
                .LifestyleTransient());
        }
    }
}
