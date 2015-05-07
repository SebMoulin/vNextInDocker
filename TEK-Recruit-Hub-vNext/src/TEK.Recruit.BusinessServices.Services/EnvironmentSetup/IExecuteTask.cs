using System.Threading.Tasks;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup
{
    public interface IExecuteTask<TToken>
    {
        bool CanRunInParallel { get; }
        Task<TToken> Execute(TToken token);
        bool CanContinue(TToken token);
    }
}
