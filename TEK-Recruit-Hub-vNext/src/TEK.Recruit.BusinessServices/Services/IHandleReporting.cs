using System.Threading.Tasks;

namespace TEK.Recruit.BusinessServices.Services
{
    public interface IHandleReporting
    {
        Task<bool> GenerateReport(string customerId, string projectId, string candidateId);
    }
}
