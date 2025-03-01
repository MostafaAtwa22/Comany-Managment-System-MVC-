using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MimeKit;

namespace Comany_Managment_System_MVC_.EmailServices
{
    public class EmailSettings : IEmailSettings
    {
        private readonly MailSettingsVM _options;

        public EmailSettings(IOptions<MailSettingsVM> options)
        {
            _options = options.Value;
        }

        public void SendEmail(EmailVM email)
        {
            if (string.IsNullOrEmpty(email.To))
            {
                throw new ArgumentException("The 'To' field cannot be null or empty.");
            }

            var mail = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject
            };

            mail.To.Add(MailboxAddress.Parse(email.To));
            var builder = new BodyBuilder();
            builder.HtmlBody = email.Body;
            mail.Body = builder.ToMessageBody();

            mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

            using var smtp = new SmtpClient();

            smtp.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Email, _options.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
