using System;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Windsor.Installer;
using TEK.Recruit.Facade;
using TEK.Recruit.Web;

namespace TEK.Recruit.SetUpCandidateRuntime
{
    public class WindsorApplicationRuntime : IContainerAccessor, IDisposable
    {
        private readonly IWindsorContainer _container;

        WindsorApplicationRuntime(IWindsorContainer container)
        {
            _container = container;
        }

        public static async Task<WindsorApplicationRuntime> StartApplication(IWindsorContainer container)
        {
            container.Install(FromAssembly.This());
            AutoMapperBootstrap.Configure();
            AutoMapperConfiguration.Configure();
            await DatabaseConfiguration.Configure(container);
            return new WindsorApplicationRuntime(container);
        }

        public void Shutdown()
        {
            Dispose();
        }

        public IWindsorContainer Container
        {
            get { return _container; }
        }

        public void Dispose()
        {
            if (Container != null)
                Container.Dispose();
        }
    }
}
