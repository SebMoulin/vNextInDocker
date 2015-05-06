using Commons;
using Commons.Entities;

namespace BusinessServices.Contracts
{
    public interface IEmailService
    {
        void SendEmail(EnvironmentSetUpResult environmentSetUpResult);
    }
}