using System.Threading.Tasks;
using Castle.Windsor;
using TEK.Recruit.Facade.Services;

namespace TEK.Recruit.SetUpCandidateRuntime
{
    public static class DatabaseConfiguration
    {
        public static async Task Configure(IWindsorContainer container)
        {
            var facade = container.Resolve<ICodingExcerciseEnvironmentFacade>();
            var result = await facade.CreateDefaultCustomer();
        }
    }
}
