using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
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
            try
            {
                var mail = new MimeMessage()
                {
                    Sender = MailboxAddress.Parse(_options.Email),
                    Subject = email.Subject
                };

                mail.To.Add(MailboxAddress.Parse(email.To));
                var builder = new BodyBuilder
                {
                    HtmlBody = email.Body
                };
                mail.Body = builder.ToMessageBody();
                mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

                using var smtp = new SmtpClient();
                if (_options.Port <= 0)
                    throw new InvalidOperationException("Invalid port number.");
                smtp.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_options.Email, _options.Password);
                smtp.Send(mail);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
}
