using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Interview;

namespace TEK.Recruit.BusinessServices.Services
{
    public interface IHandleAdmin
    {
        Task<bool> DeleteRecruiterReportIndex(string customerId);
        Task<bool> CreateRecruiterReportMapping(string customerId);
        Task<bool> CreateCustomer(string customerName);
        Task<Customer[]> GetAllCustomers();
    }
}
