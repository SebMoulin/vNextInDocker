using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using BusinessServices.Contracts;
using Commons.Entities;
using Framework.Configuration.Contracts;

namespace BusinessServices.Services
{
    public class EmailService : IEmailService
    {
        private readonly IProvideConfig _configProvider;

        public EmailService(IProvideConfig configProvider)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            _configProvider = configProvider;
        }

        public void SendEmail(EnvironmentSetUpResult environmentSetUpResult)
        {
            var mail = new MailMessage();

            var smtpClient = new SmtpClient(_configProvider.GetSmtpServer());
            var smtpPort = _configProvider.GetSmtpServerPort();
            if(!string.IsNullOrWhiteSpace(smtpPort))
            {
                smtpClient.Port = int.Parse(smtpPort);
            }

            var bodyHtml = PrepareBody(environmentSetUpResult);

            mail.From = new MailAddress(_configProvider.GetNotificationEmailFrom());
            mail.To.Add(environmentSetUpResult.User.Email);
            mail.Subject = _configProvider.GetNotificationEmailSubject();
            mail.SubjectEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Body = bodyHtml;
            mail.BodyEncoding = Encoding.UTF8;

            smtpClient.Send(mail);
        }

        private string PrepareBody(EnvironmentSetUpResult environmentSetUpResult)
        {
            var htmlEmailTemplate = GetHtmlEmailTemplate();
            if (string.IsNullOrWhiteSpace(htmlEmailTemplate)) return htmlEmailTemplate;

            var parsedBody = string.Format(htmlEmailTemplate,
                environmentSetUpResult.User.Username,
                environmentSetUpResult.User.Password,
                environmentSetUpResult.WebUrl,
                environmentSetUpResult.HttpUrlToRepo,
                environmentSetUpResult.SshUrlToRepo);
            return parsedBody;
        }

        private string GetHtmlEmailTemplate()
        {
            var htmlEmailTemplate = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(Constants.EmbeddedEmailTemplateFilePath))
            {
                if (stream == null) return htmlEmailTemplate;
                using (var reader = new StreamReader(stream))
                {
                    htmlEmailTemplate = reader.ReadToEnd();
                }
            }
            return htmlEmailTemplate;
        }
    }
}