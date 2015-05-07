using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TEK.Recruit.Framework.Configuration.Services;

namespace TEK.Recruit.BusinessServices.Services
{
    public class EmailService : IEmailService
    {
        private readonly IProvideConfig _configProvider;

        public EmailService(IProvideConfig configProvider)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            _configProvider = configProvider;
        }

        public Task<bool> SendEmail(string username, string password, string email, string webUrl, string recruiterEmail)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    var mail = new MailMessage();

                    var smtpClient = new SmtpClient(_configProvider.GetSmtpServer());
                    var smtpPort = _configProvider.GetSmtpServerPort();
                    if (!string.IsNullOrWhiteSpace(smtpPort))
                    {
                        smtpClient.Port = int.Parse(smtpPort);
                    }

                    var bodyHtml = PrepareBody(username, password, webUrl);

                    mail.From = new MailAddress(_configProvider.GetNotificationEmailFrom());
                    mail.To.Add(email);
                    if (!string.IsNullOrWhiteSpace(recruiterEmail))
                    {
                        mail.Bcc.Add(recruiterEmail);
                    }
                    mail.Subject = _configProvider.GetNotificationEmailSubject();
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Body = bodyHtml;
                    mail.BodyEncoding = Encoding.UTF8;

                    smtpClient.Send(mail);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        private string PrepareBody(string username, string password, string webUrl)
        {
            var htmlEmailTemplate = GetHtmlEmailTemplate();
            if (string.IsNullOrWhiteSpace(htmlEmailTemplate)) return htmlEmailTemplate;

            var youTubeVideoUrl = _configProvider.GetEmailYouTubeIntroVideoUrl();
            var youTubeVideoUrlfr = _configProvider.GetEmailYouTubeIntroVideoUrlFr();

            var parsedBody = string.Format(htmlEmailTemplate,
                username,
                password,
                youTubeVideoUrl,
                youTubeVideoUrlfr,
                webUrl);
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