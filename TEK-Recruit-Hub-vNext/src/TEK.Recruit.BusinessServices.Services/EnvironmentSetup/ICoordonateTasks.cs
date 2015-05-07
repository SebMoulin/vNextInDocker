using System.Threading.Tasks;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup
{
    public interface ICoordonateTasks<TToken>
    {
        void RegisterTask(int order, IExecuteTask<TToken> task);
        Task<TToken> StartProcess();
    }
}
