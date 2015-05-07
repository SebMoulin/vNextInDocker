using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup.Tasks
{
    internal class SendEmailNotificationsTask : IExecuteTask<EnvironmentSetUpResult>
    {
        private readonly IEmailService _emailService;
        private readonly string _recruiterEmail;

        public SendEmailNotificationsTask(IEmailService emailService, string recruiterEmail)
        {
            if (emailService == null) throw new ArgumentNullException("emailService");
            _emailService = emailService;
            _recruiterEmail = recruiterEmail;
            CanRunInParallel = true;
        }

        public bool CanRunInParallel { get; private set; }
        public async Task<EnvironmentSetUpResult> Execute(EnvironmentSetUpResult token)
        {
            var success = await _emailService.SendEmail(token.User.Username, token.User.Password, token.User.Email, token.WebUrl.ToString(), _recruiterEmail);
            token.Success = success;
            token.Message = success 
                ? "Welcome email has been sent to candidate" 
                : "An error occured while sending the email to the candidate";

            return token;
        }
        public bool CanContinue(EnvironmentSetUpResult token)
        {
            return token.Success;
        }
    }
}