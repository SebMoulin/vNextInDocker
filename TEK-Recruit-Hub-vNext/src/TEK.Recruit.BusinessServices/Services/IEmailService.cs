using System.Threading.Tasks;

namespace TEK.Recruit.BusinessServices.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string username, string password, string email, string webUrl, string recruiterEmail);
    }
}