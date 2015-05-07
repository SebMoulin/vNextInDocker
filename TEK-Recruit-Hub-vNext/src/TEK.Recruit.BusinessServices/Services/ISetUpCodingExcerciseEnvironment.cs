using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;

namespace TEK.Recruit.BusinessServices.Services
{
    public interface ISetUpCodingExcerciseEnvironment
    {
        Task<EnvironmentSetUpResult> CreateCodingExcerciseEnvironment(string name, string email, string username, string customerId, string customerName, string devEnv, string city, string postalCode, string state, string country, string position, string tekCenter, string recruiterEmail);
    }
}
